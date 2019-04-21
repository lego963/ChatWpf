using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using ChatWpf.Core.IoC.Interfaces;
using ChatWpf.Core.Logging.Core;

namespace ChatWpf.Core.Logging.Implementation
{
    public class BaseLogFactory : ILogFactory
    {
        protected List<ILogger> mLoggers = new List<ILogger>();

        protected object mLoggersLock = new object();

        public LogOutputLevel LogOutputLevel { get; set; }

        public bool IncludeLogOriginDetails { get; set; } = true;

        public event Action<(string Message, LogLevel Level)> NewLog = (details) => { };

        public BaseLogFactory(ILogger[] loggers = null)
        {
            // Add console logger
            AddLogger(new DebugLogger());

            if (loggers != null)
                foreach (var logger in loggers)
                    AddLogger(logger);
        }

        public void AddLogger(ILogger logger)
        {
            lock (mLoggersLock)
            {
                if (!mLoggers.Contains(logger))
                    mLoggers.Add(logger);
            }
        }

        public void RemoveLogger(ILogger logger)
        {
            lock (mLoggersLock)
            {
                if (mLoggers.Contains(logger))
                    mLoggers.Remove(logger);
            }
        }

        public void Log(string message, LogLevel level = LogLevel.Informative,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if ((int)level < (int)LogOutputLevel)
                return;

            if (IncludeLogOriginDetails)
                message = $"{message} [{Path.GetFileName(filePath)} > {origin}() > Line {lineNumber}]";

            lock (mLoggersLock)
            {
                // Log to all loggers
                mLoggers.ForEach(logger => logger.Log(message, level));
            }


            NewLog.Invoke((message, level));
        }
    }
}
