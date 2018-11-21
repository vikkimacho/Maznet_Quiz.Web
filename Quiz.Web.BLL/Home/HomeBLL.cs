using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Web.DAL.Home;

namespace Quiz.Web.BLL.Home
{
    public class HomeBLL
    {
        public string GetDashBoard()
        {
            HomeDAL homeDAL = new HomeDAL();
           var dashboard =  homeDAL.GetDashBoard();
            return dashboard;
        }
    }
}
