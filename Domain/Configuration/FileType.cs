using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Library.Attributes;

namespace Domain.Configuration
{
    public enum FileTypes
    {
        [File(Extension = "pdf", Mime = "application/pdf")]
        PDF,

        [File(Extension = "docx", Mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
        Docx,

        [File(Extension = "doc", Mime = "application/msword")]
        Doc
    }
}
