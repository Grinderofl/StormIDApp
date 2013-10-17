using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class JobApplicationModel
    {
        [Required(ErrorMessage = "Please fill out your name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your e-mail.")]
        [EmailAddress(ErrorMessage = "The e-mail address is invalid.")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please write a short message about yourself.")]
        [DisplayName("Cover letter")]
        public string Cover { get; set; }

        [Required(ErrorMessage = "Please upload your CV.")]
        [DisplayName("Upload CV")]
        public HttpPostedFileBase CVFile { get; set; }
    }
}