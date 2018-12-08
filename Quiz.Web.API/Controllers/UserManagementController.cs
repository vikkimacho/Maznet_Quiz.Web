using Quiz.Web.BLL.UserManagement;
using Quiz.Web.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Quiz.Web.API.Controllers
{
    public class UserManagementController : ApiController
    {
        [System.Web.Http.HttpGet]
        public List<UsersDetails> GetUsersList()
        {
            List<UsersDetails> usersDetails = new List<UsersDetails>();
            UserManagementBLL userManagementBLL = new UserManagementBLL();
            try
            {
                usersDetails = userManagementBLL.GetUsersList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return usersDetails;
        }

        [System.Web.Http.HttpGet]
        public List<UsersDetailsModel> GetUsersDetailList(Guid? UserDetailId)
        {
            List<UsersDetailsModel> usersDetails = new List<UsersDetailsModel>();
            UserManagementBLL userManagementBLL = new UserManagementBLL();
            try
            {
                usersDetails = userManagementBLL.GetUsersDetailList(UserDetailId);
            }
            catch (Exception ex)
            {

                throw;
            }
            return usersDetails;
        }


        // Post: UserManagement
        [System.Web.Http.HttpPost]
        public APIResponse UploadUserDetail(UsersDetails usersDetails)
        {
            UsersDetailsModel details = new UsersDetailsModel();
            APIResponse response = new APIResponse();
            UserManagementBLL userManagementBLL = new UserManagementBLL();
            try
            {
                response = userManagementBLL.UploadUserDetail(usersDetails);
            }
            catch (Exception ex)
            {

                throw;
            }            
            return response;
        }
    }
}