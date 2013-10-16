using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class FileAttribute : Attribute
    {

        public string Extension { get; set; }
        public string Mime { get; set; }
    }
}
