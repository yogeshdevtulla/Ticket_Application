using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADO_Example.Models
{
    public class TATReportModel
    {
        public DateTime CreatedDate { get; set; }
        public string ShowDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string CloseDate { get; set; }
        public string TicketNumber { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Status { get; set; }


        public int? TAT { get; set; } 

        public string TATDate { get; set; }
    }
}