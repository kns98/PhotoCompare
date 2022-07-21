using PhotoCompare.Logging;

namespace PhotoCompare.Logging;

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
        var ret = Program.CheckEqual(_fi, _fi2);

        if (ret != null)
        {
            var b = (bool)ret;
            if (b)
            {
                typeof(Log).Info( _fi.FullName + " is the same as " + _fi2.FullName);

                if (Program.Dupkey != null && _fi.Name.Contains(Program.Dupkey))
                {
                    typeof(Log).Info("Deleting file: " + _fi.Name);
                    //File.Delete(_fi.FullName);
                }
                
                else if (Program.Dupkey != null && _fi2.Name.Contains(Program.Dupkey))
                {
                    typeof(Log).Info(   "Deleting file: " + _fi2.Name);
                    //File.Delete(_fi2.FullName);
                }
                else
                {
                    typeof(Log).Info(   "No dupkey so no deltions.");
                }
            }
            else
            {
                typeof(Log).Info(_fi.FullName + " is not the same as " + _fi2.FullName);
            }
        }
    }
}

