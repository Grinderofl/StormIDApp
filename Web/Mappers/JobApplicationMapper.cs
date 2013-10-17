using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Domain.Entities;
using Web.Models;

namespace Web.Mappers
{
    public static class JobApplicationMapper
    {
        public static JobApplication Map(JobApplicationModel model)
        {
            var result = new JobApplication()
            {
                Cover = model.Cover,
                Email = model.Email,
                Name = model.Name
            };
            if (model.CVFile != null)
            {
                result.FileName = model.CVFile.FileName;
                using (var ms = new MemoryStream())
                {
                    model.CVFile.InputStream.CopyTo(ms);
                    result.CV = ms.ToArray();
                }
            }
            

            return result;
        }
    }
}