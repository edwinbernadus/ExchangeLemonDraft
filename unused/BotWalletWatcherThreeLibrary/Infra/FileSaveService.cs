using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace BotWalletWatcher
{


    public class FileSaveService
    {
        public void Delete(string fileName)
        {
            File.Delete(fileName);
        }

        internal void DeleteFolder(string pathOutputRead)
        {
            Directory.Delete(pathOutputRead, true);
        }

        public void Save(string fileName, object content)
        {
            var content2 = JsonConvert.SerializeObject(content);
            File.WriteAllText(fileName, content2);
        }

        public void Archive(string sourceFileName, string targetFilePath)
        {

            using (var archive = ZipArchive.Create())
            {
                archive.AddEntry(sourceFileName, sourceFileName);
                archive.SaveTo(targetFilePath, CompressionType.Deflate);
            }


        }



        internal void EnsureFolder(string target)
        {
            if (Directory.Exists(target) == false)
            {
                Directory.CreateDirectory(target);
            }
        }


        internal void UnzipFile(string fullPath, string outputFullPath)
        {

            using (Stream stream = File.Open(fullPath, FileMode.Open, FileAccess.Read))
            //using (Stream stream = File.OpenRead(fullPath))
            using (var reader = ReaderFactory.Open(stream))
            {
                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                    {
                        var key = reader.Entry.Key;
                        Console.WriteLine(reader.Entry.Key);

                        reader.WriteEntryToDirectory(outputFullPath, new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
            }
        }


    }
}
