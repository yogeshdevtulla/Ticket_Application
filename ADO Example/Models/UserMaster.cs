using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ADO_Example.Models
{
    public class UserMaster
    {
        public int UserID { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Role { get; set; }

        [Required]
        public string Password { get; set; }

        public int DepartmentID { get; set; }

        public string Designation { get; set; }

        [Required]
        [Phone]
        public string MobileNo { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Status { get; set; }

        public string DepartmentName { get; set; }
    }
}