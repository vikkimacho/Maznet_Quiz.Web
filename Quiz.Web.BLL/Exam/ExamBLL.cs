using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Web.DAL.DAL;
using Quiz.Web.DTO.Models;

namespace Quiz.Web.BLL.Exam
{
    public class ExamBLL
    {
        ExamDAL examDAL = new ExamDAL();
        public string GetExamPortal(Guid assessmentID)
        {
            string result = "Failed";
            try
            {
                var examAssessmentDetail = examDAL.ValidateAssessmentID(assessmentID);
                if (examAssessmentDetail.Count() > 0)
                {
                    result = "Success";
                }
            }
            catch (Exception)
            {

            }
            return result;
        }

        public string ValidateExaminer(string username, string password, string assessmentID)
        {
            string result = "FAILED";
            try
            {
                result = examDAL.ValidateExaminer(username, password, new Guid(assessmentID));
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }



        public List<CustomRegistration> GetRegistration(string assessmentID)
        {
            List<CustomRegistration> result = new List<CustomRegistration>();
            try
            {
                var data = examDAL.GetRegistration(new Guid(assessmentID));
                foreach (var item in data)
                {
                    CustomRegistration customRegistration = new CustomRegistration();
                    customRegistration.AssessmentId = item.AssessmentId;
                    customRegistration.Createddate = item.Createddate;
                    customRegistration.DisplayFieldName = item.DisplayFieldName;
                    customRegistration.FieldName = item.FieldName;
                    customRegistration.FieldType = item.FieldType;
                    customRegistration.FormId = item.FormId;
                    customRegistration.id = item.id;
                    customRegistration.IsEnabled = item.IsEnabled;
                    customRegistration.IsLocked = item.IsLocked;
                    customRegistration.IsMandatory = item.IsMandatory;
                    customRegistration.ModificationHistory = item.ModificationHistory;
                    customRegistration.ModifiedDate = item.ModifiedDate;
                    customRegistration.Remarks = item.Remarks;
                    customRegistration.Values = item.Values;
                    result.Add(customRegistration);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }
    }
}
