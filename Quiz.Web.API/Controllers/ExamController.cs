using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Quiz.Web.BLL.Exam;

namespace Quiz.Web.API.Controllers
{
    public class ExamController : ApiController
    {
        public string GetPortalLogin(Guid assessmentID)
        {
            ExamBLL examBLL = new ExamBLL();
            var result = examBLL.GetExamPortal(assessmentID);
            return result;
        }

        public string ValidateExaminer(string username, string password, string assessmentID)
        {
            string result = "Failed";
            try
            {

            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}