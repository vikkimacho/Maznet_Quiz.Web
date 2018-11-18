using Quiz.Web.DAL.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Web.BLL.Login
{
    public class LoginBLL
    {

        public string Login()
        {
            LoginDAL obj = new LoginDAL();
            var login = obj.Login();

            return login;
        }       
        
    }
}
