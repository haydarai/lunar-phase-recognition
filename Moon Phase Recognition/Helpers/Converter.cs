using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace Moon_Phase_Recognition.Helpers
{
    public static class Converter
    {
        public static BitmapImage ConvertBitmapToBitmapImage(Bitmap Bitmap)
        {
            MemoryStream ms = new MemoryStream();
            Bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();
            return bi;
        }

        public static Bitmap ConvertColorToGrayscale(Bitmap bitmap)
        {
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    int gray = (int)((color.R * 0.3) + (color.G * 0.59) + (color.B * 0.11));
                    Color newColor = Color.FromArgb(gray, gray, gray);
                    bitmap.SetPixel(i, j, newColor);
                }
            }
            return bitmap;
        }
    }
}
