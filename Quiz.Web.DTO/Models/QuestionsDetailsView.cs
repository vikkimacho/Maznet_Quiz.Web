using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Web.DTO.Models
{
    public class QuestionsDetailsView
    {
        public System.Guid ID { get; set; }
        public Nullable<System.Guid> QuestionBankID { get; set; }
        public string Question { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string OptionE { get; set; }
        public string Answer { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        
    }

    public class QuestionBankDetail
    {
        public Guid ID { get; set; }
        public string QuestionBankName { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Description { get; set; }
        public string ModifiedDate { get; set; }
        public bool Status { get; set; }

        public  List<QuestionsDetailsView> questionsDetailsViews { get; set; }
    }
}
