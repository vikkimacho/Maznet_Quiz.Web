﻿using Quiz.Web.DAL.DataAccess;
using Quiz.Web.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiz.Web.DAL.DAL
{
    public class UserManagementDAL
    {
        private DateTime dateTime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
        public List<UserDetailMaster> GetUsersList()
        {
            List<UserDetailMaster> details = new List<UserDetailMaster>();
            APIResponse response = new APIResponse();
            try
            {
                using (DBEntities testEngineEntities = new DBEntities())
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
                using (DBEntities testEngineEntities = new DBEntities())
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
            dateTime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            APIResponse response = new APIResponse();
            var resultUserDetailMasterGuid = Guid.NewGuid();
            var usertype = "upload";
            response.ResultUserDetailMasterGuid = resultUserDetailMasterGuid;
            try
            {
                using (DBEntities testEngineEntities = new DBEntities())
                {
                    UserDetailMaster users = new UserDetailMaster();
                    users.Id = resultUserDetailMasterGuid;
                    users.UserTitle = usersDetails.UserTitleName;
                    users.UserType = usertype;
                    users.CreatedDate = dateTime;
                    users.ModifiedDate = dateTime;
                    users.IsDeleted = false;
                    testEngineEntities.UserDetailMasters.Add(users);

                    foreach (var item in usersDetails.UsersDetailsModel)
                    {
                        DefaultRegistation defaultRegistation = new DefaultRegistation();
                        defaultRegistation.ID = Guid.NewGuid();
                        defaultRegistation.UserDetailId = users.Id;
                        defaultRegistation.Name = item.Name;
                        defaultRegistation.Email = item.Email;
                        defaultRegistation.Password = item.Password;
                        defaultRegistation.MobileNumber = item.MobileNumber;
                        defaultRegistation.Degree = item.Degree;
                        defaultRegistation.Institution = item.Institution;
                        defaultRegistation.Major = item.Major;
                        defaultRegistation.Percentage = item.Percentage;
                        defaultRegistation.Gender = item.Gender;
                        defaultRegistation.Address = item.Address;
                        defaultRegistation.SSLCPassedOutYear = item.SSLCPassedOutYear;
                        defaultRegistation.SSLCPercentage = item.SSLCPercentage;
                        defaultRegistation.SSLCBoardName = item.SSLCBoardName;
                        defaultRegistation.TechnicalSkills = item.TechnicalSkills;
                        defaultRegistation.HSCPercentage = item.HSCPercentage;
                        defaultRegistation.LastName = item.LastName;
                        defaultRegistation.DOB = item.DOB;
                        defaultRegistation.State = item.State;
                        defaultRegistation.DegreePassedOutYear = item.DegreePassedOutYear;
                        defaultRegistation.HSCBoardName = item.HSCBoardName;
                        defaultRegistation.HSCPassedOutYear = item.HSCPassedOutYear;
                        defaultRegistation.ModifiedDate = dateTime; ;
                        defaultRegistation.CreatedDate = dateTime; ;
                        defaultRegistation.IsDeleted = false;
                        defaultRegistation.IsExamCompleted = false;
                        defaultRegistation.UserName = item.Email;
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
                using (DBEntities testEngineEntities = new DBEntities())
                {
                    details = testEngineEntities.DefaultRegistations.Where(x => x.ID == UserId && x.IsDeleted == false).FirstOrDefault();

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
                using (DBEntities testEngineEntities = new DBEntities())
                {
                    var data = testEngineEntities.DefaultRegistations.Where(x => x.ID == UserId && x.IsDeleted == false).FirstOrDefault();
                    if (data != null)
                    {
                        data.IsDeleted = true;
                        data.ModifiedDate = dateTime; ;
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
                using (DBEntities testEngineEntities = new DBEntities())
                {
                    var data = testEngineEntities.DefaultRegistations.Where(x => x.ID == usersDetailsModel.Id && x.IsDeleted == false).FirstOrDefault();
                    if (data != null)
                    {
                        data.Name = string.IsNullOrEmpty(usersDetailsModel.Name) ? data.Name : usersDetailsModel.Name;
                        data.Email = string.IsNullOrEmpty(usersDetailsModel.Email) ? data.Email : usersDetailsModel.Email;
                        data.MobileNumber = string.IsNullOrEmpty(usersDetailsModel.MobileNumber) ? data.MobileNumber : usersDetailsModel.MobileNumber;
                        data.Degree = string.IsNullOrEmpty(usersDetailsModel.Degree) ? data.Name : usersDetailsModel.Degree;
                        data.Institution = string.IsNullOrEmpty(usersDetailsModel.Institution) ? data.Institution : usersDetailsModel.Institution;
                        data.Major = string.IsNullOrEmpty(usersDetailsModel.Major) ? data.Name : usersDetailsModel.Major;
                        data.Percentage = string.IsNullOrEmpty(usersDetailsModel.Percentage) ? data.Percentage : usersDetailsModel.Percentage;
                        data.Gender = string.IsNullOrEmpty(usersDetailsModel.Gender) ? data.Name : usersDetailsModel.Gender;
                        data.Address = string.IsNullOrEmpty(usersDetailsModel.Address) ? data.Address : usersDetailsModel.Address;
                        data.SSLCPassedOutYear = string.IsNullOrEmpty(usersDetailsModel.SSLCPassedOutYear) ? data.SSLCPassedOutYear : usersDetailsModel.SSLCPassedOutYear;
                        data.SSLCPercentage = string.IsNullOrEmpty(usersDetailsModel.SSLCPercentage) ? data.SSLCPercentage : usersDetailsModel.SSLCPercentage;
                        data.SSLCBoardName = string.IsNullOrEmpty(usersDetailsModel.SSLCBoardName) ? data.SSLCBoardName : usersDetailsModel.SSLCBoardName;
                        data.TechnicalSkills = string.IsNullOrEmpty(usersDetailsModel.TechnicalSkills) ? data.TechnicalSkills : usersDetailsModel.TechnicalSkills;
                        data.HSCPercentage = string.IsNullOrEmpty(usersDetailsModel.HSCPercentage) ? data.HSCPercentage : usersDetailsModel.HSCPercentage;
                        data.LastName = string.IsNullOrEmpty(usersDetailsModel.LastName) ? data.LastName : usersDetailsModel.LastName;
                        data.DOB = string.IsNullOrEmpty(usersDetailsModel.DOB) ? data.DOB : usersDetailsModel.DOB;
                        data.State = string.IsNullOrEmpty(usersDetailsModel.State) ? data.State : usersDetailsModel.State;
                        data.DegreePassedOutYear = string.IsNullOrEmpty(usersDetailsModel.DegreePassedOutYear) ? data.DegreePassedOutYear : usersDetailsModel.DegreePassedOutYear;
                        data.HSCBoardName = string.IsNullOrEmpty(usersDetailsModel.HSCBoardName) ? data.HSCBoardName : usersDetailsModel.HSCBoardName;
                        data.HSCPassedOutYear = string.IsNullOrEmpty(usersDetailsModel.HSCPassedOutYear) ? data.HSCPassedOutYear : usersDetailsModel.HSCPassedOutYear;
                        data.ModifiedDate = dateTime;

                        var userDetail = testEngineEntities.UserDetailMasters.FirstOrDefault(x => x.Id == data.UserDetailId);
                        if (userDetail != null)
                        {
                            var assessmentUserDetail = testEngineEntities.AssessmentUserDetails.FirstOrDefault(x => x.UserID == userDetail.Id);
                            if (assessmentUserDetail != null)
                            {
                                var examinarMaster = testEngineEntities.ExaminerMasters.FirstOrDefault(x => x.AssessmentId == assessmentUserDetail.AssessmentID);
                                if (examinarMaster != null)
                                {
                                    var examinerMasterDetails = testEngineEntities.ExaminerMasterDetails.FirstOrDefault(x => x.ExaminerMasterId == examinarMaster.ID && x.UserId == data.ID);
                                    if (examinerMasterDetails == null)
                                    {
                                        ExaminerMasterDetail examinerMasterDetail = new ExaminerMasterDetail();
                                        examinerMasterDetail.CreatedDate = dateTime;
                                        examinerMasterDetail.ExaminerMasterId = examinarMaster.ID;
                                        examinerMasterDetail.ID = Guid.NewGuid();
                                        examinerMasterDetail.ModifiedDate = dateTime;
                                        examinerMasterDetail.UserId = data.ID;
                                        testEngineEntities.ExaminerMasterDetails.Add(examinerMasterDetail);
                                        testEngineEntities.SaveChanges();
                                    }
                                }
                            }
                        }
                        testEngineEntities.SaveChanges();
                        response.Result = true;
                        response.Message = "SUCCESS";

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
                using (DBEntities testEngineEntities = new DBEntities())
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
                using (DBEntities testEngineEntities = new DBEntities())
                {
                    var data = testEngineEntities.UserDetailMasters.Where(x => x.Id == UserDetailId && x.IsDeleted == false).FirstOrDefault();
                    var Users = testEngineEntities.DefaultRegistations.Where(x => x.UserDetailId == UserDetailId && x.IsDeleted == false).FirstOrDefault();
                    if (data != null)
                    {
                        data.IsDeleted = true;
                        data.ModifiedDate = dateTime; ;
                        testEngineEntities.SaveChanges();
                        response.Result = true;

                        if (Users != null)
                        {
                            Users.IsDeleted = true;
                            Users.ModifiedDate = dateTime; ;
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
            dateTime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            response.Result = false;
            try
            {
                using (DBEntities testEngineEntities = new DBEntities())
                {
                    var data = testEngineEntities.UserDetailMasters.Where(x => x.Id == usersDetails.Id && x.IsDeleted == false).FirstOrDefault();
                    if (data != null)
                    {
                        data.UserTitle = usersDetails.UserTitleName;
                        data.ModifiedDate = dateTime;
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
