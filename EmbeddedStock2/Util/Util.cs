using FreeImageAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedStock2.Util
{
    public static class Util
    {
        public static byte[] ImageToByteArray(string path, FREE_IMAGE_FORMAT format)
        {
            using (var image = FreeImageBitmap.FromFile(path))
            {
                using (var m = new MemoryStream())
                {
                    image.Save(m, format);
                    return m.ToArray();
                }
            }
        }

        public static byte[] ThumbNailByteArray(string path, FREE_IMAGE_FORMAT format)
        {
            using (var image = FreeImageBitmap.FromFile(path))
            {
                var newImage = image.GetThumbnailImage(1000,true);
                using (var m = new MemoryStream())
                {
                    newImage.Save(m, format);
                    return m.ToArray();
                }
            }
        }
    }
}
