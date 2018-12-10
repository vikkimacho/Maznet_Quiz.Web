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

namespace Quiz.Web.UI.Controllers
{
    [AuthorizationFilter]
    public class UserManagementController : Controller
    {
        string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
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
                        UsersDetails.MobileNumber = dt.Rows[i]["MobileNumber"] != DBNull.Value ? dt.Rows[i]["MobileNumber"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.Degree = dt.Rows[i]["Degree"] != DBNull.Value ? dt.Rows[i]["Degree"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.Institution = dt.Rows[i]["Institution"] != DBNull.Value ? dt.Rows[i]["Institution"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.Major = dt.Rows[i]["Major"] != DBNull.Value ? dt.Rows[i]["Major"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.Percentage = dt.Rows[i]["Percentage"] != DBNull.Value ? dt.Rows[i]["Percentage"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.Gender = dt.Rows[i]["Gender"] != DBNull.Value ? dt.Rows[i]["Gender"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.Address = dt.Rows[i]["Address"] != DBNull.Value ? dt.Rows[i]["Address"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.CustomField1 = dt.Rows[i]["CustomField1"] != DBNull.Value ? dt.Rows[i]["CustomField1"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.CustomField2 = dt.Rows[i]["CustomField2"] != DBNull.Value ? dt.Rows[i]["CustomField2"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.CustomField3 = dt.Rows[i]["CustomField3"] != DBNull.Value ? dt.Rows[i]["CustomField3"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.CustomField4 = dt.Rows[i]["CustomField4"] != DBNull.Value ? dt.Rows[i]["CustomField4"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.CustomField5 = dt.Rows[i]["CustomField5"] != DBNull.Value ? dt.Rows[i]["CustomField5"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.CustomField6 = dt.Rows[i]["CustomField6"] != DBNull.Value ? dt.Rows[i]["CustomField6"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.CustomField7 = dt.Rows[i]["CustomField7"] != DBNull.Value ? dt.Rows[i]["CustomField7"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.CustomField8 = dt.Rows[i]["CustomField8"] != DBNull.Value ? dt.Rows[i]["CustomField8"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.CustomField9 = dt.Rows[i]["CustomField9"] != DBNull.Value ? dt.Rows[i]["CustomField9"].ToString().Trim().ToUpper() : string.Empty;
                        UsersDetails.CustomField10 = dt.Rows[i]["CustomField10"] != DBNull.Value ? dt.Rows[i]["CustomField10"].ToString().Trim().ToUpper() : string.Empty;
                        
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


    }
}