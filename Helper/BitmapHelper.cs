using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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
    }
}
