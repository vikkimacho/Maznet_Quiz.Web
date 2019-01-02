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
        private APIResponse response = new APIResponse();
        private UserManagementDAL userManagementDAL = new UserManagementDAL();
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
                        detail.SSLCPassedOutYear = x.SSLCPassedOutYear;
                        detail.SSLCPercentage = x.SSLCPercentage;
                        detail.SSLCBoardName = x.SSLCBoardName;
                        detail.TechnicalSkills = x.TechnicalSkills;
                        detail.HSCPercentage = x.HSCPercentage;
                        detail.LastName = x.LastName;
                        detail.DOB = x.DOB;
                        detail.State = x.State;
                        detail.DegreePassedOutYear = x.DegreePassedOutYear;
                        detail.HSCBoardName = x.HSCBoardName;
                        detail.HSCPassedOutYear = x.HSCPassedOutYear;
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

        public APIResponse UserDelete(Guid? UserId)
        {
            try
            {
                response = userManagementDAL.UserDelete(UserId);

            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public UsersDetails UserDetailEdit(Guid? UserDetailId)
        {
            UsersDetails detail = new UsersDetails();
            try
            {
                var data = userManagementDAL.UserDetailEdit(UserDetailId);
                detail.Id = data.Id;
                detail.UserTitleName = data.UserTitle;
            }
            catch (Exception)
            {
                throw;
            }
            return detail;
        }

        public APIResponse UpdateUser(UsersDetailsModel usersDetailsModel)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse = userManagementDAL.UpdateUser(usersDetailsModel);
            return aPIResponse;
        }


        public APIResponse UserDetailDelete(Guid? UserDetailId)
        {
            try
            {
                response = userManagementDAL.UserDetailDelete(UserDetailId);

            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public UsersDetailsModel UserEdit(Guid? UserId)
        {

            UsersDetailsModel detail = new UsersDetailsModel();
            try
            {
                var data = userManagementDAL.UserEdit(UserId);
                detail.Id = data.ID;
                detail.UserDetailId = data.UserDetailId;
                detail.Name = data.Name;
                detail.Email = data.Email;
                detail.MobileNumber = data.MobileNumber;
                detail.Degree = data.Degree;
                detail.Institution = data.Institution;
                detail.Major = data.Major;
                detail.Percentage = data.Percentage;
                detail.Gender = data.Gender;
                detail.Address = data.Address;
                detail.SSLCPassedOutYear = data.SSLCPassedOutYear;
                detail.SSLCPercentage = data.SSLCPercentage;
                detail.SSLCBoardName = data.SSLCBoardName;
                detail.TechnicalSkills = data.TechnicalSkills;
                detail.HSCPercentage = data.HSCPercentage;
                detail.LastName = data.LastName;
                detail.DOB = data.DOB;
                detail.State = data.State;
                detail.DegreePassedOutYear = data.DegreePassedOutYear;
                detail.HSCBoardName = data.HSCBoardName;
                detail.HSCPassedOutYear = data.HSCPassedOutYear;
            }
            catch (Exception)
            {
                throw;
            }
            return detail;
        }

        public APIResponse UpdateUserDetail(UsersDetails usersDetails)
        {
            response = userManagementDAL.UpdateUserDetail(usersDetails);
            return response;
        }
    }
}
