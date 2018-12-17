using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz.Web.ExamPortal.Models
{
    public class ExamLogin
    {
        public string username { get; set; }
        public string password { get; set; }
        public string assessmentid { get; set; }
    }
}