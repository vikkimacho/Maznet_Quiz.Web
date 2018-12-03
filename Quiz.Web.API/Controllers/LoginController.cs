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
        #region Declaration
        private readonly string Success = "SUCCESS";
        private readonly string Failed = "FAILED";
        #endregion
        // GET api/<controller>
        public string Get()
        {
            LoginBLL obj = new LoginBLL();
            var login = obj.Login();
            return login;
        }
        [HttpGet]
        public string ValidateUser(string username, string password)
        {
            LoginBLL obj = new LoginBLL();
            string result = Failed;
            try
            {
                result = obj.ValidateUser(username, password);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

    }

}