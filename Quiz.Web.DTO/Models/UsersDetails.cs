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
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public string CustomField4 { get; set; }
        public string CustomField5 { get; set; }
        public string CustomField6 { get; set; }
        public string CustomField7 { get; set; }
        public string CustomField8 { get; set; }
        public string CustomField9 { get; set; }
        public string CustomField10 { get; set; }
       
    }

    public class UsersDetails
    {
        public Guid? Id { get; set; }   
        public string UserTitleName { get; set; }
        public string CreatedDateTime { get; set; }
        public string ModifiedDateTime { get; set; }
        public List<UsersDetailsModel> UsersDetailsModel { get; set; }

    }
}
