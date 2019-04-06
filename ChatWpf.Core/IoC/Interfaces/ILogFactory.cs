using System;
using System.Runtime.CompilerServices;
using ChatWpf.Core.Logging.Core;

namespace ChatWpf.Core.IoC.Interfaces
{
    public interface ILogFactory
    {
        LogOutputLevel LogOutputLevel { get; set; }

        bool IncludeLogOriginDetails { get; set; }

        event Action<(string Message, LogLevel Level)> NewLog;

        void AddLogger(ILogger logger);

        void RemoveLogger(ILogger logger);

        void Log(string message, LogLevel level = LogLevel.Informative, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0);

    }
}
