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
                using (var dbContext = new TestEngineEntities())
                {
                    examAssessmentDetails = dbContext.Database.SqlQuery<ExamAssessmentDetails>("Exec ValidateAssessment @AssessmentID"
                           , new SqlParameter("@AssessmentID", AssessmentID)
                           ).ToList();
                }
            }
            catch(Exception ex)
            {
                
            }
            return examAssessmentDetails;
        }
    }
}
