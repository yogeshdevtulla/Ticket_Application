using System;
using System.Linq;
using System.Web.Mvc;
using ADO_Example.Data;
using ADO_Example.Models;

namespace ADO_Example.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly Department_Data _departmentData;

        public DepartmentController()
        {
            _departmentData = new Department_Data();
        }

        // Display the Grid
        public ActionResult Index()
        {
            try
            {
                var departments = _departmentData.GetAllDepartments();
                return View(departments);
            }
            catch (Exception)
            {
                
                ViewBag.ErrorMessage = "An error occurred while fetching department data.";
                return View("Error");
                throw;
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        // Add New Department (AJAX-enabled)
        [HttpPost]
        public ActionResult Add(string departmentName, string status)
        {
            try
            {
                if (string.IsNullOrEmpty(departmentName) || string.IsNullOrEmpty(status))
                {
                    return Json(new { success = false, message = "Department Name and Status are required." });
                }

                string userIp = Request.UserHostAddress;
                _departmentData.InsertDepartment(departmentName, status, userIp);

                
                return Json(new { success = true, message = "Department added successfully." });
            }
            catch (Exception ex)
            {
                
                return Json(new { success = false, message = "An error occurred while adding the department: " + ex.Message });
                throw;
            }
        }

        // Delete Department
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _departmentData.DeleteDepartment(id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "An error occurred while deleting the department.";
                return View("Error");
                throw;
            }
        }

        // Get method for Edit
        public ActionResult Edit(int id)
        {
            try
            {
                var department = _departmentData.GetAllDepartments().FirstOrDefault(d => d.Id == id);
                if (department == null)
                {
                    return HttpNotFound();
                }

                var departmentList = _departmentData.GetAllDepartments();

                var editViewModel = new DepartmentEditViewModel
                {
                    Id = department.Id,
                    DepartmentName = department.DepartmentName,
                    Status = department.Status,
                    DepartmentList = departmentList
                };

                return View(editViewModel);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching department details.";
                return View("Error");
                throw;
            }
        }

        // Post method for Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string userIp = Request.UserHostAddress;
                    _departmentData.UpdateDepartment(model.Id, model.DepartmentName, model.Status, userIp);
                    return RedirectToAction("Index");
                }

                model.DepartmentList = _departmentData.GetAllDepartments(); // Repopulate department list for the dropdown
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "An error occurred while updating the department.";
                return View("Error");
                throw;
            }
        }
    }
}