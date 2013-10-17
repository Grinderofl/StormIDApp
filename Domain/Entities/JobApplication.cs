using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class JobApplication
    {
        public byte[] CV { get; set; }
        public string FileName { get; set; }
        public string Email { get; set; }
        public string Cover { get; set; }
        public string Name { get; set; }
    }
}
