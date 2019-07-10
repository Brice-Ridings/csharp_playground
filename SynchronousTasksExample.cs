using System;
using System.Threading.Tasks;

namespace playground
{
    public class SynchronousTasksExample
    {
        static public async Task run()
        {
            // Tasks must be set up to 
            Task firstThing = doTheFirstThing();
            Task secondThing = doTheSecondThing();

            firstThing.Wait();
            secondThing.Wait();
        }

        static async Task doTheFirstThing()
        {
            Console.WriteLine("Doing the first thing");
            Task.Delay(6000).Wait();
            Console.WriteLine("Finished the first thing");
        }

        static async Task doTheSecondThing()
        {
            Console.WriteLine("Doing the second thing");
            Task.Delay(5000).Wait();
            Console.WriteLine("Finished the second thing");
        }
    }
    /**
     * Starting Code
     *   Doing the first thing
     *   Finished the first thing
     *   Doing the second thing
     *   Finished the second thing
     */
}

