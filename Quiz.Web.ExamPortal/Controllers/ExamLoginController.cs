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
using Quiz.Web.UI.Helper;

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
        Logger logger = new Logger();
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
                    var Response = response.Content.ReadAsStringAsync().Result;
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
                }
            }
            catch (Exception ex)
            {

            }
            return View("Registration", customRegistration);
        }

        [HttpPost]
        public ActionResult UpdateUser(string obj)
        {
            APIResponse = new APIResponse();
            try
            {
                UsersDetailsModel usersDetailsModel = JsonConvert.DeserializeObject<UsersDetailsModel>(obj);
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
            catch (Exception ex)
            {


            }

            return Json(APIResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveExamAnswer(string quesID, string answer)
        {
            string result = "FAILED";
            try
            {
                if (!string.IsNullOrEmpty(quesID))
                {
                    HttpClient client = new HttpClient();
                    Guid assessmentID = SessionHelper.sessionObjects.AssessmentID;
                    Guid qusID = new Guid(quesID);
                    Guid userID = SessionHelper.sessionObjects.UserID;
                    if (assessmentID != Guid.Empty && userID != Guid.Empty)
                    {
                        var response = client.PostAsJsonAsync(apiUrl + "/Exam/SaveExamAnswers?assesmentID=" + assessmentID + "&userID="
                            + userID + "&qusID=" + qusID + "&answer=" + answer, string.Empty).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var responseString = response.Content.ReadAsStringAsync().Result;
                            result = JsonConvert.DeserializeObject<string>(responseString);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SubmitExam()
        {
            APIResponse result = new APIResponse();
            try
            {
                    HttpClient client = new HttpClient();
                    Guid assessmentID = SessionHelper.sessionObjects.AssessmentID;
                    Guid userID = SessionHelper.sessionObjects.UserID;
                    if (assessmentID != Guid.Empty && userID != Guid.Empty)
                    {
                        var response = client.PostAsJsonAsync(apiUrl + "/Exam/SubmitExam?assesmentID=" + assessmentID + "&userID="
                            + userID, string.Empty).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var responseString = response.Content.ReadAsStringAsync().Result;
                            result = JsonConvert.DeserializeObject<APIResponse>(responseString);
                        }
                    }                
            }
            catch (Exception ex)
            {

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public ActionResult AssessmentDetails()
        {

            HttpClient client = new HttpClient();
            List<ExamAssessmentDetails> examAssessmentDetails = new List<ExamAssessmentDetails>();
            Guid assessmentid = SessionHelper.sessionObjects.AssessmentID;
            HttpResponseMessage response = client.GetAsync(apiUrl + "/Exam/GetAssessmentDetails?assessmentID=" + assessmentid).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                examAssessmentDetails = JsonConvert.DeserializeObject<List<ExamAssessmentDetails>>(result);
                ViewBag.LoginStatus = result;
            }
            return View(examAssessmentDetails);
        }

        public ActionResult StartExam()
        {
            HttpClient client = new HttpClient();
            List<Questions> questions = new List<Questions>();
            Guid assesmentID = SessionHelper.sessionObjects.AssessmentID;
            Guid UserID = SessionHelper.sessionObjects.UserID;
            HttpResponseMessage response = client.GetAsync(apiUrl + "/Exam/GetAssesmentQuestions?assesmentID=" + assesmentID + "&UserID="+ UserID).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                questions = JsonConvert.DeserializeObject<List<Questions>>(result);
                questions = ShuffleList(questions);
                var limitedQus = questions.Take(1).Skip(0).ToList();
                Session["QuestionsList"] = questions;
                ViewBag.LoginStatus = result;
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult GetQuestion(int skip, int take)
        {
            List<Questions> qusList = new List<Questions>();
            ViewBag.Skip = skip;
            ViewBag.Take = take;
            try
            {
                qusList = Session["QuestionsList"] as List<Questions>;
                HttpClient client = new HttpClient();
                Questions questions = new Questions();
                Guid assesmentID = SessionHelper.sessionObjects.AssessmentID;
                Guid UserID = SessionHelper.sessionObjects.UserID;
                ViewBag.Questions = qusList.Count();
                qusList = qusList.Take(take).Skip(skip).ToList();
                if (qusList.Count > 0)
                {
                HttpResponseMessage response = client.GetAsync(apiUrl + "/Exam/GetAssesmentQuestionDetail?assesmentID=" + assesmentID + "&UserID=" + UserID + "&QuestionId=" + qusList.FirstOrDefault().ID).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    questions = JsonConvert.DeserializeObject<Questions>(result);
                }

                qusList.FirstOrDefault().Answer = questions.Answer;
            }

            }
            catch (Exception ex)
            {
                throw ex;
            }
                return PartialView("_Exam", qusList);
        }

        private List<Questions> ShuffleList(List<Questions> inputList)
        {
            List<Questions> randomList = new List<Questions>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }

        public ActionResult UserEdit(Guid? UserId)
        {
            UsersDetailsModel usersDetailsModel = new UsersDetailsModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.GetAsync(apiUrl + "/UserManagement/UserEdit?UserId=" + UserId).Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    usersDetailsModel = JsonConvert.DeserializeObject<UsersDetailsModel>(Result);
                }
            }
            return Json(usersDetailsModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            var AssessmentID = SessionHelper.sessionObjects.AssessmentID;
            try
            {
                Response.Cache.SetExpires(DateTime.Now.AddMinutes(-1));
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();
                System.Web.HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                return RedirectToAction("ExamLogin", new { assessmentid = AssessmentID });
            }
            catch (Exception ex)
            {
                logger.WriteToLogFile(ex.ToString());
                if (ex.InnerException != null)
                {
                    logger.WriteToLogFile(ex.InnerException.ToString());
                }

                return RedirectToAction("ExamLogin", new { assessmentid = AssessmentID });
            }
        }

    }

}
