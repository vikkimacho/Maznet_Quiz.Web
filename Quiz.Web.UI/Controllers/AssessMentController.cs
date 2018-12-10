using Quiz.Web.UI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Quiz.Web.UI.Controllers
{
    [AuthorizationFilter]
    public class AssessMentController : Controller
    {
        // GET: AssessMent
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateAssessment()
        {
            try
            {
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.GetAsync(apiUrl + "/Assessment/CreateAssessment").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

    }
}