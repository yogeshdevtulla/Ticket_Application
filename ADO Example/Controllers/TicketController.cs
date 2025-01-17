using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADO_Example.Data;
using ADO_Example.Models;
using ADOExample.Data;

namespace ADO_Example.Controllers
{
    public class TicketController : Controller
    {
        private readonly Ticket_Data ticketData;
        private readonly Department_Data departmentData;
        private readonly Category_Data categoryData;
        private readonly SubCategory_Data subCategoryData;

        public TicketController()
        {
            ticketData = new Ticket_Data();
            departmentData = new Department_Data();
            categoryData = new Category_Data();
            subCategoryData = new SubCategory_Data();
        }

        // Action to show the 'Add Ticket' form
        public ActionResult AddTicket()
        {
            try
            {
                ViewBag.Departments = departmentData.GetAllDepartments();
                ViewBag.Categories = categoryData.GetAllCategories();
                ViewBag.SubCategories = subCategoryData.GetAllSubCategories();
                ViewBag.Priorities = new List<string> { "High", "Medium", "Low" };
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while loading the Add Ticket form: {ex.Message}";
                return RedirectToAction("Index");
                throw;
            }
        }

        [HttpPost]
        public ActionResult AddTicket(Ticket ticket)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ticket.CreateIP = Request.UserHostAddress;
                    ticketData.AddTicket(ticket);
                    TempData["Message"] = "Ticket added successfully!";
                    return RedirectToAction("Index");
                }
                ViewBag.Departments = departmentData.GetAllDepartments();
                ViewBag.Categories = categoryData.GetAllCategories();
                ViewBag.SubCategories = subCategoryData.GetAllSubCategories();
                ViewBag.Priorities = new List<string> { "High", "Medium", "Low" };
                return View(ticket);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while adding the ticket: {ex.Message}";
                ViewBag.Departments = departmentData.GetAllDepartments();
                ViewBag.Categories = categoryData.GetAllCategories();
                ViewBag.SubCategories = subCategoryData.GetAllSubCategories();
                ViewBag.Priorities = new List<string> { "High", "Medium", "Low" };
                return View(ticket);
                throw;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetTickets()
        {
            try
            {
                var tickets = ticketData.GetTickets();
                return Json(tickets, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching tickets: " + ex.Message);

                return Json(new { success = false, message = $"Error: {ex.Message}" });
                throw;
            }
        }

        // Action to enter ticket number
        public ActionResult EnterTicketNumber()
        {
            return View();
        }

        // Action to show details for a specific ticket
        public ActionResult UpdateTicket(string ticketNo)
        {
            try
            {
                if (string.IsNullOrEmpty(ticketNo))
                {
                    return RedirectToAction("EnterTicketNumber");
                }

                var ticketDetails = ticketData.GetTicketByTicketNo(ticketNo);
                if (ticketDetails == null)
                {
                    return HttpNotFound("Ticket not found.");
                }

                return View(ticketDetails);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while fetching the ticket: {ex.Message}";
                return RedirectToAction("EnterTicketNumber");
                throw;
            }
        }

        // Action to handle ticket status update
        [HttpPost]
        public ActionResult UpdateTicket(TicketUpdateModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool success = ticketData.UpdateTicketStatus(model);
                    if (success)
                    {
                        TempData["Message"] = "Ticket updated successfully!";
                        return RedirectToAction("Index");
                    }

                    ViewBag.ErrorMessage = "Error updating ticket.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while updating the ticket: {ex.Message}";
            }

            return View(model);
        }

        // Action to get ticket details by ticket number (via AJAX)
        [HttpPost]
        public ActionResult GetTicketDetails(string ticketNo)
        {
            try
            {
                var ticket = ticketData.GetTicketByTicketNo(ticketNo);
                if (ticket != null)
                {
                    return Json(new { success = true, ticket = ticket });
                }
                return Json(new { success = false, message = "Ticket not found" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching ticket details: " + ex.Message);

                return Json(new { success = false, message = $"Error: {ex.Message}" });
                throw;
            }
        }
    }
}