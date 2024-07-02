using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
namespace MosqueMate.Helper
{
    public class BitmapHelper
    {
        public static BitmapImage ConvertBitmapToBitmapImage(Bitmap bitmap)
        {
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                // Save the Bitmap to a memory stream using a supported format (e.g., PNG)
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

                // Create a new BitmapImage and set its stream source
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new System.IO.MemoryStream(memoryStream.ToArray());
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }
        public static void CaptureScreenshot(Window window)
        {
            // Create a RenderTargetBitmap object
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)window.ActualWidth, (int)window.ActualHeight, 96, 96, PixelFormats.Pbgra32);

            // Render the window's content to the bitmap
            renderTargetBitmap.Render(window);

            // Create a PngBitmapEncoder to save the image
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            // Show a save file dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Image|*.png",
                Title = "Save Screenshot"
            };

            // If the user clicks "Save"
            if (saveFileDialog.ShowDialog() == true)
            {
                // Save the rendered bitmap to the file
                using (var fileStream = System.IO.File.Create(saveFileDialog.FileName))
                {
                    pngImage.Save(fileStream);
                }
                MessageBox.Show("Screenshot saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public static void CaptureScreenshot(Window[] windows)
        {
            // Calculate the combined width and height
            int combinedWidth = (int)windows.Max(w => w.ActualWidth);
            int combinedHeight = (int)windows.Sum(w => w.ActualHeight);

            // Create a RenderTargetBitmap to hold the combined image
            RenderTargetBitmap combinedBitmap = new RenderTargetBitmap(combinedWidth, combinedHeight, 96, 96, PixelFormats.Pbgra32);

            // Render each window and combine the images
            int currentHeight = 0;
            foreach (var window in windows)
            {
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)window.ActualWidth, (int)window.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                renderTargetBitmap.Render(window);

                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext dc = dv.RenderOpen())
                {
                    dc.DrawImage(renderTargetBitmap, new Rect(0, currentHeight, window.ActualWidth, window.ActualHeight));
                }
                combinedBitmap.Render(dv);

                currentHeight += (int)window.ActualHeight;
            }

            // Create a PngBitmapEncoder to save the image
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(combinedBitmap));

            // Show a save file dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Image|*.png",
                Title = "Save Screenshot"
            };

            // If the user clicks "Save"
            if (saveFileDialog.ShowDialog() == true)
            {
                // Save the rendered bitmap to the file
                using (var fileStream = System.IO.File.Create(saveFileDialog.FileName))
                {
                    pngImage.Save(fileStream);
                }
                MessageBox.Show("Screenshot saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
