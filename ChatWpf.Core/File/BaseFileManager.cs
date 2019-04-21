using System.IO;
using System.Runtime.InteropServices;
using ChatWpf.Core.Async;
using ChatWpf.Core.IoC.Interfaces;
using static ChatWpf.Core.DI.CoreDI;

namespace ChatWpf.Core.File
{
    public class BaseFileManager : IFileManager
    {
        public async System.Threading.Tasks.Task WriteTextToFileAsync(string text, string path, bool append = false)
        {
            path = NormalizePath(path);

            path = ResolvePath(path);

            await AsyncAwaiter.AwaitAsync(nameof(BaseFileManager) + path, async () =>
            {
                await TaskManager.Run(() =>
                {
                    using (var fileStream = (TextWriter)new StreamWriter(System.IO.File.Open(path, append ? FileMode.Append : FileMode.Create)))
                        fileStream.Write(text);
                });
            });
        }

        public string NormalizePath(string path)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return path?.Replace('/', '\\').Trim();
            else
                return path?.Replace('\\', '/').Trim();
        }

        public string ResolvePath(string path)
        {
            return Path.GetFullPath(path);
        }

    }
}
