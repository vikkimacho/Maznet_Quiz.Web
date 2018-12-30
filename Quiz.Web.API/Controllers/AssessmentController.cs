using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Quiz.Web.BLL.Home;
using Quiz.Web.DTO.Models;

namespace Quiz.Web.API.Controllers
{
    public class AssessmentController : ApiController
    {

        AssessMentBLL ObjAssessmentBll = new AssessMentBLL();
        public string CreateAssessment()
        {
            var result= ObjAssessmentBll.CreateAssessMent();
            return result;
        }

        [HttpGet]
        public AssesmentPageModal GetAssessmentPageModal()
        {
            try
            {
                return ObjAssessmentBll.GetAssessmentPageModal();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [AcceptVerbs("Get", "Post")]
        public string PostCreateAssessment(PostAssessmentModal postAssessmentModal)
        {

            try
            {
                var newAssessemntGuid = Guid.NewGuid();
                //Need to send notification emails based on the selection.

                return ObjAssessmentBll.PostCreateAssessment(postAssessmentModal, newAssessemntGuid);
            }
            catch (Exception ex)
            {
                return "Failed";
            }

        }
        [AcceptVerbs("Get", "Post")]

        public string PostUpdateEligibleCriteria(List<PostEligibilityCriteria> lstpostAssessmentModal)
        {

            try
            {                
                return ObjAssessmentBll.PostUpdateEligibleCriteria(lstpostAssessmentModal);
            }
            catch (Exception ex)
            {
                return "Failed";
            }

        }

        [AcceptVerbs("Get", "Post")]

        public string ValidateAssesmentName(AssesmentName assesmentName)
        {
            try
            {
                return ObjAssessmentBll.ValidateAssesmentName(assesmentName.ValidateAssesmentName);
            }
            catch (Exception ex)
            {
                return "Failed";
            }

        }
        [AcceptVerbs("Get", "Post")]

        public string DeleteAssesment(Guid AssesmentId)
        {
            try
            {
                return ObjAssessmentBll.DeleteAssesment(AssesmentId);
            }
            catch (Exception ex)
            {
                return "Failed";
            }

        }


        

        [AcceptVerbs("Get", "Post")]

        public List<MyAssesmentModal> ListMyAssesments()
        {
            try
            {
                return ObjAssessmentBll.GetListMyAssesment();
            }
            catch (Exception ex)
            {
                return new List<MyAssesmentModal>();
            }
        }

        [HttpGet]
        public List<QuestionBankModal> LQuestionBankModal()
        {
            try
            {
                return ObjAssessmentBll.LQuestionBankModal();
            }
            catch (Exception ex)
            {
                return new List<QuestionBankModal>();
            }

        }
        [HttpGet]
        public List<ExistingQuestionBankDetails> GetExistingQuestionBankDetails(Guid assessmentId)
        {
            try
            {
                return ObjAssessmentBll.GetExistingQuestionBankDetails(assessmentId);
            }
            catch(Exception ex)
            {
                return new List<ExistingQuestionBankDetails>();
            }

        }


    }
}
