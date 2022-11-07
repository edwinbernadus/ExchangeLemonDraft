using System.Threading.Tasks;

public interface IFileService
{
    Task<string> Load(string fileName);
    Task Save(string content, string fileName);
}