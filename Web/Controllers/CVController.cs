using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;

namespace Web.Controllers
{
    public class CVController : Controller
    {
        private ApplicationSubmitter _submitter;

        public CVController(ApplicationSubmitter submitter)
        {
            _submitter = submitter;
        }

        public ActionResult Upload()
        {
            return View();
        }

    }
}
