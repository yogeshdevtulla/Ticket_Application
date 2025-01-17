using System;
using System.Collections.Generic;
using ADO_Example.Models;

namespace ADOExample.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string DepartmentName { get; set; }
        public string Status { get; set; }

        // For backend only, not shown on the frontend 
        public DateTime CreatedDate { get; set; }
        public string CreatedIP { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedIP { get; set; }
    }
    public class CategoryEditViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string DepartmentName { get; set; }
        public string Status { get; set; }

        public List<DepartmentModel> DepartmentList { get; set; } // List of departments for dropdown
    }

}
