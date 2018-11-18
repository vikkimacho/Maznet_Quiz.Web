using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Quiz.Web.UI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {

            //code 
            string apiUrl = "http://localhost:58491/api";
            
            
            HttpClient client = new HttpClient();
            //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.GetAsync(apiUrl + "/Login").Result;
            if (response.IsSuccessStatusCode)
            {

                ViewBag.MyData = response.Content.ReadAsStringAsync().Result;


            }







            return View();
        }
    }
}