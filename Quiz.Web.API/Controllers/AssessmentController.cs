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
    }
}
