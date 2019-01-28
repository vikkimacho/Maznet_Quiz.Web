using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Web.DTO.Models
{

    public class UsersDetailsModel
    {
        public System.Guid Id { get; set; }
        public System.Guid UserDetailId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Degree { get; set; }
        public string Institution { get; set; }
        public string Major { get; set; }
        public string Percentage { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string SSLCPassedOutYear { get; set; }
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
        public Guid assessmentID { get; set; }
    }

    public class UsersDetails
    {
        public Guid? Id { get; set; }   
        public string UserTitleName { get; set; }
        public string CreatedDateTime { get; set; }
        public string ModifiedDateTime { get; set; }
        public List<UsersDetailsModel> UsersDetailsModel { get; set; }

    }

    public class CandidateDetailsReport
    {
        public Guid AssessmentID { get; set; }
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string MobileNumber { get; set; }
        public List<TestDetails> TestDetails { get; set; }
        public bool IsExamCompleted { get; set; }      
    }
    public class TestDetails
    {
        public string QuestionBankName { get; set; }
        public string Score { get; set; }
    }

    public class CandidateFinalReport
    {

    }
}
