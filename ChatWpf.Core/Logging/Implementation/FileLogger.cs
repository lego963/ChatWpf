using System;
using System.Collections.Generic;
using System.Text;
using ChatWpf.Core.Logging.Core;

namespace ChatWpf.Core.Logging.Implementation
{
    public class FileLogger : ILogger
    {
        public string FilePath { get; set; }

        public bool LogTime { get; set; } = true;

        public FileLogger(string filePath)
        {
            // Set the file path property
            FilePath = filePath;
        }

        public void Log(string message, LogLevel level)
        {
            var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd hh:mm:ss");

            var timeLogString = LogTime ? $"[{ currentTime}] " : "";

            IoC.Base.IoC.File.WriteTextToFileAsync($"{timeLogString}{message}{Environment.NewLine}", FilePath, append: true);
        }

    }
}
