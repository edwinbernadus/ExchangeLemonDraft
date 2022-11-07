using System;
using System.IO;
using System.Threading.Tasks;

public class FileService : IFileService
{
    public async Task<string> Load(string fileName)
    {
        var path = AppDomain.CurrentDomain.BaseDirectory;
        var path2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        var content = await File.ReadAllTextAsync(path2);
        return content;
    }
    public async Task Save(string content , string fileName)
    {
        var path = AppDomain.CurrentDomain.BaseDirectory;
        var path2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

        await File.WriteAllTextAsync(path2, content);

    }
}
