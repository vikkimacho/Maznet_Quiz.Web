using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Web.DTO.Models
{
    public class DashBoardDetailsView
    {
        public int Scheduled { get; set; }

        public int Completed { get; set; }        

        public int StrongConsider { get; set; }

        public int InProgress { get; set; }

        public int Expired { get; set; }

        public int Pending { get; set; }

        public int SingleLogin { get; set; }

        public int BulkLogin { get; set; }

        public int CommonLogin { get; set; }       


    }

    public class DashBoardRange
    {
       public string StartDatetime { get; set; }
       public string  EndDatetime { get; set; }
    }
}
