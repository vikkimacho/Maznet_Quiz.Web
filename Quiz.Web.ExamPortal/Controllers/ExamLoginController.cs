﻿using System;
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
using Quiz.Web.ExamPortal.Helper;

namespace Quiz.Web.ExamPortal.Controllers
{
    public class ExamLoginController : Controller
    {
        // GET: ExamLogin

        #region Declaration
        private readonly string Success = "SUCCESS";
        private readonly string Failed = "FAILED";
        string apiUrl = ConfigurationManager.AppSettings["WebApiUrl"];
        private APIResponse APIResponse = new APIResponse();
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
            
            try
            {
                HttpClient client = new HttpClient();
                //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.GetAsync(apiUrl + "/Exam/ValidateExaminer?username=" + username + "&password=" + password + "&assessmentID=" + assessmentID).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Response =  response.Content.ReadAsStringAsync().Result;
                    APIResponse = JsonConvert.DeserializeObject<APIResponse>(Response);
                }

                if (APIResponse.Result)
                {
                    var userId = APIResponse.Guid;
                    SessionHelper.sessionObjects.UserID = userId;
                    SessionHelper.sessionObjects.AssessmentID = new Guid(assessmentID);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(APIResponse.Message, JsonRequestBehavior.AllowGet);
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
                        APIResponse = JsonConvert.DeserializeObject<APIResponse>(result);
                    }
                    if (APIResponse.Result)
                    {
                        System.Web.HttpContext.Current.Response.Cookies.Clear();
                        System.Web.HttpContext.Current.Session["userid"] = login.username;
                        FormsAuthentication.SetAuthCookie(login.username, false);
                        Session["LoginAssessmentID"] = login.assessmentid;                        
                        return RedirectToAction("Register", "ExamLogin");

                    }
                }
                return Redirect(LogoutURL);
            }
            catch (Exception ex)
            {

                return Redirect(LogoutURL);
            }
        }

        public ActionResult Register()
        {
            List<CustomRegistration> customRegistration = new List<CustomRegistration>();
            try
            {
                string assessmentID = Convert.ToString(SessionHelper.sessionObjects.AssessmentID);
                HttpClient client = new HttpClient();
                var response = client.GetAsync(apiUrl + "/Exam/GetRegistration?assessmentID=" + assessmentID).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    customRegistration = JsonConvert.DeserializeObject<List<CustomRegistration>>(result);
                    response = client.GetAsync(apiUrl + "/Exam/GetRegistration?assessmentID=" + assessmentID).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                        var usersDetails = JsonConvert.DeserializeObject<UsersDetailsModel>(result);
                    }
                }
            }
            catch (Exception ex)
            {


            }
            return View("Registration", customRegistration);
        }

        [HttpPost]
        public ActionResult UpdateUser(UsersDetailsModel usersDetailsModel)
        {


            try
            {
                HttpClient client = new HttpClient();
                usersDetailsModel.Id = SessionHelper.sessionObjects.UserID;
                usersDetailsModel.assessmentID = SessionHelper.sessionObjects.AssessmentID;
                var result = client.PostAsJsonAsync(apiUrl + "/UserManagement/UpdateUser", usersDetailsModel).Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    APIResponse = JsonConvert.DeserializeObject<APIResponse>(Result);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Json(APIResponse, JsonRequestBehavior.AllowGet);
        }
    }
}