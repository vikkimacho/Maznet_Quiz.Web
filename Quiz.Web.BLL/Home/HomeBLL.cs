using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Web.DAL.Home;
using Quiz.Web.DTO.Models;

namespace Quiz.Web.BLL.Home
{
    public class HomeBLL
    {
        #region Declaration
        private readonly string Success = "SUCCESS";
        private readonly string Failed = "FAILED";
        HomeDAL homeDAL = new HomeDAL();
        #endregion
        public DashBoardDetailsView GetDashBoard(DashBoardRange range)
        {

            DashBoardDetailsView dashBoardDetailsView = new DashBoardDetailsView();
            dashBoardDetailsView = homeDAL.GetDashBoard(range);
            return dashBoardDetailsView;
            ;
        }

        public List<AdminDetails> GetAdminDetails()
        {
            List<AdminDetails> adminDetails = new List<AdminDetails>();
            try
            {
                var list = homeDAL.GetAdminDetail();
                if (list.Count() > 0)
                {
                    list.ForEach(x =>
                    {
                        AdminDetails details = new AdminDetails();
                        details.CreatedDate = x.CreatedDate;
                        details.Email = x.Email;
                        details.ID = x.ID;
                        details.Isdeleted = x.Isdeleted;
                        details.ModifiedDate = x.ModifiedDate;
                        details.Password = x.Password;
                        details.PhoneNumber = x.PhoneNumber;
                        details.Role = x.Role;
                        details.UserName = x.UserName;
                        adminDetails.Add(details);
                    });
                }
            }
            catch (Exception ex)
            {

            }
            return adminDetails;
        }

        public string SaveAdminDtails(AdminDetails data)
        {
            string result = Failed;
            try
            {
                if (data != null)
                {
                    DAL.DataAccess.AdminDetail adminDetail = new DAL.DataAccess.AdminDetail();
                    adminDetail.Email = data.Email;
                    adminDetail.ID = data.ID;
                    adminDetail.Password = data.Password;
                    adminDetail.PhoneNumber = data.PhoneNumber;
                    adminDetail.Role = data.Role;
                    adminDetail.UserName = data.Role;
                    result = homeDAL.SaveAdminDetails(adminDetail);
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
                result = homeDAL.DeleteAdmin(id);
            }
            catch(Exception ex)
            {

            }
            return result;
        }
    }
}
