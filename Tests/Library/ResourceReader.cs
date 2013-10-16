using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Library
{
    public static class ResourceReader
    {
        public static byte[] GetResourceAsArray(string resource)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(resource);
            using (var ms = new MemoryStream())
            {
                if (stream != null) stream.CopyTo(ms);
                return ms.ToArray();
            }
        }


    }
}
