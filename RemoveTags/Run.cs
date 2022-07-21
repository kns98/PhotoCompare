using PhotoCompare.Logging;

namespace PhotoCompare.Logging
{
    internal class Runner
    {
        public static void Run_Main()
        {
            typeof(Log).Info("Please enter start folder: ");
            var startFolder = Console.ReadLine();

            if (startFolder != null)
            {
                Program.Run(startFolder);
            }

            typeof(Log).Info("Press any key to exit");
            Console.ReadKey();
        }
    }
}
