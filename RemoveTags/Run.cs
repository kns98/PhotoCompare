using LoggingDemo;
using LoggingDemo.Helpers;

namespace PhotoCompare
{
    internal class Runner
    {
        public static void Run_Main()
        {
            typeof(Program).Info("Please enter start folder: ");
            var startFolder = Console.ReadLine();

            if (startFolder != null)
            {
                PhotoCompare.Run(startFolder);
            }

            typeof(Program).Info("Press any key to exit");
            Console.ReadKey();
        }
    }
}
