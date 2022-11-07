// using System;
// using System.IO;
// using System.Threading.Tasks;
// // using BotWalletWatcher.Helper;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using PulseLogic;
// //using PulseLogic;

// namespace BotWalletWatcher
// {


//     public class WatchLogic
//     {


//         public static void Execute(BlockChainUploadServiceExt service1)
//         {


//             var path = BlockChainUploadServiceExt.PathConfig;
//             FileHelper.EnsureHasFolder(path);
//             var fileWatcher = new FileSystemWatcher(path);
//             fileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
//             fileWatcher.EnableRaisingEvents = true;
//             fileWatcher.IncludeSubdirectories = true;

//             fileWatcher.Created += async (sender, e) =>
//             {
//                 var mode = BlockChainUploadServiceExt.WatchMode;
//                 Console.WriteLine($"[{mode}] - Creating File: {e.FullPath}");

//                 if (ValidationExecuteIsValid(e.FullPath))
//                 {
//                     await Task.Delay(3000);
//                     await service1.ExecuteUpload();
//                 }
//             };

//             var folderWatcher = new FileSystemWatcher(Directory.GetCurrentDirectory());
//             folderWatcher.NotifyFilter = NotifyFilters.DirectoryName;
//             folderWatcher.EnableRaisingEvents = true;
//             folderWatcher.IncludeSubdirectories = true;

//             folderWatcher.Created += (sender, e) =>
//             {
//                 Console.WriteLine($"Creating Folder: {e.FullPath}");
//             };


//             System.Threading.Thread.Sleep(1000);

//         }

//         static bool ValidationExecuteIsValid(string inputPath)
//         {
//             if (BlockChainUploadServiceExt.WatchMode)
//             {
//                 var path = BlockChainUploadServiceExt.PathConfig;
//                 string targetFileName = $"{BlockChainUploadServiceExt.nextBlockChainPosition}.zip";
//                 var targetPath = Path.Combine(path, targetFileName);

//                 if (inputPath == targetPath)
//                 {
//                     Console.WriteLine($"[LOGIC] - new file detected - {inputPath}");
//                     return true;
//                 }
//             }

//             return false;
//         }
//     }
// }
