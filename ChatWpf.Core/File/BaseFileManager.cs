using System.IO;
using System.Runtime.InteropServices;
using ChatWpf.Core.Async;
using ChatWpf.Core.DI;
using ChatWpf.Core.DI.Interfaces;

namespace ChatWpf.Core.File
{
    public class BaseFileManager : IFileManager
    {
        public async System.Threading.Tasks.Task WriteTextToFileAsync(string text, string path, bool append = false)
        {
            // TODO: Add exception catching

            // Normalize path
            path = NormalizePath(path);

            // Resolve to absolute path
            path = ResolvePath(path);

            // Lock the task
            await AsyncAwaiter.AwaitAsync(nameof(BaseFileManager) + path, async () =>
            {
                // Run the synchronous file access as a new task
                await CoreDi.TaskManager.Run(() =>
                {
                    // Write the log message to file
                    using (var fileStream = (TextWriter)new StreamWriter(System.IO.File.Open(path, append ? FileMode.Append : FileMode.Create)))
                        fileStream.Write(text);
                });
            });
        }

        public string NormalizePath(string path)
        {
            // If on Windows...
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                // Replace any / with \
                return path?.Replace('/', '\\').Trim();
            // If on Linux/Mac
            else
                // Replace any \ with /
                return path?.Replace('\\', '/').Trim();
        }

        public string ResolvePath(string path)
        {
            // Resolve the path to absolute
            return Path.GetFullPath(path);
        }

    }
}
