using System.Security.Cryptography;

namespace PhotoCompare;

internal class Hasher
{
    public static string GetHash(Stream stream)
    {
        string mHash;
        using (var md5 = MD5.Create())
        {
            mHash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
            return mHash;
        }
    }
}