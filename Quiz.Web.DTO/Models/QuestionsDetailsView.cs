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
        public string Answer { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        
    }

    public class QuestionBankDetail
    {
        public string QuestionBankName { get; set; }
        public Nullable<System.TimeSpan> Duration { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
