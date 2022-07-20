using LoggingDemo;
using LoggingDemo.Helpers;

namespace PhotoCompare;

internal class Counter
{
    private  readonly object CountLock = new();
    private  int _count;
    private string name;

    public Counter(string name)
    {
        this.name = name;
    }
    private int Count()
    {
        lock (CountLock)
        {
            _count++;
            typeof(Program).Info(name + " Count: " + _count);
            return _count;
        }
    }
}