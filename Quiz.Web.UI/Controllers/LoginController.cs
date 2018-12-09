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
        private readonly string Success = "SUCCESS";
        private readonly string Failed = "FAILED";
        Logger logger = new Logger();
        AESEncryption encryption = new AESEncryption();
        #endregion
        // GET: Login
        public ActionResult Index()
        {

            //code 
            string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];

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
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];

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
        public ActionResult PostLogin(AdminLogin login,FormCollection form)
        {
            string LogoutURL = ConfigurationManager.AppSettings["WebUIUrl"];

            string result = Failed;
            try
            {
                string username = Convert.ToString(form["username"]);
                string password = Convert.ToString(form["password"]);
                if (!string.IsNullOrEmpty(username))
                {
                    
                    string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];

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


                        if (url.Contains("localhost"))
                        {
                            return Redirect("http://localhost:" + port + "/home/dashboard");
                        }
                        else
                        {
                            return Redirect("~/home/");
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
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
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
                
                if (ex.InnerException != null)
                {
                    logger.WriteToLogFile(ex.InnerException.ToString());
                }
               
                return Redirect(LogoutURL);
            }
        }
    }
}