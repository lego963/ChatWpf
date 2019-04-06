using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ChatWpf.Core.IoC.Interfaces
{
    public interface ITaskManager
    {
        System.Threading.Tasks.Task Run(Func<System.Threading.Tasks.Task> function, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0);

        Task<TResult> Run<TResult>(Func<Task<TResult>> function, CancellationToken cancellationToken, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0);

        Task<TResult> Run<TResult>(Func<Task<TResult>> function, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0);

        Task<TResult> Run<TResult>(Func<TResult> function, CancellationToken cancellationToken, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0);

        Task<TResult> Run<TResult>(Func<TResult> function, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0);

        System.Threading.Tasks.Task Run(Func<System.Threading.Tasks.Task> function, CancellationToken cancellationToken, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0);

        System.Threading.Tasks.Task Run(Action action, CancellationToken cancellationToken, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0);

        System.Threading.Tasks.Task Run(Action action, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0);
    }
}