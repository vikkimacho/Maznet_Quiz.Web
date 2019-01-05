using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz.Web.ExamPortal.Helper
{
    public class SessionHelper
    {
        public static SessionStoreObjects sessionObjects
        {
            get
            {
                if ((HttpContext.Current.Session["LOGINID"] == null))
                    HttpContext.Current.Session.Add("LOGINID", new SessionStoreObjects());
                return HttpContext.Current.Session["LOGINID"] as SessionStoreObjects;
            }
            set { HttpContext.Current.Session["LOGINID"] = value; }
        }
    }

    public class SessionStoreObjects
    {
        public Guid AssessmentID { get; set; }
        public Guid UserID { get; set; }
       
    }
}