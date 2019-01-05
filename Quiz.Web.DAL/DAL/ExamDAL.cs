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

        public string SaveExamAnswers(Guid assesmentID, Guid userID, Guid qusID, string answer)
        {
            string result = "FAILED";
            try
            {
                using (DBEntities dbEntities = new DBEntities())
                {
                    var examinerMaster = dbEntities.ExaminerMasters.FirstOrDefault(x => x.AssessmentId == assesmentID);
                    if (examinerMaster != null)
                    {
                        var examinerDetails = dbEntities.ExaminerMasterDetails.FirstOrDefault(x => x.ExaminerMasterId == examinerMaster.ID && x.UserId == userID);
                        var questionDetails = dbEntities.QuestionsDetails.FirstOrDefault(x => x.ID == qusID);
                        if (examinerDetails != null && questionDetails != null)
                        {
                            var examinerAssessmentDetail = dbEntities.ExaminerAssessmentDetails.FirstOrDefault(x => x.ExaminerMasterDetailId == examinerMaster.ID && x.QuestionBankID == questionDetails.QuestionBankID);
                            if (examinerAssessmentDetail != null)
                            {
                                var examinerQuestionExisting = dbEntities.ExaminerQuestionDetails.FirstOrDefault(x => x.ExaminerAssessmentDetailId == examinerAssessmentDetail.ID && x.QuestionId == qusID);
                                if (examinerQuestionExisting == null)
                                {
                                    bool ansStatus = questionDetails.Answer.Trim().ToUpper() == answer.Trim().ToUpper() ? true : false;
                                    ExaminerQuestionDetail examinerQuestion = new ExaminerQuestionDetail();
                                    examinerQuestion.Answer = answer;
                                    examinerQuestion.AnswerStatus = ansStatus;
                                    examinerQuestion.CreatedDate = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                                    examinerQuestion.ExaminerAssessmentDetailId = examinerAssessmentDetail.ID;
                                    examinerQuestion.ModifiedDate = null;
                                    examinerQuestion.QuestionId = questionDetails.ID;
                                    dbEntities.ExaminerQuestionDetails.Add(examinerQuestion);
                                    dbEntities.SaveChanges();
                                    result = "SUCCESS";                                    
                                }
                                else
                                {
                                    examinerQuestionExisting.Answer = answer;
                                    examinerQuestionExisting.ModifiedDate= DateTime.UtcNow.AddHours(5).AddMinutes(30);
                                    dbEntities.SaveChanges();
                                    result = "SUCCESS";
                                }
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public List<QuestionsDetail> GetAssesmentQuestions(Guid assesmentID)
        {
            List<QuestionsDetail> questionlist = new List<QuestionsDetail>();
            try
            {
                using (DBEntities dbEntities = new DBEntities())
                {
                    questionlist = (from x in dbEntities.QuestionsDetails
                                 join y in dbEntities.AssessmentQuestionBankDetails on x.QuestionBankID equals y.QuestionBankID
                                 where x.IsDeleted == false && y.IsDeleted == false && y.AssessmentID == assesmentID
                                 select x).ToList();


                    
                 }
            }
            catch (Exception ex)
            {

            }
            return questionlist;
        }

    }
}
