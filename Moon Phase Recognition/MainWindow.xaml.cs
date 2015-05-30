using Microsoft.Win32;
using Moon_Phase_Recognition.Helpers;
using System.Drawing;
using System.Windows;

namespace Moon_Phase_Recognition
{
    public partial class MainWindow : Window
    {
        private string filename;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                filename = ofd.FileName;
                Bitmap OriginalBitmap = new Bitmap(ofd.FileName);
                OriginalImage.Source = Converter.ConvertBitmapToBitmapImage(OriginalBitmap);
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (OriginalImage.Source == null)
            {
                MessageBox.Show("Please browse an image first");
                return;
            }
            else
            {
                Bitmap OriginalBitmap = new Bitmap(filename);
                Bitmap EditedBitmap = new Bitmap(OriginalBitmap.Width, OriginalBitmap.Height);

                // Color to Grayscale Color and Contrast Stretch
                EditedBitmap = Converter.ConvertColorToGrayscale(OriginalBitmap);
                EditedBitmap = Stretcher.ApplyContrastStretch(EditedBitmap);
                StretchedImage.Source = Converter.ConvertBitmapToBitmapImage(EditedBitmap);

                // Otsu's Optimal Threshold
                EditedBitmap = Thresholder.ApplyOtsuThreshold(EditedBitmap);
                ThresholdedImage.Source = Converter.ConvertBitmapToBitmapImage(EditedBitmap);

                // Crop
                int[] coordinate = ValueFinder.GetCoordinate(EditedBitmap);
                int width = coordinate[2] - coordinate[0];
                int height = coordinate[3] - coordinate[1];
                int side;
                if (width > height)
                {
                    side = width;
                }
                else
                {
                    side = height;
                }
                System.Drawing.Rectangle CropSection = new System.Drawing.Rectangle(new System.Drawing.Point(coordinate[0], coordinate[1]), new System.Drawing.Size(side, side));
                EditedBitmap = Cropper.SquareCrop(EditedBitmap, CropSection);
                CroppedImage.Source = Converter.ConvertBitmapToBitmapImage(EditedBitmap);

                // Classification
                int whitePercentage = ValueFinder.PercentageOfWhite(EditedBitmap);
                if (whitePercentage > 75)
                {
                    PhaseBlock.Text = "Full Moon";
                }
                else if (whitePercentage > 45)
                {
                    PhaseBlock.Text = "Waxing Gibbous / Waning Gibbous";
                }
                else if (whitePercentage > 25)
                {
                    PhaseBlock.Text = "First Quarter / Third Quarter";
                }
                else if (whitePercentage > 6)
                {
                    PhaseBlock.Text = "Waxing Crescent / Waning Crescent";
                }
                else
                {
                    PhaseBlock.Text = "New Moon";
                }
            }
        }
    }
}
