using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADO_Example.Models
{
    public class DashboardViewModel
    {
        public int OpenTickets { get; set; }
        public int ClosedTickets { get; set; }
        public int ResolvedTickets { get; set; }
        public int TotalUsers { get; set; }
    }

    public class DepartmentTicketData
    {
        public string DepartmentName { get; set; }
        public int TicketCount { get; set; }
    }

    public class TicketStatusByDate
    {
        public DateTime TicketDate { get; set; }
        public int OpenTickets { get; set; }
        public int ClosedTickets { get; set; }
        public int ResolvedTickets { get; set; }
    }
}