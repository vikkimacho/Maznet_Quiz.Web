using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Quiz.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private static string apiUrl = "http://localhost:58491/api";
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DashBoard()
        {
            HttpClient client = new HttpClient();
            //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.GetAsync(apiUrl + "/DashBoard").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.MyData = response.Content.ReadAsStringAsync().Result;
            }
            return View();
        }

       


    }
}