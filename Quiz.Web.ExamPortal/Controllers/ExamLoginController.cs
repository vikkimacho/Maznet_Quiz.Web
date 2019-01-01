using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quiz.Web.DTO.Models;
using Quiz.Web.ExamPortal.Models;
using System.Net.Http;
using System.Security.Principal;
using Newtonsoft.Json;

namespace Quiz.Web.ExamPortal.Controllers
{
    public class ExamLoginController : Controller
    {
        // GET: ExamLogin
        string apiUrl = ConfigurationManager.AppSettings["WebApiUrl"];
        public ActionResult ExamLogin(Guid assessmentid)
        {

            HttpClient client = new HttpClient();
            //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.GetAsync(apiUrl + "/Exam/GetPortalLogin?assessmentID=" + assessmentid).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<string>(result);
                ViewBag.LoginStatus = result;
            }
            return View("ExamLogin");
        }
        public ActionResult UserLogin(string assessmentID)
        {
            ExamLogin login = new ExamLogin();
            login.assessmentid = assessmentID;
            return PartialView("UserLogin", login);
        }

     
        public ActionResult ValidateExaminer(string username, string password, string assessmentID)
        {
            string result = "Failed";
            try
            {
                HttpClient client = new HttpClient();
                //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.GetAsync(apiUrl + "/Login/ValidateExaminer?username=" + username + "&password=" + password).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(result);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}