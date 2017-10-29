using FreeImageAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedStock2.Util
{
    public class Util
    {
        public byte[] ImageToByteArray(string path)
        {
            using (var image = FreeImageBitmap.FromFile(path))
            {
                
            }
            throw new NotImplementedException();
        }

        public void CreateImageFromPath(string path)
        {
            throw new NotImplementedException();
        }
    }
}
