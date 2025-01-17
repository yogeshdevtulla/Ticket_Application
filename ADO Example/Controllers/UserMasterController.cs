using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ADO_Example.Data;
using ADO_Example.Models;
using ADO_Example.User;
using ADOExample.Data;


namespace ADO_Example.Controllers
{
    public class UserMasterController : Controller
    {
        private readonly UserMaster_Data _userMasterData;
        private readonly Department_Data _departmentData;
        private readonly RoleMasterData _rolemaster;
        public UserMasterController()
        {
            _userMasterData = new UserMaster_Data();
            _departmentData = new Department_Data();
            _rolemaster = new RoleMasterData();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetUserMasterData()
        {
            var users = _userMasterData.GetAllUserMasterData();
            return Json(users, JsonRequestBehavior.AllowGet);
        }


        // Action to show the "Add New User" form
        public ActionResult Add()
        {
            var departments = _departmentData.GetAllDepartments();

            ViewBag.Departments = new SelectList(departments, "Id", "DepartmentName");

            var role = _rolemaster.GetRoles();
            ViewBag.Role = new SelectList(role, "RoleId", "RoleName");
            return View();
        }

        [HttpPost]

        public ActionResult Add(UserMaster user)
        {
            if (ModelState.IsValid)
            {

                _userMasterData.AddNewUser(user);

                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = _userMasterData.GetUserMasterDataById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var departments = _departmentData.GetAllDepartments();
            ViewBag.Departments = new SelectList(departments, "ID", "DepartmentName", user.DepartmentID);

            return View(user);  
        }

        [HttpPost]
        public ActionResult Edit(UserMaster model)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = _userMasterData.EditUserMaster(model);
                if (isUpdated)
                {
                    return RedirectToAction("Index", "UserMaster");
                }
            }

            return Json(new { success = false, message = "An error occurred while updating the user." });
        }


        //delete new
        public JsonResult DeleteUser(int userID)
        {
            var result = _userMasterData.DeleteUser(userID);
            return Json(new { success = result });
        }





    }


}