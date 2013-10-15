using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Concrete
{
    public class ApplicationSubmitter : IApplicationSubmitter
    {
        private readonly IRepository<JobApplication> _repository;

        public ApplicationSubmitter(IRepository<JobApplication> repository)
        {
            _repository = repository;
        }

        public SubmissionErrors Submit(JobApplication cv)
        {
            if (cv.CV.Length == 0)
                return SubmissionErrors.MissingCV;

            _repository.Add(cv);
            _repository.Commit();
            return SubmissionErrors.None;
        }

        public int Count()
        {
            return _repository.Count();
        }
    }
}
