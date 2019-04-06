using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ChatWpf.Core.IoC.Interfaces;
using ChatWpf.Core.Logging.Core;

namespace ChatWpf.Core.Task
{
    /// <summary>
    /// Handles anything to do with Tasks
    /// </summary>
    public class TaskManager : ITaskManager
    {
        public async System.Threading.Tasks.Task Run(Func<System.Threading.Tasks.Task> function, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                await System.Threading.Tasks.Task.Run(function);
            }
            catch (Exception ex)
            {
                LogError(ex, origin, filePath, lineNumber);

                throw;
            }
        }

        public async Task<TResult> Run<TResult>(Func<Task<TResult>> function, CancellationToken cancellationToken, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                return await System.Threading.Tasks.Task.Run(function, cancellationToken);
            }
            catch (Exception ex)
            {
                LogError(ex, origin, filePath, lineNumber);
                throw;
            }
        }

        public async Task<TResult> Run<TResult>(Func<Task<TResult>> function, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                return await System.Threading.Tasks.Task.Run(function);
            }
            catch (Exception ex)
            {
                LogError(ex, origin, filePath, lineNumber);
                throw;
            }
        }

        public async Task<TResult> Run<TResult>(Func<TResult> function, CancellationToken cancellationToken, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                return await System.Threading.Tasks.Task.Run(function, cancellationToken);
            }
            catch (Exception ex)
            {
                LogError(ex, origin, filePath, lineNumber);
                throw;
            }
        }

        public async Task<TResult> Run<TResult>(Func<TResult> function, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                return await System.Threading.Tasks.Task.Run(function);
            }
            catch (Exception ex)
            {
                LogError(ex, origin, filePath, lineNumber);
                throw;
            }
        }

        public async System.Threading.Tasks.Task Run(Func<System.Threading.Tasks.Task> function, CancellationToken cancellationToken, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                await System.Threading.Tasks.Task.Run(function, cancellationToken);
            }
            catch (Exception ex)
            {
                LogError(ex, origin, filePath, lineNumber);
                throw;
            }
        }

        public async System.Threading.Tasks.Task Run(Action action, CancellationToken cancellationToken, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                await System.Threading.Tasks.Task.Run(action, cancellationToken);
            }
            catch (Exception ex)
            {
                LogError(ex, origin, filePath, lineNumber);
                throw;
            }
        }

        public async System.Threading.Tasks.Task Run(Action action, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                await System.Threading.Tasks.Task.Run(action);
            }
            catch (Exception ex)
            {
                LogError(ex, origin, filePath, lineNumber);
                throw;
            }
        }

        private void LogError(Exception ex, string origin, string filePath, int lineNumber)
        {
            IoC.Base.IoC.Logger.Log($"An unexpected error occurred running a IoC.Task.Run. {ex.Message}", LogLevel.Debug, origin, filePath, lineNumber);
        }
    }
}