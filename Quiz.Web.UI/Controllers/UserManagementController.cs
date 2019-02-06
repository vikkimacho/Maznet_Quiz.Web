using Newtonsoft.Json;
using Quiz.Web.DTO.Models;
using Quiz.Web.UI.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Configuration;

namespace Quiz.Web.UI.Controllers
{
    [AuthorizationFilter]
    public class UserManagementController : Controller
    {
        string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];

        private APIResponse APIResponse = new APIResponse();
        // GET: UserManagement
        public ActionResult UserManagement()
        {           
            List<UsersDetails> usersDetails = new List<UsersDetails>();            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.GetAsync(apiUrl + "/UserManagement/GetUsersList").Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    usersDetails = JsonConvert.DeserializeObject<List<UsersDetails>>(Result);
                }
            }
            return View(usersDetails);
        }
        public ActionResult UserManagementList(Guid? UserDetailId)
        {
            List<UsersDetailsModel> usersDetails = new List<UsersDetailsModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.GetAsync(apiUrl + "/UserManagement/GetUsersDetailList?UserDetailId=" + UserDetailId).Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    usersDetails = JsonConvert.DeserializeObject<List<UsersDetailsModel>>(Result);
                }
            }
            return View(usersDetails);
        }

        [HttpPost]
        public ActionResult UploadUserDetail(string UserNameTitle)
        {
            string Result = "Failed";
            
            APIResponse response = new APIResponse();

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                string fileextension = Path.GetExtension(hpf.FileName);
                var UsersDetail = new UsersDetails();
                if (fileextension == ".xlsx" || fileextension == ".xls" || fileextension == ".csv")
                {
                    var dt = Common.ConvertFileToDateTable(hpf, "UserDetailsUploadFile");
                    UsersDetails usersDetails = new UsersDetails();
                    usersDetails.UsersDetailsModel = new List<UsersDetailsModel>();
                    List<UsersDetailsModel>  userslist =new List<UsersDetailsModel>();
                    usersDetails.UserTitleName = UserNameTitle;
                    var rowCount = dt.Rows.Count;
                    for (int i = 0; i < rowCount; i++)
                    {
                        UsersDetailsModel UsersDetails = new UsersDetailsModel();
                        UsersDetails.Name = dt.Rows[i]["Name"] != DBNull.Value ? dt.Rows[i]["Name"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.Email = dt.Rows[i]["Email"] != DBNull.Value ? dt.Rows[i]["Email"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.Password = dt.Rows[i]["Password"] != DBNull.Value ? dt.Rows[i]["Password"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.MobileNumber = dt.Rows[i]["MobileNumber"] != DBNull.Value ? dt.Rows[i]["MobileNumber"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.Degree = dt.Rows[i]["Degree"] != DBNull.Value ? dt.Rows[i]["Degree"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.Institution = dt.Rows[i]["Institution"] != DBNull.Value ? dt.Rows[i]["Institution"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.Major = dt.Rows[i]["Major"] != DBNull.Value ? dt.Rows[i]["Major"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.Percentage = dt.Rows[i]["Percentage"] != DBNull.Value ? dt.Rows[i]["Percentage"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.Gender = dt.Rows[i]["Gender"] != DBNull.Value ? dt.Rows[i]["Gender"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.Address = dt.Rows[i]["Address"] != DBNull.Value ? dt.Rows[i]["Address"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.SSLCPercentage = dt.Rows[i]["SSLCPercentage"] != DBNull.Value ? dt.Rows[i]["SSLCPercentage"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.SSLCBoardName = dt.Rows[i]["SSLCBoardName"] != DBNull.Value ? dt.Rows[i]["SSLCBoardName"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.TechnicalSkills = dt.Rows[i]["TechnicalSkills"] != DBNull.Value ? dt.Rows[i]["TechnicalSkills"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.HSCPercentage = dt.Rows[i]["HSCPercentage"] != DBNull.Value ? dt.Rows[i]["HSCPercentage"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.LastName = dt.Rows[i]["LastName"] != DBNull.Value ? dt.Rows[i]["LastName"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.DOB = dt.Rows[i]["DOB"] != DBNull.Value ? dt.Rows[i]["DOB"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.State = dt.Rows[i]["State"] != DBNull.Value ? dt.Rows[i]["State"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.DegreePassedOutYear = dt.Rows[i]["DegreePassedOutYear"] != DBNull.Value ? dt.Rows[i]["DegreePassedOutYear"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.HSCBoardName = dt.Rows[i]["HSCBoardName"] != DBNull.Value ? dt.Rows[i]["HSCBoardName"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.HSCPassedOutYear = dt.Rows[i]["HSCPassedOutYear"] != DBNull.Value ? dt.Rows[i]["HSCPassedOutYear"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.SSLCPassedOutYear = dt.Rows[i]["SSLCPassedOutYear"] != DBNull.Value ? dt.Rows[i]["SSLCPassedOutYear"].ToString().Trim().ToUpper() : string.Empty;

                        if (UsersDetails != null)
                        {
                            userslist.Add(UsersDetails);
                        }                        
                    }

                    usersDetails.UsersDetailsModel = userslist;
                    if (usersDetails.UsersDetailsModel.Any())
                    {
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(apiUrl);
                            var result = client.PostAsJsonAsync(apiUrl + "/UserManagement/UploadUserDetail", usersDetails).Result;
                            if (result.IsSuccessStatusCode)
                            {
                                Result = result.Content.ReadAsStringAsync().Result;

                                response = JsonConvert.DeserializeObject<APIResponse>(Result);
                            }
                        }

                    }
                }
            }
            return Json(new { data = response });
        }
        

        public ActionResult UserDetailEdit(Guid? UserDetailId)
        {
            UsersDetails usersDetails = new UsersDetails();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.GetAsync(apiUrl + "/UserManagement/UserDetailEdit?UserDetailId=" + UserDetailId).Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    usersDetails = JsonConvert.DeserializeObject<UsersDetails>(Result);
                }
            }
            return Json(usersDetails, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public ActionResult UpdateUserDetail(UsersDetails usersDetails)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.PostAsJsonAsync(apiUrl + "/UserManagement/UpdateUserDetail", usersDetails).Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    APIResponse = JsonConvert.DeserializeObject<APIResponse>(Result);
                }
            }
            return Json(APIResponse, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UpdateUser(UsersDetailsModel usersDetailsModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.PostAsJsonAsync(apiUrl + "/UserManagement/UpdateUser", usersDetailsModel).Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    APIResponse = JsonConvert.DeserializeObject<APIResponse>(Result);
                }
            }
            return Json(APIResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserDetailDelete(Guid? UserDetailId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.GetAsync(apiUrl + "/UserManagement/UserDetailDelete?UserDetailId=" + UserDetailId).Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    APIResponse = JsonConvert.DeserializeObject<APIResponse>(Result);
                }
            }
            return Json(APIResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserDelete(Guid? UserId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.GetAsync(apiUrl + "/UserManagement/UserDelete?UserId=" + UserId).Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    APIResponse = JsonConvert.DeserializeObject<APIResponse>(Result);
                }
            }
            return Json(APIResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DownloadTemplate()
        {
            string FilePath = "";
            try
            {
                string UserTemplate = ConfigurationManager.AppSettings["UserTemplate"];
                FilePath = System.Web.Hosting.HostingEnvironment.MapPath(UserTemplate);
            }
            catch (Exception ex)
            {
                
            }
            return File(FilePath, "application/vnd.ms-excel", Path.GetFileName(FilePath));
        }
    }
}