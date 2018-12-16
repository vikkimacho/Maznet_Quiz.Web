﻿using System;
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
        public List<CustomCandidateAssesmentDetailsForm> LstCandidateAssesmentDetailsForm { get; set; }
        public List<CustomUserDetailMaster> LstUserDetailMaster { get; set; }

    }

    public class CustomUserDetailMaster
    {
        public Guid Id { get; set; }
        public string UserTitle { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
         
    }


    public class CustomCandidateAssesmentDetailsForm
    {
        public System.Guid id { get; set; }
        public System.Guid FormId { get; set; }
        public string DisplayFieldName { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public bool IsMandatory { get; set; }
        public string Values { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string Remarks { get; set; }
        public string ModificationHistory { get; set; }
        public bool IsLocked { get; set; }
        public bool IsEnabled { get; set; }
        public System.DateTime Createddate { get; set; }
    }


    public class PostEligibilityCriteria
    {

        public Guid QuestionBankID { get; set; }
        public string Name { get; set; }
        public long NotConsider { get; set; }
        public long MayConsider { get; set; }
        public long StrongConsider { get; set; }

        //var postData = { QuestionBankID :  obj.id, Name : txtShortListCriteria, NotConsider :NotConsider, MayConsider : MayConsider, StrongConsider:StrongConsider };

    }


    public class PostAssessmentModal
    {
        public string AssessmentName { get; set; }
        public List<Guid> LstQuestionBankSelected { get; set; }
        public bool IsBrowserLockEnabled { get; set; }
        public bool IsPrintScreenLockEnabled { get; set; }
        public List<AssessmentCandidateForm> LstCandidateFormSelectedFields { get; set; }
        public Guid SelectedShortListCriteria { get; set; }
        public DateTime ScheduleFrom { get; set; }
        public DateTime ScheduleTo { get; set; }
        public string SchedulePlan { get; set; }
        public List<Guid> lstBulkScheduleIds { get; set; }
        public SingleScheduleModal SingleScheduleModal { get; set; }
        public bool IsAssessmentCompletionAlertEnabled { get; set; }
        public string AssesmentAlertEmail { get; set; }
        public AssessmentStudentAlertModal AssessmentStudentAlertModal { get; set; }
        public CommonLoginModal CommonLoginModal { get; set; }



    }


    public class CommonLoginModal
    {
        public string CommonLoginUserName { get; set; }
        public string CommonLoginPassword { get; set; }
        public string CLNoOfCandidates { get; set; }
        public string CLSendLoginDetailsto { get; set; }
        
    }


    public class AssessmentCandidateForm
    {
        public bool IsMandatory { get; set; }
        public Guid FormId { get; set; }          
    }

    public class AssessmentStudentAlertModal
    {
        public bool IsEnabled { get; set; }
        public string Type { get; set; } //Assessment
        public string CommunicationType { get; set; }//SMS / Email
        public string BodyofMessage { get; set; } 
        public string CC { get; set; }
        public string BCC { get; set; }
    }

    public class SingleScheduleModal
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public DateTime DOB { get; set; }

    }


}
