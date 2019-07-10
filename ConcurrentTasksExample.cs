using System;
using System.Threading.Tasks;

namespace playground
{
    public static class ConcurrentTasksExample
    {
        static public async Task run()
        {
            // Tasks must be set up to 
            Task firstThing = doTheFirstThing();
            Task secondThing = doTheSecondThing();

            await firstThing;
            await secondThing;
        }

        static async Task doTheFirstThing()
        {
            Console.WriteLine("Doing the first thing");
            await Task.Delay(6000);
            Console.WriteLine("Finished the first thing");
        }

        static async Task doTheSecondThing()
        {
            Console.WriteLine("Doing the second thing");
            await Task.Delay(5000);
            Console.WriteLine("Finished the second thing");
        }
    }
    /**
     * Starting Code
     *   Doing the first thing
     *   Doing the second thing
     *   Finished the second thing
     *   Finished the first thing
     */
}