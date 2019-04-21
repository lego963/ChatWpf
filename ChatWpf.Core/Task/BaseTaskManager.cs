using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ChatWpf.Core.IoC.Interfaces;
using ChatWpf.Core.Logging.Core;
using static Dna.FrameworkDI;
using Dna;

namespace ChatWpf.Core.Task
{
    /// <summary>
    /// Handles anything to do with Tasks
    /// </summary>
    public class BaseTaskManager : ITaskManager
    {
        public async System.Threading.Tasks.Task Run(Func<System.Threading.Tasks.Task> function, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                await System.Threading.Tasks.Task.Run(function);
            }
            catch (Exception ex)
            {
                Logger.LogErrorSource(ex.ToString(), origin: origin, filePath: filePath, lineNumber: lineNumber);

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
                Logger.LogErrorSource(ex.ToString(), origin: origin, filePath: filePath, lineNumber: lineNumber);

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
                Logger.LogErrorSource(ex.ToString(), origin: origin, filePath: filePath, lineNumber: lineNumber);

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
                Logger.LogErrorSource(ex.ToString(), origin: origin, filePath: filePath, lineNumber: lineNumber);

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
                Logger.LogErrorSource(ex.ToString(), origin: origin, filePath: filePath, lineNumber: lineNumber);

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
                Logger.LogErrorSource(ex.ToString(), origin: origin, filePath: filePath, lineNumber: lineNumber);

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
                Logger.LogErrorSource(ex.ToString(), origin: origin, filePath: filePath, lineNumber: lineNumber);

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
                Logger.LogErrorSource(ex.ToString(), origin: origin, filePath: filePath, lineNumber: lineNumber);

                throw;
            }
        }
    }
}