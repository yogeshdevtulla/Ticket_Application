using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADO_Example.Models
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedIP { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedIP { get; set; }
    }
    public class DepartmentEditViewModel
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string Status { get; set; }
        public List<DepartmentModel> DepartmentList { get; set; }
    }
}