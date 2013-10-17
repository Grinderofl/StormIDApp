using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Configuration;
using Domain.Entities;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        private IRepository<JobApplication> _repository;

        public AdminController(IRepository<JobApplication> repository)
        {
            _repository = repository;
        }

        public ActionResult Applications()
        {
            var model = _repository.All();
            return View(model);
        }

        public ActionResult Settings()
        {
            return View();
        }

        public PartialViewResult AllowedFileTypes()
        {
            return PartialView("AllowedFileTypes");
        }

        public RedirectToRouteResult EnableExtension(FileTypes type)
        {
            Domain.Configuration.Settings.AllowedFileTypes.Add(type);
            return RedirectToAction("AllowedFileTypes");
        }

        public RedirectToRouteResult DisableExtension(FileTypes type)
        {
            Domain.Configuration.Settings.AllowedFileTypes.Remove(type);
            return RedirectToAction("AllowedFileTypes");
        }
    }
}
