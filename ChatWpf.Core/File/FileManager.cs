using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ChatWpf.Core.Async;
using ChatWpf.Core.IoC.Interfaces;

namespace ChatWpf.Core.File
{
    public class FileManager : IFileManager
    {
        public async System.Threading.Tasks.Task WriteTextToFileAsync(string text, string path, bool append = false)
        {
            path = NormalizePath(path);

            path = ResolvePath(path);

            await AsyncAwaiter.AwaitAsync(nameof(FileManager) + path, async () =>
            {
                await IoC.Base.IoC.Task.Run(() =>
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
