using System;
using System.Threading.Tasks;

namespace playground
{
    /**
     * Example of concurrent operations with tasks
     */
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Code");
            //ConcurrentTasksExample.run().Wait();
            //SynchronousTasksExample.run().Wait();
            ThreadingExample.run();
        }
    }
}
