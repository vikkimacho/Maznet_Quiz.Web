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
}
