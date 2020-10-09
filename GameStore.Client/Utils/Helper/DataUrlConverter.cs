using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace GameStore.Client.Utils.Helper
{
    public static class DataUrlConverter
    {
        public static Image ToImage(string dataUrl)
        {
            var base64Data = Regex.Match(dataUrl, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var binData = System.Convert.FromBase64String(base64Data);

            using (var stream = new MemoryStream(binData))
            {
                var image = new Bitmap(stream);
                return image;
            }
        }

    }
}