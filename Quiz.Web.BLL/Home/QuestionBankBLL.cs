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
        public List<QuestionBankView> GetExistingQuestionBank()
        {
            List<QuestionBankView> questionBankViews = new List<QuestionBankView>();
            var QuestionBankData = objQuestion.GetQuestionBank();
            if(QuestionBankData.Any())
            {
                questionBankViews= JsonConvert.DeserializeObject<List<QuestionBankView>>(JsonConvert.SerializeObject(QuestionBankData));
            }
            return questionBankViews;
        }
    }
}
