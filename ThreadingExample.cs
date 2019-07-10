using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace playground
{
    public static class ThreadingExample
    {
        static public async void run()
        {
            List<int> timeList = new List<int>(){5000, 3000, 7000, 2000};

            foreach(int time in timeList)
            {
                Task.Delay(time).Wait();
                Console.WriteLine($"{time/1000} seconds passed");
            }

            /**
             * Starting Code
             *    5 seconds passed
             *    3 seconds passed
             *    7 seconds passed
             *    2 seconds passed
             */
            
            
            // Parallel class is an easy way create multiple threads from a list. 
            Console.WriteLine("\n Execute via threads \n");
            Parallel.ForEach(timeList, time =>
            {
                Task.Delay(time).Wait();
                Console.WriteLine($"{time/1000} seconds passed");
            });
            
            /**
             *  Execute via threads 
             *    2 seconds passed
             *    3 seconds passed
             *    5 seconds passed
             *    7 seconds passed
             */
            
            Console.WriteLine("\n Execute via threads with max parallelism of 3");
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 3
            };
            Parallel.ForEach(timeList, options, time =>
            {
                Console.WriteLine($"Starting {time/1000} second pass");
                Task.Delay(time).Wait();
                Console.WriteLine($"{time/1000} seconds passed");
            });
            
            /**
             *  Execute via threads with max parallelism of 3
             *    Starting 5 second pass
             *    Starting 3 second pass
             *    Starting 7 second pass
             *    3 seconds passed
             *    Starting 2 second pass
             *    5 seconds passed
             *    2 seconds passed
             *    7 seconds passed
             */
        }
        
        
    }
}