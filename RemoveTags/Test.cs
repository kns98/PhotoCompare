using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingDemo;
using LoggingDemo.Helpers;
using NLog;

namespace PhotoCompare
{
    class Test
    {
        public static void Main_Test()
        {
            PhotoCompare._dupkey = "-BIBI";
            PhotoCompare.Threading = false;


            var startFolder1 =
                Path.Combine(
                    Path.GetDirectoryName(
                        Process.GetCurrentProcess().MainModule.FileName
                    ), @"..\..\..\Tests\Different");

            PhotoCompare.Run(startFolder1);


            var startFolder2 =
                Path.Combine(
                    Path.GetDirectoryName(
                        Process.GetCurrentProcess().MainModule.FileName
                    ), @"..\..\..\Tests\Same");

            PhotoCompare.Run(startFolder2);

            typeof(Program).Info("Press any key to exit");
            Console.ReadKey();
        }
    }
}
