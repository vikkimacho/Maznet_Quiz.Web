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
    
    public partial class DefaultRegistation
    {
        public System.Guid ID { get; set; }
        public System.Guid UserDetailId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Degree { get; set; }
        public string Institution { get; set; }
        public string Major { get; set; }
        public string Percentage { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string SSLCPercentage { get; set; }
        public string SSLCBoardName { get; set; }
        public string TechnicalSkills { get; set; }
        public string HSCPercentage { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string State { get; set; }
        public string DegreePassedOutYear { get; set; }
        public string HSCBoardName { get; set; }
        public string HSCPassedOutYear { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string UserName { get; set; }
        public string SSLCPassedOutYear { get; set; }
        public Nullable<bool> IsExamCompleted { get; set; }
    
        public virtual UserDetailMaster UserDetailMaster { get; set; }
    }
}
