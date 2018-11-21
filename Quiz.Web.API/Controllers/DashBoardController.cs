using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Quiz.Web.BLL.Home;

namespace Quiz.Web.API.Controllers
{
    public class DashBoardController : ApiController
    {
        
        public string GetDashBoard()
        {
            HomeBLL homeBLL = new HomeBLL();
            var Dashboard= homeBLL.GetDashBoard();
            return Dashboard;
        }
    }
}
