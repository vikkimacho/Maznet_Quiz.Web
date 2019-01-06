using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Quiz.Web.UI.Helper;
using System.Configuration;
using System.Web.Security;
using Newtonsoft.Json;
using Quiz.Web.DTO.Models;
using System.Security.Principal;

namespace Quiz.Web.UI.Controllers
{

    public class LoginController : Controller
    {
        #region Declaration
        private Random random = new Random();
        private readonly string Success = "SUCCESS";
        private readonly string Failed = "FAILED";
        Logger logger = new Logger();
        AESEncryption encryption = new AESEncryption();
        #endregion
        // GET: Login
        public ActionResult Index()
        {

            //code 
            string apiUrl = ConfigurationManager.AppSettings["WebApiUrl"];

            HttpClient client = new HttpClient();
            //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.GetAsync(apiUrl + "/Login/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.MyData = response.Content.ReadAsStringAsync().Result;
            }
            return View("Login");
        }

        public ActionResult ValidateUser(string username, string password)
        {

            string result = Failed;
            try
            {
                string apiUrl = ConfigurationManager.AppSettings["WebApiUrl"];

                HttpClient client = new HttpClient();
                //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.GetAsync(apiUrl + "/Login/ValidateUser?username=" + username + "&password=" + password).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(result);
                }
            }
            catch (Exception ex)
            {
                logger.WriteToLogFile("ValidateUser - " + ex.ToString());
                if (ex.InnerException != null)
                {
                    logger.WriteToLogFile("ValidateUser InnerException - " + ex.ToString());
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostLogin(AdminLogin login, FormCollection form)
        {
            string LogoutURL = ConfigurationManager.AppSettings["WebUIUrl"];

            string result = Failed;
            try
            {
                string username = Convert.ToString(form["username"]);
                string password = Convert.ToString(form["password"]);
                if (!string.IsNullOrEmpty(username))
                {

                    string apiUrl = ConfigurationManager.AppSettings["WebApiUrl"];

                    HttpClient client = new HttpClient();
                    //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.GetAsync(apiUrl + "/Login/ValidateUser?username=" + username + "&password=" + password).Result;
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
                        string url = Convert.ToString(Request.Url);
                        string port = Convert.ToString(Request.Url.Port);


                        if (!string.IsNullOrEmpty(url))
                        {
                            return RedirectToAction("Dashboard", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                return Redirect(LogoutURL);
            }
            catch (Exception ex)
            {

                return Redirect(LogoutURL);
            }
        }


        public ActionResult AdminLogin()
        {
            AdminLogin login = new AdminLogin();
            return PartialView("AdminLogin", login);
        }

        public ActionResult LogOut()
        {

            string LogoutURL = ConfigurationManager.AppSettings["WebUIUrl"];
            try
            {
                Response.Cache.SetExpires(DateTime.Now.AddMinutes(-1));
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();
                System.Web.HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                return Redirect(LogoutURL);
            }
            catch (Exception ex)
            {
                logger.WriteToLogFile(ex.ToString());
                if (ex.InnerException != null)
                {
                    logger.WriteToLogFile(ex.InnerException.ToString());
                }

                return Redirect(LogoutURL);
            }
        }

        [Route("forgot_password")]
        public ActionResult forgot_password()
        {
            return View();
        }

        public ActionResult ForgotPassword(string emailOrName)
        {
            string result = Failed;
            try
            {
                string apiUrl = ConfigurationManager.AppSettings["WebApiUrl"];

                HttpClient client = new HttpClient();

                HttpResponseMessage response = client.GetAsync(apiUrl + "/AdminManagement/GetAdminList").Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    var adminDetails = JsonConvert.DeserializeObject<List<AdminDetails>>(result);
                    if (adminDetails.Count() > 0)
                    {
                        var userDetailsEmail = adminDetails.FirstOrDefault(x => x.Email.ToUpper().Trim() == emailOrName.Trim().ToUpper() && x.Isdeleted == false);
                        var admuserDetailsName = adminDetails.FirstOrDefault(x => x.UserName.ToUpper().Trim() == emailOrName.Trim().ToUpper() && x.Isdeleted == false);
                        var userDetail = userDetailsEmail == null ? admuserDetailsName : userDetailsEmail;
                        if (userDetail != null)
                        {

                            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                            var password = new string(Enumerable.Repeat(chars, 8)
                              .Select(s => s[random.Next(s.Length)]).ToArray());

                            GoogleMail mail = new GoogleMail();
                            mail.Body = "Hi " + userDetail.Name + ", Your temporary password is - " + password;
                            mail.Subject = "Forgot Password";
                            mail.ToMail = userDetail.Email;
                            var data = JsonConvert.SerializeObject(mail);
                            response = client.PostAsJsonAsync(apiUrl + "/GoogleMail/SendGoogleMail", mail).Result;
                            result = response.Content.ReadAsStringAsync().Result;
                            result = JsonConvert.DeserializeObject<string>(result);
                            if (result == Success)
                            {
                                userDetail.Password = password;
                                response = client.PostAsJsonAsync(apiUrl + "/AdminManagement/SaveAdminDetails", userDetail).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    result = response.Content.ReadAsStringAsync().Result;
                                    result = JsonConvert.DeserializeObject<string>(result);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.WriteToLogFile(ex.ToString());
                if (ex.InnerException != null)
                {
                    logger.WriteToLogFile(ex.InnerException.ToString());
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
    public class GoogleMail
    {
        public string ToMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}