using Newtonsoft.Json;
using Quiz.Web.DTO.Models;
using Quiz.Web.UI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Quiz.Web.UI.Controllers
{
    [AuthorizationFilter]
    public class AdminManagementController : Controller
    {
        // GET: AdminManagement
        public ActionResult AdminManagement()
        {
            List<AdminDetails> adminDetails = new List<AdminDetails>();
            try
            {
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.GetAsync(apiUrl + "/AdminManagement/GetAdminList").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    adminDetails = JsonConvert.DeserializeObject<List<AdminDetails>>(result);
                }
            }
            catch(Exception ex)
            {

            }
            return View(adminDetails);
        }

        public ActionResult EditAdminDtails(string id)
        {
            AdminDetails admin = new AdminDetails();
            try
            {
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.GetAsync(apiUrl + "/AdminManagement/GetAdminList").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var adminDetails = JsonConvert.DeserializeObject<List<AdminDetails>>(result);
                    Guid adminID = new Guid(id);
                    admin = adminDetails.FirstOrDefault(x => x.ID == adminID);
                }
            }
            catch(Exception ex)
            {

            }
            return Json(admin, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveAdminDetails(AdminDetails adminDetail)
        {
            List<AdminDetails> adminDetails = new List<AdminDetails>();
            try
            {
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.GetAsync(apiUrl + "/AdminManagement/GetAdminList").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    adminDetails = JsonConvert.DeserializeObject<List<AdminDetails>>(result);
                }
            }
            catch(Exception ex)
            {

            }
            return View(adminDetails);
        }

        public ActionResult DeleteAdminDetails(string id)
        {
            List<AdminDetails> adminDetails = new List<AdminDetails>();
            try
            {
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.GetAsync(apiUrl + "/AdminManagement/GetAdminList").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    adminDetails = JsonConvert.DeserializeObject<List<AdminDetails>>(result);
                }
            }
            catch(Exception ex)
            {

            }
            return View(adminDetails);
        }
    }
}