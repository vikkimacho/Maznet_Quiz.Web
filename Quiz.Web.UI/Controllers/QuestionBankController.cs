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

namespace Quiz.Web.UI.Controllers
{
    public class QuestionBankController : Controller
    {
        private static readonly string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
        private DataTable dt = new DataTable();
        // GET: QuestionBank
        public ActionResult QuestionBank()
        {
            IEnumerable<QuestionBankView> questionBankMasters = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("QuestionBank");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<QuestionBankView>>();
                    readTask.Wait();

                    questionBankMasters = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    questionBankMasters = Enumerable.Empty<QuestionBankView>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(questionBankMasters);
        }

        public ActionResult UploadQuestionBank(string QuestionBankName, string Duration, string Description, bool Status)
        {
            string Result = "Failed";

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                string fileextension = Path.GetExtension(hpf.FileName);
                var questionBankDetail = new QuestionBankDetail();
                if (fileextension == ".xlsx" || fileextension == ".xls" || fileextension == ".csv")
                {
                    dt = ConvertFileToDateTable(hpf);
                    List<QuestionsDetailsView> questionsDetailsView = new List<QuestionsDetailsView>();
                    var rowCount = dt.Rows.Count;
                    for (int i = 0; i < rowCount; i++)
                    {
                        QuestionsDetailsView questionDetail = new QuestionsDetailsView();
                        string Question = dt.Rows[i]["Question"] != DBNull.Value ? dt.Rows[i]["Question"].ToString().Trim().ToUpper() : string.Empty;
                        questionDetail.Question = Question;
                        string OptionA = dt.Rows[i]["OptionA"] != DBNull.Value ? dt.Rows[i]["OptionA"].ToString().Trim().ToUpper() : string.Empty;
                        questionDetail.OptionA = OptionA;
                        string OptionB = dt.Rows[i]["OptionB"] != DBNull.Value ? dt.Rows[i]["OptionB"].ToString().Trim().ToUpper() : string.Empty;
                        questionDetail.OptionB = OptionB;
                        string OptionC = dt.Rows[i]["OptionC"] != DBNull.Value ? dt.Rows[i]["OptionC"].ToString().Trim().ToUpper() : string.Empty;
                        questionDetail.OptionC = OptionC;
                        string OptionD = dt.Rows[i]["OptionD"] != DBNull.Value ? dt.Rows[i]["OptionD"].ToString().Trim().ToUpper() : string.Empty;
                        questionDetail.OptionC = OptionD;
                        string Answer = dt.Rows[i]["Answer"] != DBNull.Value ? dt.Rows[i]["Answer"].ToString().Trim().ToUpper() : string.Empty;
                        questionDetail.Answer = Answer;
                        if (questionDetail != null)
                        {
                            questionsDetailsView.Add(questionDetail);
                        }
                    }
                    if (questionsDetailsView.Any())
                    {
                        using (var client = new HttpClient())
                        {
                            questionBankDetail.Description = Description;
                            questionBankDetail.QuestionBankName = QuestionBankName;
                            questionBankDetail.Status = Status;
                            questionBankDetail.Duration = new TimeSpan(0, 0, 13, 0, 0);
                            client.BaseAddress = new Uri(apiUrl);
                            var jsonSerialiser = new JavaScriptSerializer();
                            var json = JsonConvert.SerializeObject(questionsDetailsView);
                            var responseTask = client.GetAsync("QuestionBankUpload?QuestionsDetailsView=" + questionsDetailsView);
                            responseTask.Wait();
                            var result = responseTask.Result;
                            if (result.IsSuccessStatusCode)
                            {

                            }
                            else //web api sent error response 
                            {
                                //log response status here..


                            }
                        }

                    }
                }
            }
            return Json("");
        }



        public DataTable ConvertFileToDateTable(HttpPostedFileBase hpf)
        {
            if (!String.IsNullOrEmpty(hpf.FileName))
            {
                //string FilePath = System.Configuration.ConfigurationManager.AppSettings["UnitUploadFilePath"].ToString() + hpf.FileName;
                string FilePath = System.Web.Hosting.HostingEnvironment.MapPath(System.Configuration.ConfigurationManager.AppSettings["QuestionUploadFile"].ToString()) + hpf.FileName;
                int count = 1;
                string newFullPath = FilePath;
                while (System.IO.File.Exists(newFullPath))
                {
                    string tempFileName = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FilePath), count++);
                    newFullPath = Path.Combine(Path.GetDirectoryName(FilePath), tempFileName + Path.GetExtension(FilePath));
                }
                hpf.SaveAs(newFullPath);
                FileStream stream = new FileStream(newFullPath, FileMode.Open, FileAccess.ReadWrite);
                hpf.InputStream.CopyTo(stream);

                string fileExtension = Path.GetExtension(hpf.FileName);
                IExcelDataReader excelReader;
                excelReader = fileExtension == ".xlsx" ? ExcelReaderFactory.CreateOpenXmlReader(stream) : ExcelReaderFactory.CreateBinaryReader(stream);
                stream.Close();
                excelReader.IsFirstRowAsColumnNames = true;

                DataSet ds = excelReader.AsDataSet();
                dt = ds.Tables[0];
                excelReader.Close();
                dt = dt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull || string.Compare((Convert.ToString(field)).Trim(), string.Empty) == 0)).CopyToDataTable();
                //dt = dt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull || string.Compare((field as string).Trim(), string.Empty) == 0)).CopyToDataTable();
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                foreach (DataColumn column in dt.Columns)
                    column.ColumnName = ti.ToTitleCase(column.ColumnName);
            }
            return dt;
        }

    }
}