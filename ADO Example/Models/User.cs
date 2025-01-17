using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADO_Example.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string Designation { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
}