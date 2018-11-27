using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Web.DTO.Models
{
   public class QuestionBankView
    {
        public System.Guid ID { get; set; }
        public string QuestionBankName { get; set; }
        public Nullable<System.TimeSpan> Duration { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string QuestionBankDescription { get; set; }
        public bool IsActive { get; set; }
    }
}
