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
    public class AdminManagementController : ApiController
    {
        #region Declaration
        private readonly string Success = "SUCCESS";
        private readonly string Failed = "FAILED";
        HomeBLL homeBLL = new HomeBLL();
        #endregion

        [HttpGet]
        public List<AdminDetails> GetAdminList()
        {
            List<AdminDetails> adminDetails = new List<AdminDetails>();
            try
            {
                
                
                adminDetails = homeBLL.GetAdminDetails();
                
            }
            catch(Exception ex)
            {

            }
            return adminDetails;

        }

        [HttpPost]
        public string SaveAdminDetails(AdminDetails adminDetails)
        {
            string result = Failed;
            try
            {
                result = homeBLL.SaveAdminDtails(adminDetails);
            }
            catch(Exception ex)
            {

            }
            return result;
        }

        [HttpPost]
        public string DeleteAdmin(string id)
        {
            string result = Failed;
            try
            {
                result = homeBLL.DeleteAdmin(id);
            }
            catch(Exception ex)
            {

            }
            return result;
        }
    }
}