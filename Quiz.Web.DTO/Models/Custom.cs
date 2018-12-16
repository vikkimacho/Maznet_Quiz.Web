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

    public class ExamAssessmentDetails
    {
        public Guid AssessmentID { get; set; }
        public string QuestionBankName { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid QuestionBankID { get; set; }
        public bool IsPrintScreenLock { get; set; }
        public bool IsBrowserLock { get; set; }
    }


    public class AssesmentPageModal
    {
        public List<QuestionBankModal> LQuestionBankModal { get; set; }



    }

    public class ExamPortalLogin
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }

    public class AssessmentDetailMaster
    {
        public System.Guid ID { get; set; }
        public string AssessmentName { get; set; }
        public Nullable<bool> IsBrowserLock { get; set; }
        public Nullable<bool> IsPrintScreenLock { get; set; }
        public Nullable<System.Guid> EligibilityCriteriaId { get; set; }
        public Nullable<System.DateTime> ScheduledStartDatetime { get; set; }
        public Nullable<System.DateTime> ScheduledEndDatetime { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
