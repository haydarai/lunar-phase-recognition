using System.Drawing;

namespace Moon_Phase_Recognition.Helpers
{
    public static class ValueFinder
    {
        public static int[] GetMaxMin(Bitmap bitmap)
        {
            int[] maxAndMin = new int[] { 0, 255 };
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int g = color.G;
                    if (maxAndMin[0] < g)
                    {
                        maxAndMin[0] = g;
                    }
                    if (maxAndMin[1] > g)
                    {
                        maxAndMin[1] = g;
                    }
                }
            }
            return maxAndMin;
        }

        public static int[] GetCoordinate(Bitmap bitmap)
        {
            int[] coordinate = new int[] { bitmap.Width, bitmap.Height, 0, 0 };
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int g = color.G;
                    if (g == 255)
                    {
                        if (coordinate[0] > x)
                        {
                            coordinate[0] = x;
                        }
                        if (coordinate[1] > y)
                        {
                            coordinate[1] = y;
                        }
                        if (coordinate[2] < x)
                        {
                            coordinate[2] = x;
                        }
                        if (coordinate[3] < y)
                        {
                            coordinate[3] = y;
                        }
                    }
                }
            }
            return coordinate;
        }

        public static int PercentageOfWhite(Bitmap bitmap)
        {
            int[] numbers = new int[] { 0, 0 };
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    if (bitmap.GetPixel(x, y).G == 255)
                    {
                        numbers[0]++;
                    }
                    else
                    {
                        numbers[1]++;
                    }
                }
            }
            return (numbers[0] * 100) / (bitmap.Width * bitmap.Height);
        }
    }
}
