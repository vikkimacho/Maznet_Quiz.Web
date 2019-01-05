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


        public APIResponse ValidateExaminer(string username, string password, Guid assessmentID)
        {
            APIResponse result = new APIResponse();
            result.Message = "FAILED";
            try
            {
                using (var dbContext = new DBEntities())
                {
                    var validateResult = dbContext.Database.SqlQuery<Guid>("Exec ValidateExaminer @AssessmentID, @Username, @Password"
                           , new SqlParameter("@AssessmentID", assessmentID)
                           , new SqlParameter("@Username", username)
                           , new SqlParameter("@Password", password)
                           ).FirstOrDefault();
                        result.Result = true;
                        result.Message = "SUCCESS";
                        result.Guid = validateResult;
                    
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
