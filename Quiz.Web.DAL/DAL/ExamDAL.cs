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
                    if(validateResult.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        result.Result = true;
                        result.Message = "SUCCESS";
                        result.Guid = validateResult;
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
                            var examinerAssessmentDetail = dbEntities.ExaminerAssessmentDetails.FirstOrDefault(x => x.ExaminerMasterDetailId == examinerDetails.ID && x.QuestionBankID == questionDetails.QuestionBankID);
                            if (examinerAssessmentDetail != null)
                            {
                                var examinerQuestionExisting = dbEntities.ExaminerQuestionDetails.FirstOrDefault(x => x.ExaminerAssessmentDetailId == examinerAssessmentDetail.ID && x.QuestionId == qusID);
                                if (examinerQuestionExisting == null)
                                {
                                    bool ansStatus = questionDetails.Answer.Trim().ToUpper() == answer.Trim().ToUpper() ? true : false;
                                    ExaminerQuestionDetail examinerQuestion = new ExaminerQuestionDetail();
                                    examinerQuestion.ID = Guid.NewGuid();
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
                            else
                            {
                                ExaminerAssessmentDetail examinerAssessment = new ExaminerAssessmentDetail();
                                examinerAssessment.CreatedDate = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                                examinerAssessment.ExaminerMasterDetailId = examinerDetails.ID;
                                examinerAssessment.QuestionBankID = questionDetails.QuestionBankID;
                                examinerAssessment.ID = Guid.NewGuid();
                                examinerAssessment.ModifiedDate = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                                dbEntities.ExaminerAssessmentDetails.Add(examinerAssessment);
                                dbEntities.SaveChanges();
                                result = SaveExamAnswers(assesmentID, userID, qusID, answer);
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

        public APIResponse SubmitExam(Guid assesmentID, Guid userID)
        {
            APIResponse result = new APIResponse();
            try
            {
                using (DBEntities dbEntities = new DBEntities())
                {
                    var SubmitExam = dbEntities.DefaultRegistations.FirstOrDefault(x => x.ID == userID);
                    SubmitExam.IsExamCompleted = true;
                    dbEntities.SaveChanges();
                    result.Result = true;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public List<QuestionsDetail> GetAssesmentQuestions(Guid assesmentID,Guid UserID)
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

        public Questions GetAssesmentQuestionDetail(Guid assesmentID, Guid UserID, Guid QuestionId)
        {
            Questions questions = new Questions();
            try
            {
                using (DBEntities dbEntities = new DBEntities())
                {
                    var Answer = (from x in dbEntities.ExaminerMasters
                                           join y in dbEntities.ExaminerMasterDetails on x.ID equals y.ExaminerMasterId
                                           join z in dbEntities.ExaminerAssessmentDetails on y.ID equals z.ExaminerMasterDetailId
                                           join a in dbEntities.ExaminerQuestionDetails on z.ID equals a.ExaminerAssessmentDetailId
                                           where x.AssessmentId == assesmentID && y.UserId == UserID && a.QuestionId == QuestionId
                                           select a).ToList();

                    questions.Answer = Answer.FirstOrDefault().Answer;
                    questions.ID = Answer.FirstOrDefault().QuestionId;
                }

                

            }
            catch (Exception ex)
            {

            }
            return questions;
        }

    }
}
