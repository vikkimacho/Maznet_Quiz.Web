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
    
    public partial class AssessmentStudentNotification
    {
        public System.Guid id { get; set; }
        public System.Guid AssessmentId { get; set; }
        public bool IsEnabled { get; set; }
        public string Type { get; set; }
        public string CommunicationType { get; set; }
        public string BodyofMessage { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Remarks { get; set; }
        public string ModHistory { get; set; }
    
        public virtual AssessmentDetailMaster AssessmentDetailMaster { get; set; }
    }
}
