﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DBEntities : DbContext
    {
        public DBEntities()
            : base("name=DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdminDetail> AdminDetails { get; set; }
        public virtual DbSet<AssessmentAdminEmailNotification> AssessmentAdminEmailNotifications { get; set; }
        public virtual DbSet<AssessmentDetailMaster> AssessmentDetailMasters { get; set; }
        public virtual DbSet<AssessmentQuestionBankDetail> AssessmentQuestionBankDetails { get; set; }
        public virtual DbSet<AssessmentStudentNotification> AssessmentStudentNotifications { get; set; }
        public virtual DbSet<AssessmentUserDetail> AssessmentUserDetails { get; set; }
        public virtual DbSet<CandidateAssesmentDetailsForm> CandidateAssesmentDetailsForms { get; set; }
        public virtual DbSet<CustomRegistrationForm> CustomRegistrationForms { get; set; }
        public virtual DbSet<EligibilityCriteriaDetail> EligibilityCriteriaDetails { get; set; }
        public virtual DbSet<ExamFinalReport> ExamFinalReports { get; set; }
        public virtual DbSet<InputControlMaster> InputControlMasters { get; set; }
        public virtual DbSet<QuestionBankMaster> QuestionBankMasters { get; set; }
        public virtual DbSet<QuestionsDetail> QuestionsDetails { get; set; }
        public virtual DbSet<RegistrationFormControl> RegistrationFormControls { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UserAssessmentAnswerdetail> UserAssessmentAnswerdetails { get; set; }
        public virtual DbSet<AssesmentMasterDetailsForm> AssesmentMasterDetailsForms { get; set; }
        public virtual DbSet<ExaminerAssessmentDetail> ExaminerAssessmentDetails { get; set; }
        public virtual DbSet<ExaminerMaster> ExaminerMasters { get; set; }
        public virtual DbSet<ExaminerMasterDetail> ExaminerMasterDetails { get; set; }
        public virtual DbSet<ExaminerQuestionDetail> ExaminerQuestionDetails { get; set; }
        public virtual DbSet<UserDetailMaster> UserDetailMasters { get; set; }
        public virtual DbSet<DefaultRegistation> DefaultRegistations { get; set; }
    
        public virtual ObjectResult<Assesmentpagemodal_Result> Assesmentpagemodal()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Assesmentpagemodal_Result>("Assesmentpagemodal");
        }
    
        public virtual ObjectResult<string> DeletionofAssesmentId(Nullable<System.Guid> assesmentId)
        {
            var assesmentIdParameter = assesmentId.HasValue ?
                new ObjectParameter("AssesmentId", assesmentId) :
                new ObjectParameter("AssesmentId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("DeletionofAssesmentId", assesmentIdParameter);
        }
    
        public virtual ObjectResult<GetAssessmentQuestions_Result> GetAssessmentQuestions(Nullable<System.Guid> assessmentID)
        {
            var assessmentIDParameter = assessmentID.HasValue ?
                new ObjectParameter("AssessmentID", assessmentID) :
                new ObjectParameter("AssessmentID", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAssessmentQuestions_Result>("GetAssessmentQuestions", assessmentIDParameter);
        }
    
        public virtual ObjectResult<GetCandidateAssesmentDetailsForm_Result> GetCandidateAssesmentDetailsForm()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCandidateAssesmentDetailsForm_Result>("GetCandidateAssesmentDetailsForm");
        }
    
        public virtual ObjectResult<GetDashBoardDetails_Result> GetDashBoardDetails(string starDatetime, string endDatetime)
        {
            var starDatetimeParameter = starDatetime != null ?
                new ObjectParameter("StarDatetime", starDatetime) :
                new ObjectParameter("StarDatetime", typeof(string));
    
            var endDatetimeParameter = endDatetime != null ?
                new ObjectParameter("EndDatetime", endDatetime) :
                new ObjectParameter("EndDatetime", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDashBoardDetails_Result>("GetDashBoardDetails", starDatetimeParameter, endDatetimeParameter);
        }
    
        public virtual ObjectResult<GetEligibilityCriteriaList_Result> GetEligibilityCriteriaList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEligibilityCriteriaList_Result>("GetEligibilityCriteriaList");
        }
    
        public virtual ObjectResult<GetExistingAssessmentDetails_Result> GetExistingAssessmentDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetExistingAssessmentDetails_Result>("GetExistingAssessmentDetails");
        }
    
        public virtual ObjectResult<GetExistingQuestionDetails_Result> GetExistingQuestionDetails(Nullable<System.Guid> assessmentId)
        {
            var assessmentIdParameter = assessmentId.HasValue ?
                new ObjectParameter("assessmentId", assessmentId) :
                new ObjectParameter("assessmentId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetExistingQuestionDetails_Result>("GetExistingQuestionDetails", assessmentIdParameter);
        }
    
        public virtual ObjectResult<GetLstUserDetailMaster_Result> GetLstUserDetailMaster()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetLstUserDetailMaster_Result>("GetLstUserDetailMaster");
        }
    
        public virtual ObjectResult<GetMyAssesments_Result> GetMyAssesments()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetMyAssesments_Result>("GetMyAssesments");
        }
    
        public virtual ObjectResult<GetUploadedUserDetailsOnUserDetailId_Result> GetUploadedUserDetailsOnUserDetailId(Nullable<System.Guid> userDetailId)
        {
            var userDetailIdParameter = userDetailId.HasValue ?
                new ObjectParameter("userDetailId", userDetailId) :
                new ObjectParameter("userDetailId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUploadedUserDetailsOnUserDetailId_Result>("GetUploadedUserDetailsOnUserDetailId", userDetailIdParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual ObjectResult<ValidateAssessment_Result> ValidateAssessment(Nullable<System.Guid> assessmentID)
        {
            var assessmentIDParameter = assessmentID.HasValue ?
                new ObjectParameter("AssessmentID", assessmentID) :
                new ObjectParameter("AssessmentID", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ValidateAssessment_Result>("ValidateAssessment", assessmentIDParameter);
        }
    
        public virtual ObjectResult<string> ValidateDeletionofAssesmentId(Nullable<System.Guid> assesmentId)
        {
            var assesmentIdParameter = assesmentId.HasValue ?
                new ObjectParameter("AssesmentId", assesmentId) :
                new ObjectParameter("AssesmentId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("ValidateDeletionofAssesmentId", assesmentIdParameter);
        }
    
        public virtual ObjectResult<string> ValidateExaminer(Nullable<System.Guid> assessmentID, string username, string password)
        {
            var assessmentIDParameter = assessmentID.HasValue ?
                new ObjectParameter("AssessmentID", assessmentID) :
                new ObjectParameter("AssessmentID", typeof(System.Guid));
    
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("ValidateExaminer", assessmentIDParameter, usernameParameter, passwordParameter);
        }
    }
}
