using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADO_Example.Models
{
    public class Ticket
    {
        public string TicketNo { get; set; }
        public int DepartmentID { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public string Priority { get; set; }
        public string Feedback { get; set; }
        public string Status { get; set; }
        public string CreateDate { get; set; }
        public string CreateIP { get; set; }
    }

    public class TicketGridModel
    {
        public DateTime CreateDate { get; set; }
        public string ShowDate { get; set; }
        public string TicketNo { get; set; }
        public string DepartmentName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string Status { get; set; }
    }

    public class TicketUpdateModel
    {
        public string TicketNo { get; set; }
        public DateTime CreateDate { get; set; }
        public string ShowDate { get; set; }
        public string DepartmentName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string Status { get; set; }
        public string Action { get; set; }
        public string Remarks { get; set; }
        public string Feedback { get; set; }
        //public string Action { get; set; }  
       
    }



}