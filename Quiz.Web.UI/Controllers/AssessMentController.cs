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

namespace Quiz.Web.UI.Controllers
{
    public class AssessMentController : Controller
    {
        Logger logger = new Logger();
        private DataTable dt = new DataTable();
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


        public ActionResult PostCreateAssessment(PostAssessmentModal postAssessmentModal)
        {
            string result = "Failed";
            try
            {
                string postData = JsonConvert.SerializeObject(postAssessmentModal);
                logger.WriteToLogFile("Post Data " + postData);

                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.PostAsJsonAsync(apiUrl + "/Assessment/PostCreateAssessment", postAssessmentModal).Result;
                if (response.IsSuccessStatusCode)
                {
                    var retresult = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(retresult);
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
                    if (result == "Success")
                    {
                        return Json(new { Result = result, ReturnPostEligibileCriteria = lstEligibilityCriterias });
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