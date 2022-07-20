using LoggingDemo;
using LoggingDemo.Helpers;

namespace PhotoCompare;
using NLog;


public class Cleaner
{
    private readonly FileInfo _fi;
    private readonly FileInfo _fi2;

    public Cleaner(FileInfo f1, FileInfo f2)
    {
        _fi = f1;
        _fi2 = f2;
    }

    public static WaitCallback Action(object stateInfo)
    {
        var me = (Cleaner)stateInfo;
        return _ => me.Run();
    }

    public void Run()
    {
        var ret = PhotoCompare.CheckEqual(_fi, _fi2);

        if (ret != null)
        {
            var b = (bool)ret;
            if (b)
            {
                typeof(Program).Info( _fi.FullName + " is the same as " + _fi2.FullName);

                if (PhotoCompare.Dupkey != null && _fi.Name.Contains(PhotoCompare.Dupkey))
                {
                    typeof(Program).Info("Deleting file: " + _fi.Name);
                    File.Delete(_fi.FullName);
                }
                
                else if (PhotoCompare.Dupkey != null && _fi2.Name.Contains(PhotoCompare.Dupkey))
                {
                    typeof(Program).Info(   "Deleting file: " + _fi2.Name);
                    File.Delete(_fi2.FullName);
                }
                else
                {
                    typeof(Program).Info(   "No dupkey so no deltions.");
                }
            }
            else
            {
                typeof(Program).Info(_fi.FullName + " is not the same as " + _fi2.FullName);
            }
        }
    }
}

