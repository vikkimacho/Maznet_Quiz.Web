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
        #region Declaration
        private readonly string Success = "SUCCESS";
        private readonly string Failed = "FAILED";
        Logger logger = new Logger();

        #endregion
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
            catch (Exception ex)
            {

            }
            return View(adminDetails);
        }

        public ActionResult EditAdminDtails(string id)
        {
            AdminDetails adminDetails = new AdminDetails();

            try
            {
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.GetAsync(apiUrl + "/AdminManagement/GetAdminList").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var list = JsonConvert.DeserializeObject<List<AdminDetails>>(result);
                    Guid adminID = new Guid(id);
                    adminDetails = list.FirstOrDefault(x => x.ID == adminID);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(adminDetails, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveAdminDetails(AdminDetails adminDetail)
        {
            string result = Failed;
            try
            {
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsJsonAsync(apiUrl + "/AdminManagement/SaveAdminDetails", adminDetail).Result;
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

        public ActionResult DeleteAdminDetails(string id)
        {
            string result = Failed;
            try
            {
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsJsonAsync(apiUrl + "/AdminManagement/DeleteAdmin?id=" + id, id).Result;
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

        public ActionResult ActivateAdmin(string id)
        {
            string result = Failed;
            try
            {
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.GetAsync(apiUrl + "/AdminManagement/GetAdminList").Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    var list = JsonConvert.DeserializeObject<List<AdminDetails>>(result);
                    Guid adminID = new Guid(id);
                    var adminDetails = list.FirstOrDefault(x => x.ID == adminID);
                    if (adminDetails != null)
                    {
                        response = client.PostAsJsonAsync(apiUrl + "/AdminManagement/SaveAdminDetails", adminDetails).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            result = response.Content.ReadAsStringAsync().Result;
                            result = JsonConvert.DeserializeObject<string>(result);
                        }

                    }
                }
            }
            catch (Exception ex)
            {


            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}