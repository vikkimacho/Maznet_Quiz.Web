using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Web.DAL.DataAccess;
using Quiz.Web.DTO;
using Quiz.Web.DTO.Models;
using System.Data.SqlClient;

namespace Quiz.Web.DAL.Home
{
   public class AssessMentDAL
    {
        public string CreateAssessMent()
        {
            return "";
        }


        /// <summary>
        /// This method will return the page modal from DB SP and also from single entity from table values but completely customized modals.
        /// </summary>
        /// <returns></returns>
        public AssesmentPageModal GetAssessmentPageModal()
        {
            AssesmentPageModal assesmentPageModal = new AssesmentPageModal();
            using (TestEngineEntities TestEngineDBContext = new TestEngineEntities())
            {
                assesmentPageModal.LQuestionBankModal = TestEngineDBContext.Database.SqlQuery<QuestionBankModal>("exec Assesmentpagemodal").ToList();
            }
            return assesmentPageModal;
        }
    }
}
