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

        public static FREE_IMAGE_FORMAT FindImageFormat(string contentType)
        {
            switch (contentType)
            {
                case "image/jpeg":
                    return FREE_IMAGE_FORMAT.FIF_JPEG;
                case "image/gif":
                    return FREE_IMAGE_FORMAT.FIF_GIF;
                case "image/png":
                    return FREE_IMAGE_FORMAT.FIF_PNG;
                case "image/bmp":
                    return FREE_IMAGE_FORMAT.FIF_BMP;
                default:
                    throw new Exception("Unknown image mimetype");
            }
        }
    }
}
