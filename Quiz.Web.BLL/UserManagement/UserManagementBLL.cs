using Quiz.Web.DAL.DAL;
using Quiz.Web.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Web.BLL.UserManagement
{
    public class UserManagementBLL
    {
        public List<UsersDetails> GetUsersList()
        {
            List<UsersDetails> detaillist = new List<UsersDetails>();            
            UserManagementDAL userManagementDAL = new UserManagementDAL();
            try
            {
                var data = userManagementDAL.GetUsersList();
                if(data.Count > 0)
                {
                    data.ForEach(x =>
                    {
                        UsersDetails detail = new UsersDetails();
                        detail.Id = x.Id;
                        detail.UserTitleName = x.UserTitle;
                        detail.CreatedDateTime = x.CreatedDate.ToString();
                        detail.ModifiedDateTime = x.ModifiedDate.ToString();
                        detaillist.Add(detail);
                    });

                }
            }
            catch (Exception)
            {
                throw;
            }
            return detaillist;
        }

        public List<UsersDetailsModel> GetUsersDetailList(Guid? UserDetailId)
        {
            List<UsersDetailsModel> detaillist = new List<UsersDetailsModel>();
            UserManagementDAL userManagementDAL = new UserManagementDAL();
            try
            {
                var data = userManagementDAL.GetUsersDetailList(UserDetailId);
                if (data.Count > 0)
                {
                    data.ForEach(x =>
                    {
                        UsersDetailsModel detail = new UsersDetailsModel();
                        detail.Id = x.ID;
                        detail.UserDetailId = x.UserDetailId;
                        detail.Name = x.Name;
                        detail.Email = x.Email;
                        detail.MobileNumber = x.MobileNumber;
                        detail.Degree = x.Degree;
                        detail.Institution = x.Institution;
                        detail.Major = x.Major;
                        detail.Percentage = x.Percentage;
                        detail.Gender = x.Gender;
                        detail.Address = x.Address;
                        detail.CustomField1 = x.CustomField1;
                        detail.CustomField2 = x.CustomField2;
                        detail.CustomField3 = x.CustomField3;
                        detail.CustomField4 = x.CustomField4;
                        detail.CustomField5 = x.CustomField5;
                        detail.CustomField6 = x.CustomField6;
                        detail.CustomField7 = x.CustomField7;
                        detail.CustomField8 = x.CustomField8;
                        detail.CustomField9 = x.CustomField9;
                        detail.CustomField10 = x.CustomField10;
                        detaillist.Add(detail);
                    });

                }
            }
            catch (Exception)
            {
                throw;
            }
            return detaillist;
        }
        public APIResponse UploadUserDetail(UsersDetails usersDetails)
        {
            UsersDetailsModel details = new UsersDetailsModel();
            APIResponse response = new APIResponse();
            UserManagementDAL userManagementDAL = new UserManagementDAL();
            try
            {
                response = userManagementDAL.UploadUserDetail(usersDetails);
            }
            catch (Exception)
            {
                throw;
            }            
            return response;
        }
    }
}
