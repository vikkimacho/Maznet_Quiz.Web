﻿using Quiz.Web.DAL.DataAccess;
using Quiz.Web.DTO.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Web.DAL.Home
{
    public class HomeDAL
    {
        private DateTime dateTime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
        #region Declaration
        private readonly string Success = "SUCCESS";
        private readonly string Failed = "FAILED";
        #endregion
        public DashBoardDetailsView GetDashBoard(DashBoardRange range)
        {
            DashBoardDetailsView dashBoardDetailsView = new DashBoardDetailsView();
            try
            {
                using (DBEntities TestEngineDBContext = new DBEntities())
                {
                    var x = TestEngineDBContext.QuestionBankMasters.ToList();

                    var dashBoardDetails = TestEngineDBContext.Database.SqlQuery<DashBoardDetailsView>("Exec GetDashBoardDetails @StarDatetime,@EndDatetime"
                        , new SqlParameter("@StarDatetime", range.StartDatetime)
                        , new SqlParameter("@EndDatetime", range.EndDatetime)
                        ).ToList();

                    dashBoardDetailsView = dashBoardDetails.FirstOrDefault();
                }

            }
            catch (Exception)
            {


            }
            return dashBoardDetailsView;
        }

        public string ValidateUser(string username, string password)
        {
            string result = Failed;
            try
            {
                using (DBEntities TestEngineDBContext = new DBEntities())
                {
                    var adminUserDetails = TestEngineDBContext.AdminDetails.FirstOrDefault(x => x.UserName == username && x.Password == password && x.Isdeleted == false);
                    if (adminUserDetails != null)
                    {
                        result = Success;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public List<AdminDetail> GetAdminDetail()
        {
            List<AdminDetail> adminDetails = new List<AdminDetail>();
            try
            {
                using (DBEntities TestEngineDBContext = new DBEntities())
                {
                    adminDetails = TestEngineDBContext.AdminDetails.Where(x => x.IsSuperAdmin == false).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return adminDetails;
        }

        public string SaveAdminDetails(AdminDetail adminDetail)
        {
            dateTime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string result = Failed;
            try
            {
                using (DBEntities TestEngineDBContext = new DBEntities())
                {
                    var admin = TestEngineDBContext.AdminDetails.FirstOrDefault(x => x.ID == adminDetail.ID);
                    if (admin == null)
                    {
                        admin = new AdminDetail();
                        admin.CreatedDate = dateTime;
                        admin.Email = adminDetail.Email;
                        admin.ID = Guid.NewGuid();
                        admin.Isdeleted = false;
                        admin.ModifiedDate = dateTime;
                        admin.Password = adminDetail.Password;
                        admin.PhoneNumber = adminDetail.PhoneNumber;
                        admin.Role = adminDetail.Role;
                        admin.Name = adminDetail.Name;
                        admin.UserName = adminDetail.UserName;
                        admin.IsSuperAdmin = false;
                        TestEngineDBContext.AdminDetails.Add(admin);
                    }
                    else
                    {
                        admin.Email = adminDetail.Email;
                        admin.ModifiedDate = dateTime;
                        admin.Password = adminDetail.Password;
                        admin.PhoneNumber = adminDetail.PhoneNumber;
                        admin.Role = adminDetail.Role;
                        admin.UserName = adminDetail.UserName;
                        admin.Name = adminDetail.Name;
                        admin.Isdeleted = false;
                    }
                    TestEngineDBContext.SaveChanges();
                    result = Success;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public string DeleteAdmin(string id)
        {
            string result = Failed;
            try
            {
                Guid adminID = new Guid(id);
                using (DBEntities TestEngineDBContext = new DBEntities())
                {
                    var admin = TestEngineDBContext.AdminDetails.FirstOrDefault(x => x.ID == adminID);
                    if (admin != null)
                    {
                        admin.ModifiedDate = dateTime;
                        admin.Isdeleted = true;
                        TestEngineDBContext.SaveChanges();
                        result = Success;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

    }
}
