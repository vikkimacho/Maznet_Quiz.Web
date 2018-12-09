using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Web.DAL.Home;
using Quiz.Web.DTO.Models;
using Newtonsoft.Json;

namespace Quiz.Web.BLL.Home
{
    public class AssessMentBLL
    {

        private AssessMentDAL ObjAssessMentDAL = new AssessMentDAL();
        public string CreateAssessMent()
        {
            var result= ObjAssessMentDAL.CreateAssessMent();
            return result;
        }
    }
}
