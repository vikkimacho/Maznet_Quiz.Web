using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Web.DAL.DataAccess;
using Quiz.Web.DTO;
using Quiz.Web.DTO.Models;

namespace Quiz.Web.DAL.Home
{
    public class QuestionBankDAL
    {
        public List<QuestionBankMaster> GetQuestionBank()
        {
            List<QuestionBankMaster> questionBankData = new List<QuestionBankMaster>();
            using (var QuizContext = new TestEngineEntities())
            {
                QuizContext.Configuration.ProxyCreationEnabled = false;
                questionBankData = QuizContext.QuestionBankMasters.Where(x => x.IsDeleted == false).ToList();
            }
            return questionBankData;
        }

        public List<QuestionsDetail> GetQuestionsList(Guid? QuestionBankId)
        {
            List<QuestionsDetail> details = new List<QuestionsDetail>();
            APIResponse response = new APIResponse();
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    details = testEngineEntities.QuestionsDetails.Where(x => x.QuestionBankID == QuestionBankId && x.IsDeleted == false).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return details;
        }

        public QuestionsDetail QuestionsEdit(Guid? QuestionId)
        {
            QuestionsDetail details = new QuestionsDetail();
            APIResponse response = new APIResponse();
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    details = testEngineEntities.QuestionsDetails.Where(x => x.ID == QuestionId && x.IsDeleted == false).FirstOrDefault();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return details;
        }

        public APIResponse QuestionsDelete(Guid? QuestionId)
        {
            APIResponse response = new APIResponse();
            response.Result = false;
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    var data = testEngineEntities.QuestionsDetails.Where(x => x.ID == QuestionId && x.IsDeleted == false).FirstOrDefault();
                    if(data != null)
                    {
                        data.IsDeleted = true;
                        data.ModifiedDate = DateTime.UtcNow;
                        testEngineEntities.SaveChanges();
                        response.Result = true;
                    }                    

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return response;
        }

        public APIResponse UpdateQuestion(QuestionsDetailsView questionsDetailsView)
        {
            APIResponse response = new APIResponse();
            response.Result = false;
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    var data = testEngineEntities.QuestionsDetails.Where(x => x.ID == questionsDetailsView.ID && x.IsDeleted == false).FirstOrDefault();
                    if (data != null)
                    {
                        data.Answer = questionsDetailsView.Answer;
                        data.OptionA = questionsDetailsView.OptionA;
                        data.OptionB = questionsDetailsView.OptionB;
                        data.OptionC = questionsDetailsView.OptionC;
                        data.OptionD = questionsDetailsView.OptionD;
                        data.OptionE = questionsDetailsView.OptionE;
                        data.Question = questionsDetailsView.Question;
                        data.ModifiedDate = DateTime.UtcNow;
                        testEngineEntities.SaveChanges();
                        response.Result = true;
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return response;
        }

        public QuestionBankMaster QuestionsBankEdit(Guid? QuestionBankId)
        {
            QuestionBankMaster details = new QuestionBankMaster();
            APIResponse response = new APIResponse();
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    details = testEngineEntities.QuestionBankMasters.Where(x => x.ID == QuestionBankId && x.IsDeleted == false).FirstOrDefault();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return details;
        }

        public APIResponse QuestionsBankDelete(Guid? QuestionBankId)
        {
            APIResponse response = new APIResponse();
            response.Result = false;
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    var data = testEngineEntities.QuestionBankMasters.Where(x => x.ID == QuestionBankId && x.IsDeleted == false).FirstOrDefault();
                    var questions = testEngineEntities.QuestionsDetails.Where(x => x.ID == QuestionBankId && x.IsDeleted == false).FirstOrDefault();
                    if (data != null)
                    {
                        data.IsDeleted = true;
                        data.ModifiedDate = DateTime.UtcNow;
                        testEngineEntities.SaveChanges();
                        response.Result = true;

                        if(questions!= null)
                        {
                            questions.IsDeleted = true;
                            questions.ModifiedDate = DateTime.UtcNow;
                            testEngineEntities.SaveChanges();
                            response.Result = true;

                        }
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return response;
        }

        public APIResponse UpdateQuestionBank(QuestionBankDetail questionsDetailsView)
        {
            APIResponse response = new APIResponse();
            response.Result = false;
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    var data = testEngineEntities.QuestionBankMasters.Where(x => x.ID == questionsDetailsView.ID && x.IsDeleted == false).FirstOrDefault();
                    if (data != null)
                    {
                        data.QuestionBankDescription = questionsDetailsView.Description;
                        data.Duration = questionsDetailsView.Duration;
                        data.QuestionBankName= questionsDetailsView.QuestionBankName;
                        data.ModifiedDate = DateTime.UtcNow;
                        testEngineEntities.SaveChanges();
                        response.Result = true;
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return response;
        }


        public List<DefaultRegistation> GetUsersDetailList(Guid? UserDetailId)
        {
            List<DefaultRegistation> details = new List<DefaultRegistation>();
            APIResponse response = new APIResponse();
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    details = testEngineEntities.DefaultRegistations.Where(x => x.UserDetailId == UserDetailId).ToList();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return details;
        }


        public APIResponse QuestionBankUpload(QuestionBankDetail questionsDetailsData)
        {
            List<QuestionsDetailsView> QuestionsDetailsView = new List<QuestionsDetailsView>();
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.Result = false;
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    QuestionBankMaster questionBankMaster = new QuestionBankMaster();
                    questionBankMaster.ID = Guid.NewGuid();
                    questionBankMaster.QuestionBankName = questionsDetailsData.QuestionBankName;                    
                    questionBankMaster.CreatedDate = DateTime.UtcNow;
                    questionBankMaster.Duration = questionsDetailsData.Duration;
                    questionBankMaster.IsActive = true;
                    questionBankMaster.IsDeleted = false;
                    questionBankMaster.ModifiedDate = DateTime.UtcNow;
                    questionBankMaster.QuestionBankDescription = questionsDetailsData.Description;                         
                    testEngineEntities.QuestionBankMasters.Add(questionBankMaster);

                    foreach (var item in questionsDetailsData.questionsDetailsViews)
                    {
                        QuestionsDetail questionsDetails = new QuestionsDetail();
                        questionsDetails.Answer = item.Answer;
                        questionsDetails.CreatedDate = DateTime.UtcNow;
                        questionsDetails.ID = Guid.NewGuid();
                        questionsDetails.IsDeleted = false;
                        questionsDetails.ModifiedDate = DateTime.UtcNow;
                        questionsDetails.OptionA = item.OptionA;
                        questionsDetails.OptionB = item.OptionB;
                        questionsDetails.OptionC = item.OptionC;
                        questionsDetails.OptionD = item.OptionD;
                        questionsDetails.OptionE = item.OptionE;
                        questionsDetails.Question = item.Question;
                        questionsDetails.QuestionBankID = questionBankMaster.ID;
                        testEngineEntities.QuestionsDetails.Add(questionsDetails);

                    }
                    testEngineEntities.SaveChanges();
                    aPIResponse.Result = true;
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }


            return aPIResponse;
        }
    }
}
