using System.Diagnostics;
using LoggingDemo;
using LoggingDemo.Helpers;

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

            typeof(PhotoLogger).Info("Press any key to exit");
            Console.ReadKey();
        }
    }
}
