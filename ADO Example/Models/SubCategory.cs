using System;

namespace ADO_Example.Models
{
    public class SubCategory
    {
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }
        public int Id { get; set; }
        
        public string DepartmentName { get; set; }
        public string Status { get; set; }

        // For backend only, not shown on the frontend 
        //public DateTime CreatedDate { get; set; }
        //public string CreatedIP { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        //public string UpdatedIP { get; set; }
    }

}
