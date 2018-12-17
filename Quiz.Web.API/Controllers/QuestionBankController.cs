using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Quiz.Web.BLL.Home;
using Quiz.Web.DTO.Models;

namespace Quiz.Web.API.Controllers
{
    public class QuestionBankController : ApiController
    {
        private QuestionBankBLL objQuestionBank = new QuestionBankBLL();
        private APIResponse APIResponse = new APIResponse();
         
        [System.Web.Http.HttpGet]
        public List<QuestionBankDetail> GetQuestionBank()
        {
            List<QuestionBankDetail> QuestionBankDetail  = new List<QuestionBankDetail>();
            QuestionBankDetail = objQuestionBank.GetExistingQuestionBank();
            return QuestionBankDetail;
        }

        [System.Web.Http.HttpGet]
        public List<QuestionsDetailsView> GetQuestionsList(Guid? QuestionBankId)
        {
            List<QuestionsDetailsView> usersDetails = new List<QuestionsDetailsView>();
            try
            {
                usersDetails = objQuestionBank.GetQuestionsList(QuestionBankId);
            }
            catch (Exception ex)
            {

                throw;
            }
            return usersDetails;
        }

        [System.Web.Http.HttpGet]
        public QuestionsDetailsView QuestionsEdit(Guid? QuestionId)
        {
            QuestionsDetailsView detailsView = new QuestionsDetailsView();
            try
            {
                detailsView = objQuestionBank.QuestionsEdit(QuestionId);
            }
            catch (Exception ex)
            {

                throw;
            }
            return detailsView;
        }

        [System.Web.Http.HttpGet]
        public QuestionBankDetail QuestionsBankEdit(Guid? QuestionBankId)
        {
            QuestionBankDetail questionBankDetail  = new QuestionBankDetail();
            try
            {
                questionBankDetail = objQuestionBank.QuestionsBankEdit(QuestionBankId);
            }
            catch (Exception ex)
            {

                throw;
            }
            return questionBankDetail;
        }

        [System.Web.Http.HttpPost]
        public APIResponse UpdateQuestion(QuestionsDetailsView questionsDetailsView)
        {
            try
            {
                APIResponse = objQuestionBank.UpdateQuestion(questionsDetailsView);
            }
            catch (Exception ex)
            {

                throw;
            }
            return APIResponse;
        }

        [System.Web.Http.HttpPost]
        public APIResponse UpdateQuestionBank(QuestionBankDetail questionsDetailsView)
        {
            try
            {
                APIResponse = objQuestionBank.UpdateQuestionBank(questionsDetailsView);
            }
            catch (Exception ex)
            {

                throw;
            }
            return APIResponse;
        }

        [System.Web.Http.HttpGet]
        public APIResponse QuestionsDelete(Guid? QuestionId)
        {
            APIResponse.Result = false;
            try
            {
                APIResponse = objQuestionBank.QuestionsDelete(QuestionId);
            }
            catch (Exception ex)
            {

                throw;
            }
            return APIResponse;
        }

        [System.Web.Http.HttpGet]
        public APIResponse QuestionsBankDelete(Guid? QuestionBankId)
        {
            APIResponse.Result = false;
            try
            {
                APIResponse = objQuestionBank.QuestionsBankDelete(QuestionBankId);
            }
            catch (Exception ex)
            {

                throw;
            }
            return APIResponse;
        }

        [System.Web.Http.HttpPost]
        public APIResponse UploadQuestionBank(QuestionBankDetail questionsDetailsData)
        {
            APIResponse aPIResponse = new APIResponse();
            QuestionBankBLL questionBankBLL = new QuestionBankBLL();
            aPIResponse = questionBankBLL.QuestionBankUpload(questionsDetailsData);
            return aPIResponse;
        }


    }
}
