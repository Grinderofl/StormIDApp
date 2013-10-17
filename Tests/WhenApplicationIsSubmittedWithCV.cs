using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Concrete;
using Domain.Configuration;
using Domain.Entities;
using Domain.Enums;
using NUnit.Framework;
using Tests.Library;

namespace Tests
{
    [TestFixture]
    public class WhenApplicationIsSubmittedWithCV
    {
        #region Fields

        private ApplicationSubmitter _submitter;

        #endregion

        [SetUp]
        public void Setup()
        {
            _submitter = new ApplicationSubmitter(new MemoryRepository<JobApplication>());
            Settings.AllowedFileTypes = new ObservableCollection<FileTypes>(){ FileTypes.Docx };
        }

        [Test]
        public void DisallowedFileTypeReturnsInvalidExtension()
        {
            var application = new JobApplication()
            {
                FileName = "neroscv.pdf",
                CV = ResourceReader.GetResourceAsArray("Tests.neroscv.pdf")
            };

            var result = _submitter.Submit(application);
            Assert.That(result, Is.EqualTo(SubmissionErrors.InvalidExtension));
        }

        [Test]
        public void AllowedFileTypeReturnsNone()
        {
            var application = new JobApplication()
            {
                FileName = "neroscv.docx",
                CV = ResourceReader.GetResourceAsArray("Tests.neroscv.docx")
            };

            var result = _submitter.Submit(application);
            Assert.That(result, Is.EqualTo(SubmissionErrors.None));
        }

        [Test]
        public void AllowedFileTypeIncreasesSubmittedApplicationCountByOne()
        {
            var application = new JobApplication()
            {
                FileName = "neroscv.docx",
                CV = ResourceReader.GetResourceAsArray("Tests.neroscv.docx")
            };
            var originalCount = _submitter.Count();

            var result = _submitter.Submit(application);
            Assert.That(_submitter.Count(), Is.EqualTo(originalCount + 1));
        }
    }
}
