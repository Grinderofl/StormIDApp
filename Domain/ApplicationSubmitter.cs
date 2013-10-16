using System.IO;
using System.Linq;
using System.Reflection;
using Domain.Abstract;
using Domain.Configuration;
using Domain.Entities;
using Domain.Enums;
using Library.Attributes;

namespace Domain
{
    public class ApplicationSubmitter
    {
        private readonly IRepository<JobApplication> _repository;

        public ApplicationSubmitter(IRepository<JobApplication> repository)
        {
            _repository = repository;
        }

        public SubmissionErrors Submit(JobApplication cv)
        {
            if (cv.CV.Length == 0 || cv.FileName.Length == 0)
                return SubmissionErrors.MissingCV;

            foreach (var item in Settings.AllowedFileTypes)
            {
                var fi = item.GetType().GetField(item.ToString());
                var attribute = (FileAttribute) fi.GetCustomAttribute(typeof (FileAttribute), true);
                if (attribute.Extension.ToLowerInvariant() == Path.GetExtension(cv.FileName).Replace(".","").ToLowerInvariant())
                    break;
                return SubmissionErrors.InvalidExtension;
            }
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
