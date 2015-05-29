using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon_Phase_Recognition.Helpers
{
    public static class Thresholder
    {
        public static Bitmap ApplyOtsuThreshold(Bitmap OriginalBitmap)
        {
            double[] histogram = new double[256];
            double[] normalizedhistogram = new double[256];

            for (int i = 0; i < histogram.Length; i++)
            {
                histogram[i] = 0;
                normalizedhistogram[i] = 0;
            }

            for (int i = 1; i < OriginalBitmap.Width; i++)
            {
                for (int j = 1; j < OriginalBitmap.Height; j++)
                {
                    Color tempColor = OriginalBitmap.GetPixel(i, j);
                    int temp = tempColor.R;
                    histogram[temp]++;
                }
            }
            double cumulative = histogram.Sum();
            for (int i = 1; i < histogram.Length; i++)
            {
                normalizedhistogram[i] = histogram[i] / cumulative;
            }
            double mG = 0;
            for (int i = 0; i < normalizedhistogram.Length; i++)
            {
                mG = mG + (i * normalizedhistogram[i]);
            }
            List<double> matrixBCV = new List<double>();
            List<double> listnormalizedhistogram = normalizedhistogram.ToList();
            for (int k = 1; k < normalizedhistogram.Length; k++)
            {
                double P1 = normalizedhistogram.Take(k).Sum();
                double P2 = normalizedhistogram.Skip(k + 1).Take(normalizedhistogram.Length - (k + 1)).Sum();
                double smallP1 = 0;
                double smallP2 = 0;
                for (int i = 1; i < k; i++)
                {
                    smallP1 = smallP1 + (i * normalizedhistogram[i]);
                }
                double m1 = smallP1 / P1;
                for (int i = k + 1; i < normalizedhistogram.Length; i++)
                {
                    smallP2 = smallP2 + (i * normalizedhistogram[i]);
                }
                double m2 = smallP2 / P2;
                double BCV = P1 * Math.Pow((m1 - mG), 2) + P2 * Math.Pow((m2 - mG), 2);
                matrixBCV.Add(BCV);
            }
            int optimal = matrixBCV.IndexOf(matrixBCV.Max());
            Bitmap EditedBitmap = new Bitmap(OriginalBitmap.Width, OriginalBitmap.Height);
            for (int i = 0; i < EditedBitmap.Width; i++)
            {
                for (int j = 0; j < EditedBitmap.Height; j++)
                {
                    if (OriginalBitmap.GetPixel(i, j).G > optimal)
                    {
                        EditedBitmap.SetPixel(i, j, Color.FromArgb(255, 255, 255, 255));
                    }
                    else
                    {
                        EditedBitmap.SetPixel(i, j, Color.FromArgb(255, 0, 0, 0));
                    }
                }
            }
            return EditedBitmap;
        }
    }
}
