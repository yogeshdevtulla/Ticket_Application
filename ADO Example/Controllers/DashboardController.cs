using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADO_Example.Data;

namespace ADO_Example.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardRepository _repository;

        public DashboardController()
        {
            _repository = new DashboardRepository();
        }


        public ActionResult PieChart()
        {
            var departmentData = _repository.GetTicketsByDepartment();
            return Json(departmentData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BarGraph()
        {
            var statusData = _repository.GetTicketsStatusByDate();
            return Json(statusData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            var dashboardData = _repository.GetDashboardData();
            return View(dashboardData);
        }
        public ActionResult GetOpenTickets()
        {
            var openTickets = _repository.GetOpenTickets();
            return Json(openTickets, JsonRequestBehavior.AllowGet);
        }

    }
}