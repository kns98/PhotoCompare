using PhotoCompare.Logging;

namespace PhotoCompare.Logging;

internal class Program
{

    private static readonly Dictionary<(string, string), object> Lockdict = new();
    public static string? _dupkey = "";
    private static readonly object KeyLock = new();

    public static bool testing = false;

    public static bool Threading { get; set; }

    public static string? Dupkey
    {
        get
        {
            lock (KeyLock)
            {
                if (_dupkey == "")
                {
                    typeof(Log).Info("Please enter a dup key:");
                    _dupkey = Console.ReadLine();
                }

                return _dupkey;
            }
        }
        set
        {
            lock (KeyLock)
            {
                _dupkey = value;
            }
        }
    }

    public static void Main()
    {
         PhotoCompare.Logging.LogExt.Initialize();

        if (testing)
        {
            Test.Main_Test();
        }
        else
        {
            Runner.Run_Main();
        }
    }


    public static void Run(string startFolder)
    {
        var dir = new DirectoryInfo(startFolder);

        IEnumerable<FileInfo> fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);

        IEnumerable<FileInfo> fileQuery =
            from file in fileList
            where Dupkey != null && file.Name.Contains(Dupkey)
            orderby file.Name
            select file;

        var loopLock = new object();

        foreach (var fi in fileQuery)
        {
            lock (loopLock)
            {
                typeof(Log).Info(fi.FullName);

                File.SetAttributes(fi.FullName,
                    File.GetAttributes(fi.FullName) & ~FileAttributes.ReadOnly);

                var parts = fi.Name.Split(Dupkey);
                var ext = fi.Extension;

                if (fi.DirectoryName != null)
                {
                    var newName = Path.Combine(fi.DirectoryName, parts[0] + ext);

                    var fi2 = new FileInfo(newName);

                    if (Threading)
                    {
                        ThreadPool.QueueUserWorkItem(Cleaner.Action(new Cleaner(fi, fi2)));
                    }
                    else
                    {
                        new Cleaner(fi, fi2).Run();
                    }
                }
            }
        }
    }


    public static bool? CheckEqual(FileInfo f1, FileInfo f2)
    {
        var innerlock = new object();

        lock (Lockdict)
        {
            if (Lockdict.ContainsKey((f1.FullName, f2.FullName)))
            {
                innerlock = Lockdict[(f1.FullName, f2.FullName)];
            }
            else
            {
                Lockdict.Add((f1.FullName, f2.FullName), innerlock);
            }
        }

        lock (innerlock)
        {
            try
            {
                if (!BoundaryConditions(f1, f2))
                {
                    return false;
                }

                return ImageIsTheSame(f1, f2);
            }
            catch (NullReferenceException ex)
            {
                typeof(Log).Error(ex);
                return null;
            }
            catch (Exception ex)
            {
                typeof(Log).Error( ex);
                return null;
            }
        }
    }

    private static bool? ImageIsTheSame(FileInfo f1, FileInfo f2)
    {
        ImageWriter w = new BitmapImageWriter();
        using var ms1 = new MemoryStream();
        using var ms2 = new MemoryStream();

        w.WriteImage(ms1, f1);
        w.WriteImage(ms2, f2);

        var hash1 = Hasher.GetHash(ms1);
        var hash2 = Hasher.GetHash(ms2);

        return hash1 == hash2;
    }

    private static bool BoundaryConditions(FileInfo f1, FileInfo f2)
    {
        if (!f1.Exists)
        {
            return false;
        }

        if (!f2.Exists)
        {
            return false;
        }

        if (f1.Length == 0)
        {
            return false;
        }

        if (f2.Length == 0)
        {
            return false;
        }

        return true;
    }
}