namespace ChatWpf.Core.DI.Interfaces
{
    public interface IFileManager
    {
        System.Threading.Tasks.Task WriteTextToFileAsync(string text, string path, bool append = false);

        string NormalizePath(string path);

        string ResolvePath(string path);
    }
}
