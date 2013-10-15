using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Abstract
{
    public interface IApplicationSubmitter
    {
        SubmissionErrors Submit(JobApplication cv);
        int Count();
    }
}
