using System.Threading.Tasks;

namespace ChatWpf.Core.IoC.Interfaces
{
    public interface IFileManager
    {
        System.Threading.Tasks.Task WriteTextToFileAsync(string text, string path, bool append = false);

        string NormalizePath(string path);

        string ResolvePath(string path);
    }
}