using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Quiz.Web.BLL.Home;
using Quiz.Web.DTO.Models;

namespace Quiz.Web.API.Controllers
{
    public class DashBoardController : ApiController
    {
        [HttpPost]
        public DashBoardDetailsView GetDashBoard(DashBoardRange range)
        {
            DashBoardDetailsView dashBoardDetailsView = new DashBoardDetailsView();
            HomeBLL homeBLL = new HomeBLL();
            dashBoardDetailsView = homeBLL.GetDashBoard(range);
            return dashBoardDetailsView;
        }
    }
}
