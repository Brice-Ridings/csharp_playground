using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace playground
{
    public class ConcurrentTasksWithSemaphoreExample
    {
        static public async Task run()
        {
            List<int> timeList = new List<int>(){5000, 3000, 1000, 2000, 6000, 7000, 4000, 8000};

            await timeList.ForEachAsyncSemaphore(2, async time =>
            {
//                int currentThreads = Process.GetCurrentProcess().Threads.Count;
//                Console.WriteLine($"CURRENT THREADS: {currentThreads}");
                Console.WriteLine($"Starting {time/1000} seconds");
                await Task.Delay(time);
                Console.WriteLine($"{time/1000} seconds passed");
            });

            }
        }
    

    public static class extensions
    {
        public static async Task ForEachAsyncSemaphore<T>(this IEnumerable<T> source,
            int degreeOfParallelism, Func<T, Task> body)
        {
            var tasks = new List<Task>();
            using (var throttler = new SemaphoreSlim(degreeOfParallelism))
            {
                foreach (var element in source)
                {
                    
                    // block thread if MAX degreeOfParallelism is met
                    await throttler.WaitAsync();
                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            await body(element);
                        }
                        finally
                        {
                            throttler.Release();
                        }
                    }));
                }
                await Task.WhenAll(tasks);
            }
        }
    }
}