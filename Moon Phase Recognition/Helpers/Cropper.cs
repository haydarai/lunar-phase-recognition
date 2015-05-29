using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon_Phase_Recognition.Helpers
{
    public static class Cropper
    {
        public static Bitmap SquareCrop(Bitmap OriginalBitmap, Rectangle CropSection)
        {
            Bitmap CroppedBitmap = new Bitmap(CropSection.Width, CropSection.Height);
            Graphics g = Graphics.FromImage(CroppedBitmap);
            g.DrawImage(OriginalBitmap, 0, 0, CropSection, GraphicsUnit.Pixel);
            return CroppedBitmap;
        }
    }
}
