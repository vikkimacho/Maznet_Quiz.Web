using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Web.DAL.DataAccess;
using Quiz.Web.DTO.Models;

namespace Quiz.Web.DAL.DAL
{
    public class ExamDAL
    {
        public List<ExamAssessmentDetails> ValidateAssessmentID(Guid AssessmentID)
        {
            List<ExamAssessmentDetails> examAssessmentDetails = new List<ExamAssessmentDetails>();
            try
            {
                using (var dbContext = new DBEntities())
                {
                    examAssessmentDetails = dbContext.Database.SqlQuery<ExamAssessmentDetails>("Exec ValidateAssessment @AssessmentID"
                           , new SqlParameter("@AssessmentID", AssessmentID)
                           ).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return examAssessmentDetails;
        }

        public string ValidateExaminer(string username, string password, Guid assessmentID)
        {
            string result = "FAILED";
            try
            {
                using (var dbContext = new DBEntities())
                {
                    var validateResult = dbContext.Database.SqlQuery<string>("Exec ValidateExaminer @AssessmentID, @Username, @Password"
                           , new SqlParameter("@AssessmentID", assessmentID)
                           , new SqlParameter("@Username", username)
                           , new SqlParameter("@Password", password)
                           ).FirstOrDefault();
                    if (!string.IsNullOrEmpty(validateResult))
                    {
                        result = "SUCCESS";
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        public List<CandidateAssesmentDetailsForm> GetRegistration(Guid assessmentID)
        {
            List<CandidateAssesmentDetailsForm> list = new List<CandidateAssesmentDetailsForm>();
            try
            {
                using (var dbContext = new DBEntities())
                {
                    list = dbContext.CandidateAssesmentDetailsForms.Where(x => x.AssessmentId == assessmentID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return list;
        }
    }
}
