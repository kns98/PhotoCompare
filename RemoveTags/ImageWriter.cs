using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;

namespace PhotoCompare
{
    public interface ImageWriter
    {
        void WriteImage(Stream s, FileInfo f);
    }

    public class BitmapImageWriter : ImageWriter
    {
        public void WriteImage(Stream s, FileInfo f)
        {
            using var b = new Bitmap(f.FullName);
            b.Save(s, ImageFormat.Bmp);
        }
    }

    public class MagickImageWriter : ImageWriter
    {
        public void WriteImage(Stream s, FileInfo f)
        {
            using var bmp1 = new MagickImage(f.FullName);
            bmp1.Write(s, MagickFormat.Bmp);
        }
    }
}
