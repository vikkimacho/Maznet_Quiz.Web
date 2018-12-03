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
            var questionBankData = new List<QuestionBankMaster>();
            using (var QuizContext = new TestEngineEntities())
            {
                QuizContext.Configuration.ProxyCreationEnabled = false;
               questionBankData = QuizContext.QuestionBankMasters.ToList();
            }
            return questionBankData;
        }
    }
}
