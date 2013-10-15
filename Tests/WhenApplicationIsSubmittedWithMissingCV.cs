using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Domain.Enums;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class WhenApplicationIsSubmittedWithMissingCV
    {
        #region Fields

        private ApplicationSubmitter _submitter;

        private JobApplication _application;
        #endregion

        [SetUp]
        public void Setup()
        {
            _submitter = new ApplicationSubmitter(new MemoryRepository<JobApplication>());
            _application = new JobApplication()
            {
                CV = new byte[0]
            };
        }

        [Test]
        public void MissingCVErrorIsDisplayed()
        {
            var result = _submitter.Submit(_application);
            Assert.That(result, Is.EqualTo(SubmissionErrors.MissingCV));
        }

        [Test]
        public void StoredCVCountIsNotIncreased()
        {
            var result = _submitter.Submit(_application);
            Assert.That(_submitter.Count(), Is.EqualTo(0));
        }
    }
}
