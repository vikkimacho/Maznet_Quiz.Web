using Quiz.Web.BLL.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Quiz.Web.API.Controllers
{
    public class LoginController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            LoginBLL obj = new LoginBLL();
            var login = obj.Login();
            return login;
        }
        
    }

}