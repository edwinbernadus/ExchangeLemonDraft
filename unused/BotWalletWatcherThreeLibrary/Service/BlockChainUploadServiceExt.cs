using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BotWalletWatcher.Helper;
using BotWalletWatcherLibrary;
using LogLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BotWalletWatcher
{
    public class BlockChainUploadServiceExt
    {
        public static bool WatchMode { get; private set; }
        public static int nextBlockChainPosition;
        public static string PathConfig { get; set; }
        public string PathWorkingConfig { get; }
        string HostNameConfig { get; set; }

        //private readonly INotificationWatcherService NotificationService;

        public BlockChainUploadServiceExt(
                    //INotificationWatcherService notificationService,
                    HttpSendService httpSendService,
                    FileSaveService fileSaveService,
                    IConfiguration configuration
            )
        {
            //NotificationService = notificationService;
            HttpSendService = httpSendService;
            FileSaveService = fileSaveService;
            PathConfig = configuration.GetValue<string>("FolderSource");
            PathWorkingConfig = configuration.GetValue<string>("FolderWorking");
            HostNameConfig = configuration.GetValue<string>("SignalHostName");

            Console3.WriteLine($"[item-config] FolderSource - {PathConfig }");
            Console3.WriteLine($"[item-config] FolderWorking - {PathWorkingConfig }");
            Console3.WriteLine($"[item-config] SignalHostName - {HostNameConfig }");

          

        }


        
        
      
        public HttpSendService HttpSendService { get; }
        
        public FileSaveService FileSaveService { get; }
        

        public async Task ExecuteUpload()
        {
            Console3.WriteLine($"[LOGIC] - start execute upload");
            var isContinue = true;
            DisableWatchMode();
            while (isContinue)
            {
                
                Console3.WriteLine("[LOGIC] - get last position");
                int lastBlockChainPosition = await GetLastPositionAsync();
                if (lastBlockChainPosition == -1)
                {
                    Console3.WriteLine($"[LOGIC] - [ERROR] - GetLastPositionAsync");
                    await Task.Delay(5000);
                    break;
                }
                Console3.WriteLine($"[LOGIC] - result last position {lastBlockChainPosition}");
                nextBlockChainPosition = lastBlockChainPosition + 1;
                bool isExist = IsFileAvailable(nextBlockChainPosition, PathConfig);
                if (isExist)
                {
                    Console3.WriteLine($"[LOGIC] - new file detected at position: {nextBlockChainPosition}");

                    var fileNameSource = $"{nextBlockChainPosition}.zip";
                    var folderNameOutput = nextBlockChainPosition.ToString();
                    var fileNameRead = $"{nextBlockChainPosition}.txt";
                    var fullPathRead = System.IO.Path.Combine(PathWorkingConfig, folderNameOutput, fileNameRead);
                    var pathOutputRead = System.IO.Path.Combine(PathWorkingConfig, folderNameOutput);
                    var fullPathSource = System.IO.Path.Combine(PathConfig, fileNameSource);

                    FileHelper.EnsureHasFolder(PathWorkingConfig);
                    this.FileSaveService.UnzipFile(fullPathSource, pathOutputRead);


                    string content = ReadFile(fullPathRead);
                    Console3.WriteLine($"[LOGIC] - upload server position {nextBlockChainPosition}");
                    try
                    {
                        await UploadToServer(content);
                    }
                    catch (Exception ex)
                    {

                        Console3.WriteLine($"[LOGIC] - [ERROR] - UploadToServer");
                        await Task.Delay(5000);
                        break;

                    }

                    //await this.FileSaveService.Delete(fullPathRead);

                    this.FileSaveService.DeleteFolder(pathOutputRead);
                    try
                    {
                        this.FileSaveService.Delete(fullPathSource);
                        Console3.WriteLine($"[LOGIC] - delete file OK - {fullPathSource}");
                    }
                    catch (Exception ex)
                    {
                        Console3.WriteLine($"[LOGIC] - delete file FAILED - " +
                            $"{fullPathSource} - {ex.Message}");
                    }
                    Console3.WriteLine($"[LOGIC] - clean up temp files position {nextBlockChainPosition}");
                }
                else
                {
                    Console3.WriteLine($"[LOGIC] - {nextBlockChainPosition} not found");
                    EnableWatchMode();
                    isContinue = false;
                }
            }
        }

      

    

        private void EnableWatchMode()
        {
            Console3.WriteLine($"[LOGIC] - enable watch mode");
            WatchMode = true;
        }

        private string ReadFile(string fileName)
        {
            var output = File.ReadAllText(fileName);
            return output;
        }

        private void DisableWatchMode()
        {
            Console3.WriteLine($"[LOGIC] - disable watch mode");
            WatchMode = false;
        }


        private async Task UploadToServer(string content)
        {
            string url = HostNameConfig + "/BlockChainStatus/Upload";
            try
            {
                await this.HttpSendService.ExecutePost(url, content);
            }
            catch (Exception ex)
            {

                var m = ex.Message;
                throw ex;
            }
            
        }

        private bool IsFileAvailable(int blockChainPosition,string path)
        {
            List<string> files = GetFilesName(path);
            var output = (files.Any(x => x.Contains(blockChainPosition.ToString())));
            return output;
        }

        private List<string> GetFilesName(string path)
        {
            List<string> items = Directory.GetFiles(path).ToList(); ;
            return items;
        }

        private async Task<int> GetLastPositionAsync()
        {
            
            string url = HostNameConfig + "/BlockChainStatus/inquiry";
            try
            {
                var result = await this.HttpSendService.Execute(url);
                var output = int.Parse(result);
                return output;
            }
            catch (Exception ex)
            {

                return -1;
            }
            
        }

      

    }
}
