using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Web.DTO.Models
{
    public class AdminDetails
    {
        public System.Guid ID { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Isdeleted { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string Name { get; set; }

    }
}
