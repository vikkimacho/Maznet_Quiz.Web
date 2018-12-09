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
    public class QuestionBankController : ApiController
    {
        private QuestionBankBLL objQuestionBank = new QuestionBankBLL();
         
        [HttpPost]
        public IHttpActionResult GetQuestionBank()
        {
            IList<QuestionBankView> questionBankMasters = null;
            questionBankMasters = objQuestionBank.GetExistingQuestionBank();
            return Ok(questionBankMasters);
        }
    }
}
