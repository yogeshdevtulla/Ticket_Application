using System.Web.Mvc;
using ADO_Example.Models;
using ADO_Example.Data;
using System.Net;
using System;

namespace ADO_Example.Controllers
{
    public class RoleMasterController : Controller
    {
        private readonly RoleMasterData _data = new RoleMasterData();

        // Action to display the RoleMaster view
        public ActionResult Index()
        {
            return View();
        }

        // Action to get roles via AJAX
        [HttpGet]
        public JsonResult GetRoles()
        {
            try
            {
                var roles = _data.GetRoles();
                return Json(roles, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log the exception (this can be done in a central place, e.g., a global exception handler)
                // You can replace this with your actual logging mechanism
                Console.WriteLine("Error fetching roles: " + ex.Message);

                return Json(new { success = false, message = "An error occurred while retrieving roles." }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        // Action to show the 'Add Role' view
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(RoleMaster role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _data.AddNewRole(role);
                    return RedirectToAction("Index");
                }
                return View(role);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error adding role: " + ex.Message);

                ModelState.AddModelError("", "An error occurred while adding the role.");
                return View(role);
                throw;
            }
        }

        // Action to display the 'Edit Role' view
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var role = _data.GetRoleById(id);
                if (role != null)
                {
                    return View(role);
                }
                else
                {
                    return HttpNotFound("Role not found.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error fetching role for edit: " + ex.Message);

                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
                throw;
            }
        }

        // Action to handle updating an existing role
        [HttpPost]
        public JsonResult UpdateRole(RoleMaster role)
        {
            try
            {
                var isUpdated = _data.UpdateRole(role);
                if (isUpdated)
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Failed to update role." });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error updating role: " + ex.Message);

                return Json(new { success = false, message = ex.Message });
                throw;
            }
        }

        // Action to get a role by ID via AJAX
        [HttpGet]
        public JsonResult GetRoleById(int id)
        {
            try
            {
                var role = _data.GetRoleById(id);
                return Json(role, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error fetching role by ID: " + ex.Message);

                return Json(new { success = false, message = "An error occurred while retrieving the role." }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }

        // Action to delete a role via AJAX
        [HttpPost]
        public JsonResult DeleteRole(int id)
        {
            try
            {
                var result = _data.DeleteRole(id);
                return Json(result);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error deleting role: " + ex.Message);

                return Json(new { success = false, message = "An error occurred while deleting the role." });
                throw;
            }
        }
    }
}