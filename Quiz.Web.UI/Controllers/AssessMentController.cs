using Newtonsoft.Json;
using Quiz.Web.DTO.Models;
using Quiz.Web.UI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Quiz.Web.UI.Controllers
{
    public class AssessMentController : Controller
    {
        Logger logger = new Logger();

        //public ActionResult CreateAssessment()
        //{
        //    try
        //    {
        //        string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
        //        HttpClient client = new HttpClient();
        //        //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = client.GetAsync(apiUrl + "/Assessment/CreateAssessment").Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = response.Content.ReadAsStringAsync().Result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View();
        //}

        public ActionResult CreateAssessment()
        {
            AssesmentPageModal assesmentDetails = new AssesmentPageModal();
            try
            {
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(apiUrl + "/Assessment/GetAssessmentPageModal").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    assesmentDetails = JsonConvert.DeserializeObject<AssesmentPageModal>(result);
                    return View(assesmentDetails);
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
            return View();
        }



    }
}