using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Web.DAL.Home;
using Quiz.Web.DTO.Models;
using Newtonsoft.Json;

namespace Quiz.Web.BLL.Home
{
    public class AssessMentBLL
    {

        private AssessMentDAL ObjAssessMentDAL = new AssessMentDAL();
        public string CreateAssessMent()
        {
            var result= ObjAssessMentDAL.CreateAssessMent();
            return result;
        }

        public AssesmentPageModal GetAssessmentPageModal()
        {
            var result = ObjAssessMentDAL.GetAssessmentPageModal();
            return result;
        }


        public string PostCreateAssessment(PostAssessmentModal postAssessmentModal,Guid AssessmentId)
        {
            var result = ObjAssessMentDAL.PostCreateAssessment(postAssessmentModal, AssessmentId);
            //Need to send notification emails based on the selection.

            return result;
        }



        public string PostUpdateEligibleCriteria(List<PostEligibilityCriteria> lstpostAssessmentModal)
        {
            var result = ObjAssessMentDAL.PostUpdateEligibleCriteria(lstpostAssessmentModal);
     

            return result;
        }
        public string ValidateAssesmentName(string assesmentName)
        {
            var result = ObjAssessMentDAL.ValidateAssesmentName(assesmentName);
            return result;
        }

        public string DeleteAssesment(Guid AssesmentId)
        {
            var result = ObjAssessMentDAL.ValidateAndDeleteAssesment(AssesmentId);
            return result;
        }

        public List<MyAssesmentModal> GetListMyAssesment()
        {
            var result = ObjAssessMentDAL.GetListMyAssesment();
            return result;

        }


        public List<QuestionBankModal> LQuestionBankModal()
        {
            var result = ObjAssessMentDAL.LQuestionBankModal();
            return result;

        }

        public List<ExistingQuestionBankDetails> GetExistingQuestionBankDetails(Guid assessmentId)
        {
            var questionBankDetails = ObjAssessMentDAL.GetExistingQuestionBankDetails(assessmentId);
            return questionBankDetails;
        }

        public List<UsersDetailsModel> GetUploadedUserDetail(Guid userDetailId)
        {
            var uploadDetail = ObjAssessMentDAL.GetUploadedUserDetails(userDetailId);
            return uploadDetail;
        }
    }

}
