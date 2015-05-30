using System.Drawing;

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
