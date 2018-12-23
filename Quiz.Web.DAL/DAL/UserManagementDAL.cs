using Quiz.Web.DAL.DataAccess;
using Quiz.Web.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Web.DAL.DAL
{
    public class UserManagementDAL
    {
        public List<UserDetailMaster> GetUsersList()
        {
            List<UserDetailMaster> details = new List<UserDetailMaster>();            
            APIResponse response = new APIResponse();
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    details = testEngineEntities.UserDetailMasters.Where(x => x.IsDeleted == false).ToList();
                    
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return details;
        }

        public List<DefaultRegistation> GetUsersDetailList(Guid? UserDetailId)
        {
            List<DefaultRegistation> details = new List<DefaultRegistation>();
            APIResponse response = new APIResponse();
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    details = testEngineEntities.DefaultRegistations.Where(x => x.UserDetailId == UserDetailId && x.IsDeleted == false).ToList();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return details;
        }
        public APIResponse UploadUserDetail(UsersDetails usersDetails)
        {
            UsersDetailsModel details = new UsersDetailsModel();
            APIResponse response = new APIResponse();
            var resultUserDetailMasterGuid = Guid.NewGuid();
            response.ResultUserDetailMasterGuid = resultUserDetailMasterGuid;
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    UserDetailMaster users = new UserDetailMaster();
                    users.Id = resultUserDetailMasterGuid;
                    users.UserTitle = usersDetails.UserTitleName;
                    users.CreatedDate = DateTime.Now;
                    users.ModifiedDate = DateTime.Now;
                    users.IsDeleted = false;
                    testEngineEntities.UserDetailMasters.Add(users);

                    foreach(var item in usersDetails.UsersDetailsModel)
                    {
                        DefaultRegistation defaultRegistation = new DefaultRegistation();
                        defaultRegistation.ID = Guid.NewGuid();
                        defaultRegistation.UserDetailId = users.Id;
                        defaultRegistation.Name = item.Name;
                        defaultRegistation.Email = item.Email;
                        defaultRegistation.MobileNumber = item.MobileNumber;
                        defaultRegistation.Degree = item.Degree;
                        defaultRegistation.Institution = item.Institution;
                        defaultRegistation.Major = item.Major;
                        defaultRegistation.Percentage = item.Percentage;
                        defaultRegistation.Gender = item.Gender;
                        defaultRegistation.Address = item.Address;
                        defaultRegistation.CustomField1 = item.CustomField1;
                        defaultRegistation.CustomField2 = item.CustomField2;
                        defaultRegistation.CustomField3 = item.CustomField3;
                        defaultRegistation.CustomField4 = item.CustomField4;
                        defaultRegistation.CustomField5 = item.CustomField5;
                        defaultRegistation.CustomField6 = item.CustomField6;
                        defaultRegistation.CustomField7 = item.CustomField7;
                        defaultRegistation.CustomField8 = item.CustomField8;
                        defaultRegistation.CustomField9 = item.CustomField9;
                        defaultRegistation.CustomField10 = item.CustomField10;
                        defaultRegistation.ModifiedDate = DateTime.UtcNow;
                        defaultRegistation.CreatedDate = DateTime.UtcNow;
                        defaultRegistation.IsDeleted = true;
                        testEngineEntities.DefaultRegistations.Add(defaultRegistation);

                    }
                    testEngineEntities.SaveChanges();
                    response.Result = true;
                }
                


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return response;
        }

        public DefaultRegistation UserEdit(Guid? UserId)
        {
            DefaultRegistation details = new DefaultRegistation();
            APIResponse response = new APIResponse();
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    details = testEngineEntities.DefaultRegistations.Where(x => x.ID  == UserId && x.IsDeleted == false).FirstOrDefault();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return details;
        }

        public APIResponse UserDelete(Guid? UserId)
        {
            APIResponse response = new APIResponse();
            response.Result = false;
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    var data = testEngineEntities.DefaultRegistations.Where(x => x.ID == UserId && x.IsDeleted == false).FirstOrDefault();
                    if (data != null)
                    {
                        data.IsDeleted = true;
                        data.ModifiedDate = DateTime.UtcNow;
                        testEngineEntities.SaveChanges();
                        response.Result = true;
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return response;
        }

        public APIResponse UpdateUser(UsersDetailsModel usersDetailsModel)
        {
            APIResponse response = new APIResponse();
            response.Result = false;
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    var data = testEngineEntities.DefaultRegistations.Where(x => x.ID == usersDetailsModel.Id && x.IsDeleted == false).FirstOrDefault();
                    if (data != null)
                    {
                        data.Name = usersDetailsModel.Name;
                        data.Email = usersDetailsModel.Email;
                        data.MobileNumber = usersDetailsModel.MobileNumber;
                        data.Degree = usersDetailsModel.Degree;
                        data.Institution = usersDetailsModel.Institution;
                        data.Major = usersDetailsModel.Major;
                        data.Percentage = usersDetailsModel.Percentage;
                        data.Gender = usersDetailsModel.Gender;
                        data.Address = usersDetailsModel.Address;
                        data.CustomField1 = usersDetailsModel.CustomField1;
                        data.CustomField2 = usersDetailsModel.CustomField2;
                        data.CustomField3 = usersDetailsModel.CustomField3;
                        data.CustomField4 = usersDetailsModel.CustomField4;
                        data.CustomField5 = usersDetailsModel.CustomField5;
                        data.CustomField6 = usersDetailsModel.CustomField6;
                        data.CustomField7 = usersDetailsModel.CustomField7;
                        data.CustomField8 = usersDetailsModel.CustomField8;
                        data.CustomField9 = usersDetailsModel.CustomField9;
                        data.CustomField10 = usersDetailsModel.CustomField10;
                        data.ModifiedDate = DateTime.UtcNow;
                        testEngineEntities.SaveChanges();
                        response.Result = true;
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return response;
        }

        public UserDetailMaster UserDetailEdit(Guid? UserDetailId)
        {
            UserDetailMaster details = new UserDetailMaster();
            APIResponse response = new APIResponse();
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    details = testEngineEntities.UserDetailMasters.Where(x => x.Id == UserDetailId && x.IsDeleted == false).FirstOrDefault();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return details;
        }

        public APIResponse UserDetailDelete(Guid? UserDetailId)
        {
            APIResponse response = new APIResponse();
            response.Result = false;
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    var data = testEngineEntities.UserDetailMasters.Where(x => x.Id == UserDetailId && x.IsDeleted == false).FirstOrDefault();
                    var Users = testEngineEntities.DefaultRegistations.Where(x => x.UserDetailId == UserDetailId && x.IsDeleted == false).FirstOrDefault();
                    if (data != null)
                    {
                        data.IsDeleted = true;
                        data.ModifiedDate = DateTime.UtcNow;
                        testEngineEntities.SaveChanges();
                        response.Result = true;

                        if (Users != null)
                        {
                            Users.IsDeleted = true;
                            Users.ModifiedDate = DateTime.UtcNow;
                            testEngineEntities.SaveChanges();
                            response.Result = true;

                        }
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return response;
        }

        public APIResponse UpdateUserDetail(UsersDetails usersDetails)
        {
            APIResponse response = new APIResponse();
            response.Result = false;
            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    var data = testEngineEntities.UserDetailMasters.Where(x => x.Id == usersDetails.Id && x.IsDeleted == false).FirstOrDefault();
                    if (data != null)
                    {
                        data.UserTitle = usersDetails.UserTitleName;
                        data.ModifiedDate = System.DateTime.UtcNow;
                        testEngineEntities.SaveChanges();
                        response.Result = true;
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return response;
        }

    }
}
