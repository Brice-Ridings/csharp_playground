using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace playground
{
    public class ConcurrentProcessingTimeSamples
    {
        static public async Task run()
        {
            List<int> timeList = new List<int>(){5000, 3000, 1000, 2000, 6000, 7000, 4000, 8000};

            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach(int time in timeList)
            {
                Task.Delay(time).Wait();
                Console.WriteLine($"{time/1000} seconds passed");
            }
            stopwatch.Stop();
            Console.WriteLine($"General forEach execution time: {stopwatch.ElapsedMilliseconds}");
            
            Stopwatch asyncStopwatch = Stopwatch.StartNew();
            List<Task> taskList = new List<Task>();
            foreach(int time in timeList)
            {
                taskList.Add(Task.Delay(time));
                Console.WriteLine($"{time/1000} seconds passed");
            }

            await Task.WhenAll(taskList);
            asyncStopwatch.Stop();
            Console.WriteLine($"General forEach execution time: {asyncStopwatch.ElapsedMilliseconds}");
            
            Stopwatch threadStopwatch = Stopwatch.StartNew(); 
            // Parallel class is an easy way create multiple threads from a list. 
            Console.WriteLine("\n Execute via threads \n");
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 4
            };
            Parallel.ForEach(timeList, options, async time =>
            {
                Task.Delay(time).Wait();
                Task.Delay(time / 2).Wait();
                Console.WriteLine($"{time/1000} seconds passed");
            });
            threadStopwatch.Stop();
            Console.WriteLine($"Parallel class processing time: {threadStopwatch.ElapsedMilliseconds}");

            Stopwatch semStopwatch = Stopwatch.StartNew();
            await timeList.ForEachAsyncSemaphore(4, async time =>
            {
//                int currentThreads = Process.GetCurrentProcess().Threads.Count;
//                Console.WriteLine($"CURRENT THREADS: {currentThreads}");
                await Task.Delay(time);
                await Task.Delay(time / 2);
                Console.WriteLine($"{time/1000} seconds passed");
            });
            semStopwatch.Stop();
            Console.WriteLine($"Async Semaphore execution time {semStopwatch.ElapsedMilliseconds}");
        }
    }
}