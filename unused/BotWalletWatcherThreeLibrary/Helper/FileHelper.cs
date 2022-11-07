using AsyncIO.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BotWalletWatcher.Helper
{
    public class FileHelper
    {
        public static void EnsureHasFolder(string PathWorkingConfig)
        {
            if (Directory.Exists(PathWorkingConfig) == false)
            {
                Directory.CreateDirectory(PathWorkingConfig);
            }

        }
        public static async Task SaveAsync(string fileName,string content)
        {
            await AsyncFile.WriteAllTextAsync(fileName, content);
        }
        public static async Task<string> LoadAsync(string fileName)
        {
            var output = "";
            try
            {
                output = await AsyncFile.ReadAllTextAsync(fileName);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

                
            }
            
            output = output ?? "";
            return output;
        }

    }
}
