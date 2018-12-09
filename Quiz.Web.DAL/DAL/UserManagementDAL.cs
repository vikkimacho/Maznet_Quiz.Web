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
                    details = testEngineEntities.DefaultRegistations.Where(x => x.UserDetailId == UserDetailId).ToList();

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

            try
            {
                using (TestEngineEntities testEngineEntities = new TestEngineEntities())
                {
                    UserDetailMaster users = new UserDetailMaster();
                    users.Id = Guid.NewGuid();
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
    }
}
