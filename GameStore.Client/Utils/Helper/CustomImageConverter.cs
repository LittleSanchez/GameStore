using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace GameStore.Client.Utils.Helper
{
    public static class CustomImageConverter
    {
        public static Bitmap ConvertImage(Image oldImage, int width, int height)
        {
            if (oldImage.Width < width)
            {
                height = Convert.ToInt32(Math.Floor(height / Convert.ToDouble(width) * oldImage.Width));
                width = oldImage.Width;
            }
            if (oldImage.Height < height)
            {
                width = Convert.ToInt32(Math.Floor(width / Convert.ToDouble(height) * oldImage.Height));
                height = oldImage.Height;
            }

            var newImage = new Bitmap(width, height);
            using (Graphics context = Graphics.FromImage(newImage))
            {
                context.DrawImage(oldImage, 0, 0, width, height);
            }

            return new Bitmap(newImage);
        }
    }
}