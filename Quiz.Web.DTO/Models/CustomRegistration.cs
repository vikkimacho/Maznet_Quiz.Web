using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Web.DTO.Models
{
    public class CustomRegistration
    {
        public System.Guid id { get; set; }
        public System.Guid FormId { get; set; }
        public string DisplayFieldName { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public bool IsMandatory { get; set; }
        public string Values { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string Remarks { get; set; }
        public string ModificationHistory { get; set; }
        public bool IsLocked { get; set; }
        public bool IsEnabled { get; set; }
        public System.DateTime Createddate { get; set; }
        public System.Guid AssessmentId { get; set; }
    }
}
