using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Quiz.Web.BLL;
using Quiz.Web.DTO.Models;

namespace Quiz.Web.API.Controllers
{
    public class QuestionBankUploadController : ApiController
    {
        public IHttpActionResult GetQuestionBank([FromBody] List<QuestionsDetailsView> questionsDetailsView)
        {

            return Ok("");
        }
    }
}
