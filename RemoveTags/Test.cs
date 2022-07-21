using System.Diagnostics;
using PhotoCompare.Logging;

namespace PhotoCompare
{
    class Test
    {
        public static void Main_Test()
        {
            Program._dupkey = "-BIBI";
            Program.Threading = false;


            var startFolder1 =
                Path.Combine(
                    Path.GetDirectoryName(
                        Process.GetCurrentProcess().MainModule.FileName
                    ), @"..\..\..\Tests\Different");

            Program.Run(startFolder1);


            var startFolder2 =
                Path.Combine(
                    Path.GetDirectoryName(
                        Process.GetCurrentProcess().MainModule.FileName
                    ), @"..\..\..\Tests\Same");

            Program.Run(startFolder2);

            typeof(Log).Info("Press any key to exit");
            Console.ReadKey();
        }
    }
}
