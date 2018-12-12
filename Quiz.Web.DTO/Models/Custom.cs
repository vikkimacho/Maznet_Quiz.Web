using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Web.DTO.Models
{
    public class AdminLogin
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class APIResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }


    public class QuestionBankModal
    {
        public Guid Id { get; set; }
        public string QuestionBankName { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string QuestionBankDescription { get; set; }
        public int NoOfQuestions { get; set; }
    }


    public class AssesmentPageModal
    {
        public List<QuestionBankModal> LQuestionBankModal { get; set; }



    }
}
