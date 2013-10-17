using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using Domain.Entities;
using Domain.Enums;
using Web.Mappers;
using Web.Models;

namespace Web.Controllers
{
    public class CVController : Controller
    {
        private readonly ApplicationSubmitter _submitter;
        private readonly IRepository<JobApplication> _repository; 

        public CVController(ApplicationSubmitter submitter, IRepository<JobApplication> repository)
        {
            _submitter = submitter;
            _repository = repository;
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(JobApplicationModel model)
        {
            if (ModelState.IsValid)
            {
                var mapped = JobApplicationMapper.Map(model);
                var result = _submitter.Submit(mapped);
                if (result == SubmissionErrors.InvalidExtension)
                    ModelState.AddModelError("CVFile", "Invalid Extension for CV");
                if (result == SubmissionErrors.MissingCV)
                    ModelState.AddModelError("CVFile", "Empty CV");

                if (result == SubmissionErrors.None)
                    return RedirectToAction("Success");
            }


            return View(model);
        }

        public ViewResult Success()
        {
            return View();
        }

        public FileResult Download(string name, string filename)
        {
            var file = _repository.Get(x => x.Name == name && x.FileName == filename).FirstOrDefault();
            if(file != null)
                return new FileContentResult(file.CV, "application/pdf");
            return null;
        }
    }
}
