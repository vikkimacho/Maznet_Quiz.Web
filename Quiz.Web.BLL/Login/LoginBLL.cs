using Quiz.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Web.DAL.Home;

namespace Quiz.Web.BLL.Login
{
    public class LoginBLL
    {
        #region Declaration
        private readonly string Success = "SUCCESS";
        private readonly string Failed = "FAILED";
        HomeDAL obj = new HomeDAL();
        #endregion
        public string Login()
        {
            //
            //var login = obj.Login();
            return "";
        }
        public string ValidateUser(string username, string password)
        {
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
