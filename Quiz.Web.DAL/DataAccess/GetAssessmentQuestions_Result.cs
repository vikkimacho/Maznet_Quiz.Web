//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quiz.Web.DAL.DataAccess
{
    using System;
    
    public partial class GetAssessmentQuestions_Result
    {
        public System.Guid ID { get; set; }
        public Nullable<System.Guid> QuestionBankID { get; set; }
        public string Question { get; set; }
        public string MasterQuestion { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string OptionE { get; set; }
        public string Answer { get; set; }
        public Nullable<bool> IsMaster { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> MasterQuestionId { get; set; }
        public System.Guid ID1 { get; set; }
        public Nullable<System.Guid> AssessmentID { get; set; }
        public Nullable<System.Guid> QuestionBankID1 { get; set; }
        public Nullable<System.DateTime> CreatedDate1 { get; set; }
        public Nullable<System.DateTime> ModifiedDate1 { get; set; }
        public Nullable<bool> IsDeleted1 { get; set; }
        public Nullable<System.TimeSpan> Duration { get; set; }
        public string QuestionBankName { get; set; }
    }
}
