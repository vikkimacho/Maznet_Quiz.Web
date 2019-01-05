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
        private DateTime dateTime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
        public List<QuestionBankMaster> GetQuestionBank()
        {
            List<QuestionBankMaster> questionBankData = new List<QuestionBankMaster>();
            using (var QuizContext = new DBEntities())
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
                using (DBEntities testEngineEntities = new DBEntities())
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
                using (DBEntities testEngineEntities = new DBEntities())
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
            dateTime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            response.Result = false;
            try
            {
                using (DBEntities testEngineEntities = new DBEntities())
                {
                    var data = testEngineEntities.QuestionsDetails.Where(x => x.ID == QuestionId && x.IsDeleted == false).FirstOrDefault();
                    if(data != null)
                    {
                        data.IsDeleted = true;
                        data.ModifiedDate = dateTime;;
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
                using (DBEntities testEngineEntities = new DBEntities())
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
                        data.MasterQuestion = questionsDetailsView.MasterQuestion;
                        data.ModifiedDate = dateTime;;
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
                using (DBEntities testEngineEntities = new DBEntities())
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
            dateTime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            response.Result = false;
            try
            {
                using (DBEntities testEngineEntities = new DBEntities())
                {
                    var data = testEngineEntities.QuestionBankMasters.Where(x => x.ID == QuestionBankId && x.IsDeleted == false).FirstOrDefault();
                    var questions = testEngineEntities.QuestionsDetails.Where(x => x.ID == QuestionBankId && x.IsDeleted == false).FirstOrDefault();
                    if (data != null)
                    {
                        data.IsDeleted = true;
                        data.ModifiedDate = dateTime;
                        testEngineEntities.SaveChanges();
                        response.Result = true;

                        if(questions!= null)
                        {
                            questions.IsDeleted = true;
                            questions.ModifiedDate = dateTime;
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
            dateTime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            response.Result = false;
            try
            {
                using (DBEntities testEngineEntities = new DBEntities())
                {
                    var data = testEngineEntities.QuestionBankMasters.Where(x => x.ID == questionsDetailsView.ID && x.IsDeleted == false).FirstOrDefault();
                    if (data != null)
                    {
                        data.QuestionBankDescription = questionsDetailsView.Description;
                        data.Duration = questionsDetailsView.Duration;
                        data.QuestionBankName= questionsDetailsView.QuestionBankName;
                        data.ModifiedDate = dateTime;
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
                using (DBEntities testEngineEntities = new DBEntities())
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
            dateTime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            aPIResponse.Result = false;
            try
            {
                using (DBEntities testEngineEntities = new DBEntities())
                {
                    QuestionBankMaster questionBankMaster = new QuestionBankMaster();
                    questionBankMaster.ID = Guid.NewGuid();
                    questionBankMaster.QuestionBankName = questionsDetailsData.QuestionBankName;                    
                    questionBankMaster.CreatedDate = dateTime;;
                    questionBankMaster.Duration = questionsDetailsData.Duration;
                    questionBankMaster.IsActive = true;
                    questionBankMaster.IsDeleted = false;
                    questionBankMaster.ModifiedDate = dateTime;;
                    questionBankMaster.QuestionBankDescription = questionsDetailsData.Description;                         
                    testEngineEntities.QuestionBankMasters.Add(questionBankMaster);

                    foreach (var item in questionsDetailsData.questionsDetailsViews)
                    {
                        bool IsMaster = false;
                        QuestionsDetail questionsDetails = new QuestionsDetail();
                        questionsDetails.Answer = item.Answer;
                        questionsDetails.CreatedDate = dateTime;;
                        questionsDetails.ID = Guid.NewGuid();
                        questionsDetails.IsDeleted = false;
                        questionsDetails.ModifiedDate = dateTime;;
                        questionsDetails.OptionA = item.OptionA;
                        questionsDetails.OptionB = item.OptionB;
                        questionsDetails.OptionC = item.OptionC;
                        questionsDetails.OptionD = item.OptionD;
                        questionsDetails.OptionE = item.OptionE;
                        questionsDetails.MasterQuestion = item.MasterQuestion;
                        questionsDetails.Question = item.Question;
                        questionsDetails.QuestionBankID = questionBankMaster.ID;
                        if (!string.IsNullOrEmpty(item.MasterQuestion))
                        {
                            IsMaster = true;
                        }
                        questionsDetails.IsMaster = IsMaster;
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
