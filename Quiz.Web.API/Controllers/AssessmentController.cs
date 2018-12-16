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

        public string PostUpdateEligibleCriteria(List<PostEligibilityCriteria> lstpostAssessmentModal)
        {

            try
            {
                var newAssessemntGuid = Guid.NewGuid();
                //Need to send notification emails based on the selection.

                return ObjAssessmentBll.PostUpdateEligibleCriteria(lstpostAssessmentModal);
            }
            catch (Exception ex)
            {
                return "Failed";
            }

        }



        
    }
}
