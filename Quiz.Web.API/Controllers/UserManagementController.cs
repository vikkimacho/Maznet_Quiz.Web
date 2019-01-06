using Quiz.Web.BLL.Home;
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
        private UserManagementBLL userManagementBLL = new UserManagementBLL();
        APIResponse response = new APIResponse();
        [System.Web.Http.HttpGet]
        public List<UsersDetails> GetUsersList()
        {
            List<UsersDetails> usersDetails = new List<UsersDetails>();
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

        [System.Web.Http.HttpGet]
        public UsersDetails UserDetailEdit(Guid? UserDetailId)
        {
            UsersDetails detailsView = new UsersDetails();
            try
            {
                detailsView = userManagementBLL.UserDetailEdit(UserDetailId);
            }
            catch (Exception ex)
            {

                throw;
            }
            return detailsView;
        }

        [System.Web.Http.HttpGet]
        public UsersDetailsModel UserEdit(Guid? UserId)
        {
            UsersDetailsModel usersDetailsModel = new UsersDetailsModel();
            try
            {
                usersDetailsModel = userManagementBLL.UserEdit(UserId);
            }
            catch (Exception ex)
            {

                throw;
            }
            return usersDetailsModel;
        }

        [System.Web.Http.HttpPost]
        public APIResponse UpdateUserDetail(UsersDetails usersDetails)
        {
            try
            {
                response = userManagementBLL.UpdateUserDetail(usersDetails);
            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }

        [System.Web.Http.HttpPost]
        public APIResponse UpdateUser(UsersDetailsModel usersDetailsModel)
        {
            try
            {
                response = userManagementBLL.UpdateUser(usersDetailsModel);
            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }

        [System.Web.Http.HttpGet]
        public APIResponse UserDetailDelete(Guid? UserDetailId)
        {
            try
            {
                response = userManagementBLL.UserDetailDelete(UserDetailId);
            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }

        [System.Web.Http.HttpGet]
        public APIResponse UserDelete(Guid? UserId)
        {
            try
            {
                response = userManagementBLL.UserDelete(UserId);
            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }



    }
}