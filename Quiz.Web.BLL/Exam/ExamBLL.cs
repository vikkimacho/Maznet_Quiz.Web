using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Web.DAL.DAL;
using Quiz.Web.DTO.Models;

namespace Quiz.Web.BLL.Exam
{
    public class ExamBLL
    {
        ExamDAL examDAL = new ExamDAL();
        public string GetExamPortal(Guid assessmentID)
        {
            string result = "Failed";
            try
            {                
                var examAssessmentDetail = examDAL.ValidateAssessmentID(assessmentID);
                if (examAssessmentDetail.Count() > 0)
                {
                    result = "Success";
                }
            }
            catch (Exception)
            {
                
            }
            return result;
        }
    }
}
