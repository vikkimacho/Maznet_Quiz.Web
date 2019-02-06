using Excel;
using Newtonsoft.Json;
using Quiz.Web.DTO.Models;
using Quiz.Web.UI.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;
using Rotativa;

namespace Quiz.Web.UI.Controllers
{
    public class AssessMentController : Controller
    {
        string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];

        Logger logger = new Logger();
        private DataTable dt = new DataTable();
        private Random random = new Random();
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
                logger.WriteToLogFile("CreateAssessment - " + ex.ToString());
                if (ex.InnerException != null)
                {
                    logger.WriteToLogFile("CreateAssessment InnerException - " + ex.ToString());
                }
            }
            return View();
        }


        public ActionResult SaveEligibleCriteria(List<PostEligibilityCriteria> lstpostAssessmentModal)
        {
            string result = "Failed";
            try
            {
                List<EligibilityCriteriaForQuestionBank> lstEligibilityCriterias = new List<EligibilityCriteriaForQuestionBank>();

                if (lstpostAssessmentModal != null)
                {
                    foreach (var item in lstpostAssessmentModal)
                    {
                        EligibilityCriteriaForQuestionBank objEligibilityCriteriaForQuestionBank = new EligibilityCriteriaForQuestionBank();
                        item.EligibilityGuid = Guid.NewGuid();
                        objEligibilityCriteriaForQuestionBank.EligibilityCriteriaGuid = item.EligibilityGuid;
                        objEligibilityCriteriaForQuestionBank.QuestionBankId = item.QuestionBankID;
                        lstEligibilityCriterias.Add(objEligibilityCriteriaForQuestionBank);
                    }
                }

                string postData = JsonConvert.SerializeObject(lstpostAssessmentModal);
                logger.WriteToLogFile("Post Data " + postData);

                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.PostAsJsonAsync(apiUrl + "/Assessment/PostUpdateEligibleCriteria", lstpostAssessmentModal).Result;
                if (response.IsSuccessStatusCode)
                {
                    var retresult = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(retresult);
                    if (result != "Failed")
                    {
                        return Json(new { Result = "Success", ReturnPostEligibileCriteria = result });
                    }
                }
            }
            catch (Exception ex)
            {
                logger.WriteToLogFile("PostCreateAssessment - " + ex.ToString());
                if (ex.InnerException != null)
                {
                    logger.WriteToLogFile("PostCreateAssessment InnerException - " + ex.ToString());
                }
            }
            return Json(new { Result = result });
        }


        public ActionResult LoadMyAssesments()
        {
            List<MyAssesmentModal> result = new List<MyAssesmentModal>();
            try
            {
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                string IsActive = "";
                HttpResponseMessage response = client.GetAsync(apiUrl + "/Assessment/ListMyAssesments").Result;
                if (response.IsSuccessStatusCode)
                {
                    var retresult = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<List<MyAssesmentModal>>(retresult);
                    result = result.OrderByDescending(x => x.LastModified).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.WriteToLogFile("LoadMyAssesments - " + ex.ToString());
                if (ex.InnerException != null)
                {
                    logger.WriteToLogFile("LoadMyAssesments InnerException - " + ex.ToString());
                }
            }
            return View(result);
        }

        [HttpGet]
        public ActionResult LoadAutoFillQuestionBankNames(string term)
        {

            logger.WriteToLogFile("LoadAutoFillQuestionBankNames " + term);
            try
            {
                List<QuestionBankModal> result = new List<QuestionBankModal>();
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                string IsActive = "";
                HttpResponseMessage response = client.GetAsync(apiUrl + "/Assessment/LQuestionBankModal").Result;
                if (response.IsSuccessStatusCode)
                {
                    var retresult = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<List<QuestionBankModal>>(retresult);
                }

                //var questionBankList = (from N in result
                //                     where N.QuestionBankName.ToUpper().Contains(term.ToUpper() ?? "")
                //                     select new { N.Id,N.QuestionBankName });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.WriteToLogFile("LoadMyAssesments - " + ex.ToString());
                if (ex.InnerException != null)
                {
                    logger.WriteToLogFile("LoadMyAssesments InnerException - " + ex.ToString());
                }
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult ValidateAssesmentName(string AssesmentName)
        {
            string result = "Failed";
            try
            {

                logger.WriteToLogFile("Post Data ValidateAssesmentName " + AssesmentName);
                AssesmentName assesmentName = new AssesmentName();
                assesmentName.ValidateAssesmentName = AssesmentName;

                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.PostAsJsonAsync(apiUrl + "/Assessment/ValidateAssesmentName", assesmentName).Result;
                if (response.IsSuccessStatusCode)
                {
                    var retresult = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(retresult);
                }
            }
            catch (Exception ex)
            {
                logger.WriteToLogFile("ValidateAssesmentName - " + ex.ToString());
                if (ex.InnerException != null)
                {
                    logger.WriteToLogFile("ValidateAssesmentName InnerException - " + ex.ToString());
                }
            }
            return Json(new { Result = result });
        }


        [HttpPost]
        public ActionResult DeleteAssesment(Guid AssesmentId)
        {
            string result = "Failed";
            try
            {
                logger.WriteToLogFile("Post Data ValidateAssesmentGuid " + AssesmentId);
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.PostAsJsonAsync(apiUrl + "/Assessment/DeleteAssesment", AssesmentId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var retresult = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(retresult);
                }
            }
            catch (Exception ex)
            {
                logger.WriteToLogFile("DeleteAssesment - " + ex.ToString());
                if (ex.InnerException != null)
                {
                    logger.WriteToLogFile("DeleteAssesment InnerException - " + ex.ToString());
                }
            }
            return Json(new { Result = result });
        }

        public ActionResult PostCreateAssessment(PostAssessmentModal postAssessmentModal)
        {
            string result = "Failed";

            string Result = "Failed";
            try
            {
                string postData = JsonConvert.SerializeObject(postAssessmentModal);
                logger.WriteToLogFile("Post Data " + postData);

                if (postAssessmentModal == null)
                {
                    postAssessmentModal.lstBulkScheduleIds = new List<Guid>();

                }

                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.PostAsJsonAsync(apiUrl + "/Assessment/PostCreateAssessment", postAssessmentModal).Result;
                if (response.IsSuccessStatusCode)
                {
                    var retresult = response.Content.ReadAsStringAsync().Result;
                    string assessmentID = JsonConvert.DeserializeObject<string>(retresult);
                    if (postAssessmentModal.lstBulkScheduleIds != null)
                    {
                        if (postAssessmentModal.lstBulkScheduleIds.Any())
                        {
                            var userDetailId = postAssessmentModal.lstBulkScheduleIds.FirstOrDefault();

                            HttpClient clientuser = new HttpClient();

                            HttpResponseMessage responsfinal = clientuser.GetAsync(apiUrl + "/Assessment/GetUploadedUserDetail?UserDetailId=" + userDetailId).Result;
                            if (responsfinal.IsSuccessStatusCode)
                            {
                                Result = responsfinal.Content.ReadAsStringAsync().Result;

                                var finalUserdetails = JsonConvert.DeserializeObject<List<UsersDetailsModel>>(Result);

                                foreach (var item in finalUserdetails)
                                {
                                    if (item != null)
                                    {
                                        var password = item.Password;
                                        GoogleMail mail = new GoogleMail();
                                        string url = ConfigurationManager.AppSettings["ExamPortalUrl"] + assessmentID;
                                        mail.Body = "Hi " + item.Name + ",UserName -" + item.Email + " Password  - " + item.Password + "<a href=\"" + url + "\">Click Here</a>";
                                        mail.Subject = "Assessment Detail";
                                        mail.ToMail = item.Email;
                                        logger.WriteToLogFile("PostCreateAssessment Google Mail -" + " Mail Body : " + mail.Body + "Mail To : " + mail.ToMail);
                                        response = client.PostAsJsonAsync(apiUrl + "/GoogleMail/SendGoogleMail", mail).Result;
                                        result = response.Content.ReadAsStringAsync().Result;
                                        result = JsonConvert.DeserializeObject<string>(result);

                                    }
                                }
                            }

                        }
                    }
                    else if (postAssessmentModal.SingleScheduleModal != null)
                    {
                        GoogleMail mail = new GoogleMail();
                        string url = ConfigurationManager.AppSettings["ExamPortalUrl"] + assessmentID;
                        mail.Body = "Hi " + postAssessmentModal.SingleScheduleModal.FirstName + ",UserName -" + postAssessmentModal.SingleScheduleModal.UserName + " Password  - " + postAssessmentModal.SingleScheduleModal.Password + "<a href=\"" + url + "\">Click Here</a>";
                        mail.Subject = "Assessment Detail";
                        mail.ToMail = postAssessmentModal.SingleScheduleModal.Email;
                        logger.WriteToLogFile("PostCreateAssessment Google Mail -" + " Mail Body : " + mail.Body + "Mail To : " + mail.ToMail);
                        //var data = JsonConvert.SerializeObject(mail);
                        response = client.PostAsJsonAsync(apiUrl + "/GoogleMail/SendGoogleMail", mail).Result;
                        result = response.Content.ReadAsStringAsync().Result;
                        result = JsonConvert.DeserializeObject<string>(result);

                    }
                    if (assessmentID.ToUpper() != "FAILED")
                    {
                        result = "SUCCESS";
                    }

                }
            }
            catch (Exception ex)
            {
                logger.WriteToLogFile("PostCreateAssessment - " + ex.ToString());
                if (ex.InnerException != null)
                {
                    logger.WriteToLogFile("PostCreateAssessment InnerException - " + ex.ToString());
                }
            }
            return Json(new { Result = result });
        }

        [HttpPost]
        public ActionResult GetExistingQuestionBankDetails(Guid assessmentId)
        {
            var questionBankDetail = new List<ExistingQuestionBankDetails>();
            string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + "/Assessment/GetExistingQuestionBankDetails?assessmentId=" + assessmentId).Result;
            if (response.IsSuccessStatusCode)
            {
                var retresult = response.Content.ReadAsStringAsync().Result;
                questionBankDetail = JsonConvert.DeserializeObject<List<ExistingQuestionBankDetails>>(retresult);
            }
            return Json(new { questions = questionBankDetail });
        }



        public ActionResult UploadBulkLoginDetail(string UserNameTitle)
        {
            string Result = "Failed";
            APIResponse response = new APIResponse();
            var finalUserdetails = new List<UsersDetailsModel>();
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
                    List<UsersDetailsModel> userslist = new List<UsersDetailsModel>();
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
                    if (response.Result)
                    {

                        string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                        HttpClient client = new HttpClient();

                        HttpResponseMessage responsfinal = client.GetAsync(apiUrl + "/Assessment/GetUploadedUserDetail?UserDetailId=" + response.ResultUserDetailMasterGuid).Result;
                        if (responsfinal.IsSuccessStatusCode)
                        {
                            Result = responsfinal.Content.ReadAsStringAsync().Result;

                            finalUserdetails = JsonConvert.DeserializeObject<List<UsersDetailsModel>>(Result);
                        }

                    }


                }
            }
            return Json(new { data = finalUserdetails });
        }


        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public ActionResult GetCandidateDetails(Guid assessmentID)
        {
            List<CandidateDetailsReport> candidateDetailsReport = new List<CandidateDetailsReport>();
            try
            {
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(apiUrl + "/Assessment/GetCandidateDetails?assessmentId=" + assessmentID).Result;
                if (response.IsSuccessStatusCode)
                {
                    var retresult = response.Content.ReadAsStringAsync().Result;
                    candidateDetailsReport = JsonConvert.DeserializeObject<List<CandidateDetailsReport>>(retresult);

                }

            }
            catch (Exception ex)
            {


            }
            return View("CandidateDetails", candidateDetailsReport);
        }

        public ActionResult GetIndividualReport(Guid userID, Guid assessmentID)
        {
            ExamReport examReport = new ExamReport();
            try
            {
                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(apiUrl + "/Assessment/GetIndividualCustomerReport?assessmentId=" + assessmentID + "&userID=" + userID).Result;
                if (response.IsSuccessStatusCode)
                {
                    var retresult = response.Content.ReadAsStringAsync().Result;
                    examReport = JsonConvert.DeserializeObject<ExamReport>(retresult);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            //return new ViewAsPdf("_CustomerInvoice", examReport);
            return PartialView("_CustomerInvoice", examReport);
        }
    }

    public class CandidateFileFields
    {
        public string FieldName { get; set; }
        public string FieldType { get; set; }
    }

    public class EligibilityCriteriaForQuestionBank
    {

        public Guid EligibilityCriteriaGuid { get; set; }
        public Guid QuestionBankId { get; set; }
    }

}