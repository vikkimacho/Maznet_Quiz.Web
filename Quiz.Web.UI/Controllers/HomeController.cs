using Newtonsoft.Json;
using Quiz.Web.DTO.Models;
using Quiz.Web.UI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Quiz.Web.UI.Controllers
{
    [AuthorizationFilter]
    public class HomeController : Controller
    {
        private static string apiUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"];
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult>  DashBoard()
        {
            var Result = "";
            HttpClient client = new HttpClient();
            DashBoardDetailsView dashBoardDetailsView = new DashBoardDetailsView();
            DashBoardRange dashBoardRange = new DashBoardRange();
            dashBoardRange.StartDatetime = "";
            dashBoardRange.EndDatetime = "";
            //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
            var response = client.PostAsJsonAsync(apiUrl + "/DashBoard/GetDashBoard", dashBoardRange).Result;

            if (response.IsSuccessStatusCode)
            {
                Result = response.Content.ReadAsStringAsync().Result;

                dashBoardDetailsView = JsonConvert.DeserializeObject<DashBoardDetailsView>(Result);
            }
            return View(dashBoardDetailsView);
        }

        [HttpPost]
        public ActionResult GetDashBoard(DashBoardRange dashBoardRange)
        {
            var Result = "";
            HttpClient client = new HttpClient();
            DashBoardDetailsView dashBoardDetailsView = new DashBoardDetailsView();
            //HttpContent inputContent = new StringContent(Encoding.UTF8, "application/json");
            var response = client.PostAsJsonAsync(apiUrl + "/DashBoard/GetDashBoard", dashBoardRange).Result;

            if (response.IsSuccessStatusCode)
            {
                Result = response.Content.ReadAsStringAsync().Result;

                dashBoardDetailsView = JsonConvert.DeserializeObject<DashBoardDetailsView>(Result);
            }
            return Json(new { Result = dashBoardDetailsView });
        }




    }
}