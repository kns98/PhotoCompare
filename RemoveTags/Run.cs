using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingDemo;
using LoggingDemo.Helpers;
using NLog;

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
