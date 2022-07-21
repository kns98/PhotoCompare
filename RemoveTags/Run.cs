using LoggingDemo;
using LoggingDemo.Helpers;

namespace PhotoCompare
{
    internal class Runner
    {
        public static void Run_Main()
        {
            typeof(PhotoLogger).Info("Please enter start folder: ");
            var startFolder = Console.ReadLine();

            if (startFolder != null)
            {
                PhotoCompare.Run(startFolder);
            }

            typeof(PhotoLogger).Info("Press any key to exit");
            Console.ReadKey();
        }
    }
}
