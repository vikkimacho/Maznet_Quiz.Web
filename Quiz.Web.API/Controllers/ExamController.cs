using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Quiz.Web.BLL.Exam;
using Quiz.Web.DTO.Models;

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

        [HttpGet]
        public string ValidateExaminer(string username, string password, string assessmentID)
        {
            string result = "Failed";
            try
            {
                ExamBLL examBLL = new ExamBLL();
                result = examBLL.ValidateExaminer(username, password, assessmentID);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        [HttpGet]
        public List<CustomRegistration> GetRegistration( string assessmentID)
        {
            List<CustomRegistration> result = new List<CustomRegistration>();
            try
            {
                ExamBLL examBLL = new ExamBLL();
                result = examBLL.GetRegistration(assessmentID);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}