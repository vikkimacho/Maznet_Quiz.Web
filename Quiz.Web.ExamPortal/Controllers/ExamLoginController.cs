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
using System.Web.Security;

namespace Quiz.Web.ExamPortal.Controllers
{
    public class ExamLoginController : Controller
    {
        // GET: ExamLogin

        #region Declaration
        private readonly string Success = "SUCCESS";
        private readonly string Failed = "FAILED";
        string apiUrl = ConfigurationManager.AppSettings["WebApiUrl"];
        #endregion
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
                HttpResponseMessage response = client.GetAsync(apiUrl + "/Exam/ValidateExaminer?username=" + username + "&password=" + password + "&assessmentID=" + assessmentID).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(result);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostLogin(ExamLogin login, FormCollection form)
        {
            string LogoutURL = ConfigurationManager.AppSettings["WebUIUrl"];

            string result = Failed;
            try
            {
                string username = Convert.ToString(form["username"]);
                string password = Convert.ToString(form["password"]);
                login.assessmentid = Convert.ToString(form["assessmentID"]); 
                if (!string.IsNullOrEmpty(username))
                {

                    string apiUrl = ConfigurationManager.AppSettings["WebApiUrl"];

                    HttpClient client = new HttpClient();
                    //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.GetAsync(apiUrl + "/Exam/ValidateExaminer?username=" + login.username + "&password=" + login.password + "&assessmentID=" + login.assessmentid).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                        result = JsonConvert.DeserializeObject<string>(result);
                    }
                    if (result != Failed)
                    {
                        System.Web.HttpContext.Current.Response.Cookies.Clear();
                        System.Web.HttpContext.Current.Session["userid"] = login.username;
                        FormsAuthentication.SetAuthCookie(login.username, false);
                        List<CustomRegistration> customRegistration = new List<CustomRegistration>();
                        response = client.GetAsync(apiUrl + "/Exam/GetRegistration?assessmentID=" + login.assessmentid).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            result = response.Content.ReadAsStringAsync().Result;
                            customRegistration = JsonConvert.DeserializeObject<List<CustomRegistration>>(result);
                        }
                        return View("Registration", customRegistration);
                    }
                }
                return Redirect(LogoutURL);
            }
            catch (Exception ex)
            {

                return Redirect(LogoutURL);
            }
        }
    }
}