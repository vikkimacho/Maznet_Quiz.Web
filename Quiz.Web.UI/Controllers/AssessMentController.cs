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
                string postData = JsonConvert.SerializeObject(lstpostAssessmentModal);
                logger.WriteToLogFile("Post Data " + postData);

                string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.PostAsJsonAsync(apiUrl + "/Assessment/PostUpdateEligibleCriteria", lstpostAssessmentModal).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
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

        



        public ActionResult ImportCandidates(string Status)
        {
            string Message = "";
            string Result = "Failed";
            foreach (string file in Request.Files)
            {

                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                string fileextension = Path.GetExtension(hpf.FileName);
                if (fileextension == ".xlsx" || fileextension == ".xls")
                {
                    dt = ConvertFileToDateTable(hpf);

                    List<CandidateFileFields> lstcolumns = GetColumnNames();
                    var mismatchcolumns = lstcolumns.Where(x => !dt.Columns.Cast<DataColumn>().Any(c => x.FieldName.Trim().ToUpper() == c.ColumnName.Trim().ToUpper())).ToList();
                    if (mismatchcolumns.Count > 0)
                    {
                        Message += "<tr><td></td><td></td><td> " + string.Join(",", mismatchcolumns.Select(x => x.FieldName).ToList()) + "</td><td>" + string.Concat(" => ") + "</td><td>Excel Header Column Name is Mismatching</td></tr>";
                    }
                    else
                    {
                        Message += ValidateDataTableCandidates(dt);

                        if (String.IsNullOrEmpty(Message))
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                var SlNo = dt.Rows[i]["S.no."] != DBNull.Value ? Convert.ToDecimal(dt.Rows[i]["S.no."]) : 0;
                                var UserName = dt.Rows[i]["UserName"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[i]["UserName"]) : 0;
                                var Password = dt.Rows[i]["Password"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[i]["Password"]) : 0;
                                var Email = dt.Rows[i]["Email"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[i]["Email"]) : 0;
                                var Mobile = dt.Rows[i]["Mobile Number"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[i]["Mobile Number"]) : 0;
                                var FirstName = dt.Rows[i]["First Name"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[i]["First Name"]) : 0;
                                var LastName = dt.Rows[i]["Last Name"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[i]["Last Name"]) : 0;
                                var DOB = dt.Rows[i]["DOB(mm/dd/yyyy)"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[i]["DOB(mm/dd/yyyy)"]) : 0;
                                var Tag1 = dt.Rows[i]["Tag1"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[i]["Tag1"]) : 0;
                                var Tag2 = dt.Rows[i]["Tag2"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[i]["Tag2"]) : 0;
                                var Tag3 = dt.Rows[i]["Tag3"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[i]["Tag3"]) : 0;
                                var Tag4 = dt.Rows[i]["Tag4"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[i]["Tag4"]) : 0;
                                var Tag5 = dt.Rows[i]["Tag5"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[i]["Tag5"]) : 0;



                                //Insert into table
                            }


                        }

                    }
                }
            }

            return Json("Success");
        }


        public string ValidateDataTableCandidates(DataTable dt)
        {



            return "";
        }

        public List<CandidateFileFields> GetColumnNames()
        {
            List<CandidateFileFields> lstCandidateFileFields = new List<CandidateFileFields>();

            var objCandidateFileFields1 = new CandidateFileFields();
            objCandidateFileFields1.FieldName = "S.no.";
            objCandidateFileFields1.FieldType = "alphanumeric";
            lstCandidateFileFields.Add(objCandidateFileFields1);

            var objCandidateFileFields2 = new CandidateFileFields();
            objCandidateFileFields1.FieldName = "UserName";
            objCandidateFileFields1.FieldType = "alphanumeric";
            lstCandidateFileFields.Add(objCandidateFileFields2);


            var objCandidateFileFields3 = new CandidateFileFields();
            objCandidateFileFields1.FieldName = "Password";
            objCandidateFileFields1.FieldType = "alphanumeric";
            lstCandidateFileFields.Add(objCandidateFileFields3);


            var objCandidateFileFields4 = new CandidateFileFields();
            objCandidateFileFields1.FieldName = "Email";
            objCandidateFileFields1.FieldType = "alphanumeric";
            lstCandidateFileFields.Add(objCandidateFileFields4);


            var objCandidateFileFields5 = new CandidateFileFields();
            objCandidateFileFields1.FieldName = "Mobile Number";
            objCandidateFileFields1.FieldType = "alphanumeric";
            lstCandidateFileFields.Add(objCandidateFileFields5);


            var objCandidateFileFields6 = new CandidateFileFields();
            objCandidateFileFields1.FieldName = "First Name";
            objCandidateFileFields1.FieldType = "alphanumeric";
            lstCandidateFileFields.Add(objCandidateFileFields6);

            var objCandidateFileFields7 = new CandidateFileFields();
            objCandidateFileFields1.FieldName = "Last Name";
            objCandidateFileFields1.FieldType = "alphanumeric";
            lstCandidateFileFields.Add(objCandidateFileFields7);

            var objCandidateFileFields8 = new CandidateFileFields();
            objCandidateFileFields1.FieldName = "DOB(mm/ dd / yyyy)";
            objCandidateFileFields1.FieldType = "datetime";
            lstCandidateFileFields.Add(objCandidateFileFields8);

            var objCandidateFileFields9 = new CandidateFileFields();
            objCandidateFileFields1.FieldName = "Tag1";
            objCandidateFileFields1.FieldType = "alphanumeric";
            lstCandidateFileFields.Add(objCandidateFileFields9);

            var objCandidateFileFields10 = new CandidateFileFields();
            objCandidateFileFields1.FieldName = "Tag2";
            objCandidateFileFields1.FieldType = "alphanumeric";
            lstCandidateFileFields.Add(objCandidateFileFields10);

            var objCandidateFileFields11 = new CandidateFileFields();
            objCandidateFileFields1.FieldName = "Tag3";
            objCandidateFileFields1.FieldType = "alphanumeric";
            lstCandidateFileFields.Add(objCandidateFileFields11);

            var objCandidateFileFields12 = new CandidateFileFields();
            objCandidateFileFields1.FieldName = "Tag4";
            objCandidateFileFields1.FieldType = "alphanumeric";
            lstCandidateFileFields.Add(objCandidateFileFields12);

            var objCandidateFileFields13 = new CandidateFileFields();
            objCandidateFileFields1.FieldName = "Tag5";
            objCandidateFileFields1.FieldType = "alphanumeric";
            lstCandidateFileFields.Add(objCandidateFileFields13);


            return lstCandidateFileFields;



        }

        public DataTable ConvertFileToDateTable(HttpPostedFileBase hpf)
        {
            if (!String.IsNullOrEmpty(hpf.FileName))
            {
                //string FilePath = System.Configuration.ConfigurationManager.AppSettings["UnitUploadFilePath"].ToString() + hpf.FileName;
                string FilePath = System.Web.Hosting.HostingEnvironment.MapPath(System.Configuration.ConfigurationManager.AppSettings["UnitUploadFilePath"].ToString()) + hpf.FileName;
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

    public class CandidateFileFields
    {
        public string FieldName { get; set; }
        public string FieldType { get; set; }
    }

    public class CandidateColumns
    {


    }
}