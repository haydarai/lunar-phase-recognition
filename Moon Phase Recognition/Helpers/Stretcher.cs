using System.Drawing;

namespace Moon_Phase_Recognition.Helpers
{
    public static class Stretcher
    {
        public static Bitmap ApplyContrastStretch(Bitmap OriginalBitmap)
        {
            Bitmap EditedBitmap = new Bitmap(OriginalBitmap.Width, OriginalBitmap.Height);
            int[] maxAndMin = ValueFinder.GetMaxMin(OriginalBitmap);
            for (int x = 0; x < OriginalBitmap.Width; x++)
            {
                for (int y = 0; y < OriginalBitmap.Height; y++)
                {
                    Color OriginalColor = OriginalBitmap.GetPixel(x, y);
                    int g = ((OriginalColor.G - maxAndMin[1]) * 255 / (maxAndMin[0] - maxAndMin[1]));
                    Color EditedColor = Color.FromArgb(g, g, g);
                    EditedBitmap.SetPixel(x, y, EditedColor);
                }
            }
            return EditedBitmap;
        }
    }
}
