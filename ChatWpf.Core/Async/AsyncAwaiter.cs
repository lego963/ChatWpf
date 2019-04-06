using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatWpf.Core.Logging.Core;

namespace ChatWpf.Core.Async
{
    public static class AsyncAwaiter
    {
        private static SemaphoreSlim SelfLock = new SemaphoreSlim(1, 1);

        private static Dictionary<string, SemaphoreSlim> Semaphores = new Dictionary<string, SemaphoreSlim>();

        public static async Task<T> AwaitResultAsync<T>(string key, Func<Task<T>> task, int maxAccessCount = 1)
        {
            await SelfLock.WaitAsync();

            try
            {
                if (!Semaphores.ContainsKey(key))
                    Semaphores.Add(key, new SemaphoreSlim(maxAccessCount, maxAccessCount));
            }
            finally
            {
                SelfLock.Release();
            }

            var semaphore = Semaphores[key];

            await semaphore.WaitAsync();

            try
            {
                return await task();
            }
            finally
            {
                semaphore.Release();
            }
        }

        public static async System.Threading.Tasks.Task AwaitAsync(string key, Func<System.Threading.Tasks.Task> task, int maxAccessCount = 1)
        {
            await SelfLock.WaitAsync();

            try
            {
                if (!Semaphores.ContainsKey(key))
                    Semaphores.Add(key, new SemaphoreSlim(maxAccessCount, maxAccessCount));
            }
            finally
            {
                SelfLock.Release();
            }

            var semaphore = Semaphores[key];

            await semaphore.WaitAsync();

            try
            {
                await task();
            }
            catch (Exception ex)
            {
                var error = ex.Message;

                IoC.Base.IoC.Logger.Log($"Crash in {nameof(AwaitAsync)}. {ex.Message}", LogLevel.Debug);

                Debugger.Break();

                throw;
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
