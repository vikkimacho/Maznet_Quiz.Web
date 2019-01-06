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
    using System.Collections.Generic;
    
    public partial class AssessmentDetailMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AssessmentDetailMaster()
        {
            this.AssessmentAdminEmailNotifications = new HashSet<AssessmentAdminEmailNotification>();
            this.AssessmentQuestionBankDetails = new HashSet<AssessmentQuestionBankDetail>();
            this.AssessmentStudentNotifications = new HashSet<AssessmentStudentNotification>();
            this.AssessmentUserDetails = new HashSet<AssessmentUserDetail>();
            this.ExamFinalReports = new HashSet<ExamFinalReport>();
            this.UserAssessmentAnswerdetails = new HashSet<UserAssessmentAnswerdetail>();
        }
    
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssessmentAdminEmailNotification> AssessmentAdminEmailNotifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssessmentQuestionBankDetail> AssessmentQuestionBankDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssessmentStudentNotification> AssessmentStudentNotifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssessmentUserDetail> AssessmentUserDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamFinalReport> ExamFinalReports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAssessmentAnswerdetail> UserAssessmentAnswerdetails { get; set; }
    }
}
