using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Web.DAL;
using Quiz.Web.DAL.Home;
using Quiz.Web.DTO.Models;
using Newtonsoft.Json;

namespace Quiz.Web.BLL.Home
{
    public class QuestionBankBLL
    {
        QuestionBankDAL objQuestion = new QuestionBankDAL();
        APIResponse aPIResponse = new APIResponse();
        public List<QuestionBankDetail> GetExistingQuestionBank()
        {
            List<QuestionBankDetail> QuestionBankDetail = new List<QuestionBankDetail>();
            var QuestionBankData = objQuestion.GetQuestionBank();
            if (QuestionBankData.Count > 0)
            {
                QuestionBankData.ForEach(x =>
                {
                    QuestionBankDetail detail = new QuestionBankDetail();
                    detail.Description = x.QuestionBankDescription;
                    detail.Duration = x.Duration;
                    detail.QuestionBankName = x.QuestionBankName;
                    detail.ID = x.ID;
                    QuestionBankDetail.Add(detail);
                });

            }

            return QuestionBankDetail;
        }

        public List<QuestionsDetailsView> GetQuestionsList(Guid? QuestionId)
        {
            List<QuestionsDetailsView> detaillist = new List<QuestionsDetailsView>();
            try
            {
                var data = objQuestion.GetQuestionsList(QuestionId);
                if (data.Count > 0)
                {
                    data.ForEach(x =>
                    {
                        QuestionsDetailsView detail = new QuestionsDetailsView();
                        detail.ID = x.ID;
                        detail.Question = x.Question;
                        detail.Answer = x.Answer;
                        detail.ModifiedDate = x.ModifiedDate;
                        detail.OptionA = x.OptionA;
                        detail.OptionB = x.OptionB;
                        detail.OptionC = x.OptionC;
                        detail.OptionD = x.OptionD;
                        detail.OptionE = x.OptionE;
                        detail.QuestionBankID = x.QuestionBankID;
                        detaillist.Add(detail);
                    });

                }
            }
            catch (Exception)
            {
                throw;
            }
            return detaillist;
        }

        public APIResponse QuestionsDelete(Guid? QuestionId)
        {
            try
            {
                aPIResponse = objQuestion.QuestionsDelete(QuestionId);
                
            }
            catch (Exception)
            {
                throw;
            }
            return aPIResponse;
        }

        public QuestionsDetailsView QuestionsEdit(Guid? QuestionId)
        {

            QuestionsDetailsView detail = new QuestionsDetailsView();
            try
            {
                var data = objQuestion.QuestionsEdit(QuestionId);               
                detail.ID = data.ID;
                detail.Question = data.Question;
                detail.Answer = data.Answer;
                detail.ModifiedDate = data.ModifiedDate;
                detail.OptionA = data.OptionA;
                detail.OptionB = data.OptionB;
                detail.OptionC = data.OptionC;
                detail.OptionD = data.OptionD;
                detail.OptionE = data.OptionE;
                detail.QuestionBankID = data.QuestionBankID;
                 
            }
            catch (Exception)
            {
                throw;
            }
            return detail;
        }

        public APIResponse UpdateQuestion(QuestionsDetailsView questionsDetailsView)
        {
            APIResponse aPIResponse = new APIResponse();
            QuestionBankDAL questionBankDAL = new QuestionBankDAL();
            aPIResponse = questionBankDAL.UpdateQuestion(questionsDetailsView);

            return aPIResponse;
        }


        public APIResponse QuestionsBankDelete(Guid? QuestionBankId)
        {
            try
            {
                aPIResponse = objQuestion.QuestionsBankDelete(QuestionBankId);

            }
            catch (Exception)
            {
                throw;
            }
            return aPIResponse;
        }

        public QuestionBankDetail QuestionsBankEdit(Guid? QuestionBankId)
        {

            QuestionBankDetail detail = new QuestionBankDetail();
            try
            {
                var data = objQuestion.QuestionsBankEdit(QuestionBankId);
                detail.ID = data.ID;
                detail.Description = data.QuestionBankDescription;
                detail.Duration = data.Duration;
                detail.QuestionBankName = data.QuestionBankName;
            }
            catch (Exception)
            {
                throw;
            }
            return detail;
        }

        public APIResponse UpdateQuestionBank(QuestionBankDetail questionsDetails)
        {
            APIResponse aPIResponse = new APIResponse();
            QuestionBankDAL questionBankDAL = new QuestionBankDAL();
            aPIResponse = questionBankDAL.UpdateQuestionBank(questionsDetails);

            return aPIResponse;
        }

        public APIResponse QuestionBankUpload(QuestionBankDetail questionsDetailsData)
        {
            APIResponse aPIResponse = new APIResponse();
            QuestionBankDAL questionBankDAL = new QuestionBankDAL();
            aPIResponse = questionBankDAL.QuestionBankUpload(questionsDetailsData);

            
            return aPIResponse;
        }
    }
}
