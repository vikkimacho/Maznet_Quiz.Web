using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quiz.Web.ExamPortal.Models;

namespace Quiz.Web.ExamPortal.Controllers
{
    public class ExamLoginController : Controller
    {
        // GET: ExamLogin
        public ActionResult ExamLogin(string assessmentid = null)
        {
            if(!string.IsNullOrEmpty(assessmentid))
            {
                Guid assessmentID = new Guid(assessmentid);
            }
            return View();
        }
        public ActionResult UserLoin()
        {
            ExamLogin login = new ExamLogin();
            return PartialView("UserLogin", login);
        }
    }
}