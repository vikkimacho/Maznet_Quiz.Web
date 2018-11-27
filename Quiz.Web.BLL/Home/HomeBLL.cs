using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Web.DAL.Home;
using Quiz.Web.DTO.Models;

namespace Quiz.Web.BLL.Home
{
    public class HomeBLL
    {
       readonly HomeDAL homeDAL = new HomeDAL();
        public string GetDashBoard()
        {
           var dashboard =  homeDAL.GetDashBoard();
            return dashboard;
        }
    }
}
