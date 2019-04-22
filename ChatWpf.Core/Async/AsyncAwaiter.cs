using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Dna;

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

            // NOTE: We never remove semaphores after creating them, so this will never be null
            var semaphore = Semaphores[key];

            // Await this semaphore
            await semaphore.WaitAsync();

            try
            {
                // Perform the job
                return await task();
            }
            finally
            {
                // Release the semaphore
                semaphore.Release();
            }
        }

        public static async System.Threading.Tasks.Task AwaitAsync(string key, Func<System.Threading.Tasks.Task> task, int maxAccessCount = 1)
        {
            await SelfLock.WaitAsync();

            try
            {
                // Create semaphore if it doesn't already exist
                if (!Semaphores.ContainsKey(key))
                    Semaphores.Add(key, new SemaphoreSlim(maxAccessCount, maxAccessCount));
            }
            finally
            {
                SelfLock.Release();
            }

            // NOTE: We never remove semaphores after creating them, so this will never be null
            var semaphore = Semaphores[key];

            // Await this semaphore
            await semaphore.WaitAsync();

            try
            {
                // Perform the job
                await task();
            }
            catch (Exception ex)
            {
                var error = ex.Message;

                FrameworkDI.Logger.LogDebugSource($"Crash in {nameof(AwaitAsync)}. {ex.Message}");

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
