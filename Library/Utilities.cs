using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Utilities
    {
        private static List<string> _knownTypes;

        private static Dictionary<string, string> _types;

        [DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
        private static extern int FindMimeFromData(IntPtr pBC,
                                                   [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
                                                   [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1,
                                                       SizeParamIndex = 3)] byte[] pBuffer,
                                                   int cbSize,
                                                   [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed,
                                                   int dwMimeFlags,
                                                   out IntPtr ppwzMimeOut,
                                                   int dwReserved);

        public static string GetContentType(string fileName)
        {
            if (_knownTypes == null || _types == null)
                InitializeMimeTypeLists();

            string contentType = "";
            string extension = Path.GetExtension(fileName).Replace(".", "").ToLower();
            _types.TryGetValue(extension, out contentType);
            if (string.IsNullOrEmpty(contentType) || _knownTypes.Contains(contentType))
            {
                string header = ScanFileForMimeType(fileName);
                if (header == "application/octet-stream" || string.IsNullOrEmpty(contentType))
                    contentType = header;
            }
            return contentType;
        }

        private static string ScanFileForMimeType(string filename)
        {
            string mime = "";
            try
            {
                IntPtr mimeout;
                var max = (int)new FileInfo(filename).Length;
                if (max > 4096) max = 4096;
                var buf = new byte[max];
                using (var fs = File.OpenRead(filename))
                    fs.Read(buf, 0, max);

                int result = FindMimeFromData(IntPtr.Zero, filename, buf, max, null, 0, out mimeout, 0);

                if (result != 0)
                    throw Marshal.GetExceptionForHR(result);
                mime = Marshal.PtrToStringUni(mimeout);
                if (string.IsNullOrWhiteSpace(mime))
                    mime = "application/octet-stream";
                Marshal.FreeCoTaskMem(mimeout);
            }
            catch
            {
                mime = "application/octet-stream";
            }
            return mime;
        }

        private static void InitializeMimeTypeLists()
        {
            _knownTypes = new[]
            {
                "application/pdf",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "application/msword"
            }.ToList();

            _types = new Dictionary<string, string>
            {
                {"pdf", "application/pdf"},
                {"docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                {"doc", "application/msword"},
            };
        }
    }
}
