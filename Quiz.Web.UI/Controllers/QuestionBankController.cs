using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Excel;
using Quiz.Web.DTO.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Quiz.Web.UI.Helper;
using System.Configuration;

namespace Quiz.Web.UI.Controllers
{
    [AuthorizationFilter]
    public class QuestionBankController : Controller
    {
        private static readonly string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
        private APIResponse APIResponse = new APIResponse();
        Logger logger = new Logger();

        // GET: QuestionBank
        public ActionResult QuestionBank()
        {
            List<QuestionBankDetail> QuestionBank = new List<QuestionBankDetail>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.GetAsync(apiUrl + "/QuestionBank/GetQuestionBank").Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    QuestionBank = JsonConvert.DeserializeObject<List<QuestionBankDetail>>(Result);
                }
            }
            return View(QuestionBank);
        }

        public ActionResult QuestionsList(Guid? QuestionBankId)
        {
            List<QuestionsDetailsView> questionsDetailsView = new List<QuestionsDetailsView>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.GetAsync(apiUrl + "/QuestionBank/GetQuestionsList?QuestionBankId=" + QuestionBankId).Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    questionsDetailsView = JsonConvert.DeserializeObject<List<QuestionsDetailsView>>(Result);
                }
            }
            return View(questionsDetailsView);
        }

        public ActionResult QuestionsEdit(Guid? QuestionId)
        {
            QuestionsDetailsView questionsDetailsView = new QuestionsDetailsView();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.GetAsync(apiUrl + "/QuestionBank/QuestionsEdit?QuestionId=" + QuestionId).Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    questionsDetailsView = JsonConvert.DeserializeObject<QuestionsDetailsView>(Result);
                }
            }
            return Json(questionsDetailsView, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QuestionsBankEdit(Guid? QuestionBankId)
        {
            QuestionBankDetail questionsDetailsView = new QuestionBankDetail();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.GetAsync(apiUrl + "/QuestionBank/QuestionsBankEdit?QuestionBankId=" + QuestionBankId).Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    questionsDetailsView = JsonConvert.DeserializeObject<QuestionBankDetail>(Result);
                }
            }
            return Json(questionsDetailsView, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateQuestion(QuestionsDetailsView questionsDetailsView)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.PostAsJsonAsync(apiUrl + "/QuestionBank/UpdateQuestion", questionsDetailsView).Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    APIResponse = JsonConvert.DeserializeObject<APIResponse>(Result);
                }
            }
            return Json(APIResponse, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UpdateQuestionBank(QuestionBankDetail questionsDetailsView)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.PostAsJsonAsync(apiUrl + "/QuestionBank/UpdateQuestionBank", questionsDetailsView).Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    APIResponse = JsonConvert.DeserializeObject<APIResponse>(Result);
                }
            }
            return Json(APIResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QuestionsDelete(Guid? QuestionId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.GetAsync(apiUrl + "/QuestionBank/QuestionsDelete?QuestionId=" + QuestionId).Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    APIResponse = JsonConvert.DeserializeObject<APIResponse>(Result);
                }
            }
            return Json(APIResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QuestionsBankDelete(Guid? QuestionBankId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var result = client.GetAsync(apiUrl + "/QuestionBank/QuestionsBankDelete?QuestionBankId=" + QuestionBankId).Result;
                if (result.IsSuccessStatusCode)
                {
                    var Result = result.Content.ReadAsStringAsync().Result;

                    APIResponse = JsonConvert.DeserializeObject<APIResponse>(Result);
                }
            }
            return Json(APIResponse, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult UploadQuestionBank(string QuestionBankName, string Duration, string Description, bool Status)
        {
            APIResponse response = new APIResponse();
            int duplicates = 0;
            int NoAnswer = 0;
            int NoOption = 0;
            int NoQuestion = 0;
            int MasterQuestionId = 1000;
            try
            {
                string Result = "Failed";
                logger.WriteToLogFile("UploadQuestionBank - Starts");

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    string fileextension = Path.GetExtension(hpf.FileName);
                    QuestionBankDetail questionBankDetail = new QuestionBankDetail();
                    questionBankDetail.questionsDetailsViews = new List<QuestionsDetailsView>();
                    if (fileextension == ".xlsx" || fileextension == ".xls" || fileextension == ".csv")
                    {
                        var dt = Common.ConvertFileToDateTable(hpf, "QuestionUploadFile");
                        List<QuestionsDetailsView> questionsDetailsView = new List<QuestionsDetailsView>();
                        var rowCount = dt.Rows.Count;
                        for (int i = 0; i < rowCount; i++)
                        {
                            QuestionsDetailsView questionDetail = new QuestionsDetailsView();
                            if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[0].ToString()))
                            {
                                string MasterQuestion = dt.Rows[i]["MasterQuestion"] != DBNull.Value ? dt.Rows[i]["MasterQuestion"].ToString().Trim().ToUpper() : string.Empty;
                                questionDetail.MasterQuestion = MasterQuestion.Trim();
                            }
                            string Question = dt.Rows[i]["Question"] != DBNull.Value ? dt.Rows[i]["Question"].ToString().Trim().ToUpper() : string.Empty;
                            questionDetail.Question = Question.Trim();
                            string OptionA = dt.Rows[i]["OptionA"] != DBNull.Value ? dt.Rows[i]["OptionA"].ToString().Trim().ToUpper() : string.Empty;
                            questionDetail.OptionA = OptionA.Trim();
                            string OptionB = dt.Rows[i]["OptionB"] != DBNull.Value ? dt.Rows[i]["OptionB"].ToString().Trim().ToUpper() : string.Empty;
                            questionDetail.OptionB = OptionB.Trim();
                            string OptionC = dt.Rows[i]["OptionC"] != DBNull.Value ? dt.Rows[i]["OptionC"].ToString().Trim().ToUpper() : string.Empty;
                            questionDetail.OptionC = OptionC.Trim();
                            string OptionD = dt.Rows[i]["OptionD"] != DBNull.Value ? dt.Rows[i]["OptionD"].ToString().Trim().ToUpper() : string.Empty;
                            questionDetail.OptionD = OptionD.Trim();
                            string OptionE = dt.Rows[i]["OptionE"] != DBNull.Value ? dt.Rows[i]["OptionE"].ToString().Trim().ToUpper() : string.Empty;
                            questionDetail.OptionE = OptionE.Trim();
                            string Answer = dt.Rows[i]["Answer"] != DBNull.Value ? dt.Rows[i]["Answer"].ToString().Trim().ToUpper() : string.Empty;
                            questionDetail.Answer = Answer.Trim();
                            if (questionDetail != null)
                            {
                                if (questionsDetailsView.Any(x => x.Question.Trim().ToUpper() == questionDetail.Question.Trim().ToUpper()))
                                {
                                    duplicates++;
                                }
                                else if (string.IsNullOrEmpty(questionDetail.Answer))
                                {
                                    NoAnswer++;
                                }
                                else if(string.IsNullOrEmpty(questionDetail.OptionA) && string.IsNullOrEmpty(questionDetail.OptionB) && string.IsNullOrEmpty(questionDetail.OptionC) && string.IsNullOrEmpty(questionDetail.OptionD) && string.IsNullOrEmpty(questionDetail.OptionE))
                                {
                                    NoOption++;
                                }
                                else if (string.IsNullOrEmpty(questionDetail.Question))
                                {
                                    NoQuestion++;
                                }                                
                                else
                                {
                                    questionsDetailsView.Add(questionDetail);
                                }
                                
                            }
                        }

                        var duplicateserror = "Duplicates Questions : " + duplicates;
                        var noanserror = "Question without Answer : " + NoAnswer;
                        var nooptionerror = "Question without Option : " + NoOption;
                        var noquestionerror = "Question without Question : " + NoQuestion;
                        var ResultMessage = "Question bank Upload Failed";
                        var AlertMessages = "";
                        if (duplicates > 0 && NoAnswer == 0 && NoOption == 0 && NoQuestion ==0)
                        {
                            AlertMessages = " => " + duplicateserror;
                        }
                        else if (duplicates == 0 && NoAnswer > 0 && NoOption == 0 && NoQuestion == 0)
                        {
                            AlertMessages = " => " + noanserror;
                        }
                        else if (duplicates == 0 && NoAnswer == 0 && NoOption > 0 && NoQuestion == 0)
                        {
                            AlertMessages = " => " + nooptionerror;
                        }
                        else if (duplicates == 0 && NoAnswer == 0 && NoOption == 0 && NoQuestion > 0)
                        {
                            AlertMessages = " => " + nooptionerror;
                        }
                        else if (duplicates > 0 && NoAnswer > 0 && NoOption == 0 && NoQuestion == 0)
                        {
                            AlertMessages = " => " + duplicateserror + " || " + noanserror;
                        }
                        else if (duplicates > 0 && NoAnswer == 0 && NoOption > 0 && NoQuestion == 0)
                        {
                            AlertMessages = " => " + duplicateserror + " || " + nooptionerror;
                        }
                        else if (duplicates > 0 && NoAnswer == 0 && NoOption == 0 && NoQuestion > 0)
                        {
                            AlertMessages = " => " + duplicateserror + " || " + noquestionerror;
                        }
                        else if (duplicates == 0 && NoAnswer > 0 && NoOption == 0 && NoQuestion > 0)
                        {
                            AlertMessages = " => " + noanserror + " || " + noquestionerror;
                        }
                        else if (duplicates == 0 && NoAnswer == 0 && NoOption > 0 && NoQuestion > 0)
                        {
                            AlertMessages = " => " + nooptionerror + " || " + noquestionerror;
                        }
                        else if (duplicates > 0 && NoAnswer > 0 && NoOption > 0 && NoQuestion == 0)
                        {
                            AlertMessages = " => " + duplicateserror + " || " + noanserror + " || " + nooptionerror;
                        }
                        else if (duplicates > 0 && NoAnswer > 0 && NoOption == 0 && NoQuestion > 0)
                        {
                            AlertMessages = " => " + duplicateserror + " || " + noanserror + " || " + noquestionerror;
                        }
                        else if (duplicates > 0 && NoAnswer == 0 && NoOption > 0 && NoQuestion > 0)
                        {
                            AlertMessages = " => " + duplicateserror + " || " + nooptionerror + " || " + noquestionerror;
                        }
                        else if (duplicates == 0 && NoAnswer > 0 && NoOption > 0 && NoQuestion > 0)
                        {
                            AlertMessages = " => " + nooptionerror + " || " + noanserror + " || " + noquestionerror;
                        }
                        else if (duplicates > 0 && NoAnswer > 0 && NoOption > 0 && NoQuestion > 0)
                        {
                            AlertMessages = " => " + duplicateserror + " || " + noanserror + " || " + noquestionerror + " || " + nooptionerror;
                        }
                        if (questionsDetailsView.Any())
                        {
                            questionBankDetail.questionsDetailsViews = questionsDetailsView;
                            using (var client = new HttpClient())
                            {
                                questionBankDetail.Description = Description;
                                questionBankDetail.QuestionBankName = QuestionBankName;
                                questionBankDetail.Status = Status;
                                var splitData = Duration.Split(':');
                                int hour = splitData.Length > 0 ? Convert.ToInt32(splitData[0]) : 0;
                                int minutes = splitData.Length > 1 ? Convert.ToInt32(splitData[1]) : 0;
                                int sec = splitData.Length > 2 ? Convert.ToInt32(splitData[2]) : 0;
                                questionBankDetail.Duration = new TimeSpan(hour, minutes, sec);
                                var masterlist = questionBankDetail.questionsDetailsViews.Where(x => !string.IsNullOrEmpty(x.MasterQuestion)).ToList();
                                questionBankDetail.questionsDetailsViews = questionBankDetail.questionsDetailsViews.Where(x => string.IsNullOrEmpty(x.MasterQuestion)).ToList();
                                foreach (var items in masterlist)
                                {
                                    for(var i = 0; i <= masterlist.Count - 1; i++)
                                    {
                                        if(items.MasterQuestion == masterlist[i].MasterQuestion)
                                        {
                                            items.MasterQuestionId = MasterQuestionId;
                                        }
                                    }
                                    MasterQuestionId++;

                                }

                                questionBankDetail.questionsDetailsViews.AddRange(masterlist);

                                if (questionBankDetail.questionsDetailsViews.Count > 0)
                                {
                                    client.BaseAddress = new Uri(apiUrl);
                                    var result = client.PostAsJsonAsync(apiUrl + "/QuestionBank/UploadQuestionBank", questionBankDetail).Result;
                                    if (result.IsSuccessStatusCode)
                                    {
                                        Result = result.Content.ReadAsStringAsync().Result;

                                        response = JsonConvert.DeserializeObject<APIResponse>(Result);
                                        ResultMessage = "Question bank Uploaded Succesfully";

                                    }                                    
                                }
                            }

                        }

                        response.Message = ResultMessage + AlertMessages;

                    }
                }
            }
            catch (Exception ex)
            {
                logger.WriteToLogFile("UploadQuestionBank - " + ex.ToString());
                if (ex.InnerException != null)
                {
                    logger.WriteToLogFile("UploadQuestionBank InnerException - " + ex.ToString());
                }
            }

            return this.Json(new { data = response });
        }

        [HttpGet]
        public ActionResult DownloadTemplate()
        {
            string FilePath = "";
            try
            {
                string UserTemplate = ConfigurationManager.AppSettings["QuestionBankTemplate"];
                FilePath = System.Web.Hosting.HostingEnvironment.MapPath(UserTemplate);
            }
            catch (Exception ex)
            {

            }
            return File(FilePath, "application/vnd.ms-excel", Path.GetFileName(FilePath));
        }




    }
}