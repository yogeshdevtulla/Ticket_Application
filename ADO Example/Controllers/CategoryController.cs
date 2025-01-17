using System;
using System.Diagnostics;
using System.Web.Mvc;
using ADO_Example.Data;
using ADO_Example.Models;
using ADOExample.Data;
using ADOExample.Models;

namespace ADO_Example.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Category_Data _categoryData;

        public CategoryController()
        {
            _categoryData = new Category_Data(); 
        }

        
        public ActionResult Index()
        {
            try
            {
                var categories = _categoryData.GetAllCategories(); 
                return View(categories); 
            }
            catch (Exception ex)
            {

                
                ViewBag.ErrorMessage = "An error occurred while fetching category data.";
                return View("Error" + ex.Message);
                throw;
            }
        }

        // Add GET
        public ActionResult Add()
        {
            try
            {
                ViewBag.departments = _categoryData.GetAllDepartments();
                
                return View(); 
            }
            catch (Exception ex)
            {
                
                ViewBag.ErrorMessage = "An error occurred while fetching department data.";
                return View("Error" + ex.Message);
                throw;
            }
        }

        // Add New Category (AJAX-enabled)
        [HttpPost]
        public ActionResult Add(string categoryName, string departmentName, string status)
        {
            try
            {
                if (string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(departmentName) || string.IsNullOrEmpty(status))
                {
                    return Json(new { success = false, message = "Category Name, Department, and Status are required." });
                }

                string userIp = Request.UserHostAddress;
                _categoryData.InsertCategory(categoryName, departmentName, status, userIp);

                return Json(new { success = true, message = "Category added successfully." });
            }
            catch (Exception ex)
            {
                
                return Json(new { success = false, message = "An error occurred while adding the category: " + ex.Message });
            }
        }

        // Delete Category
        [HttpPost]
        public ActionResult Delete(int categoryId)
        {
            try
            {
                _categoryData.DeleteCategory(categoryId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                
                ViewBag.ErrorMessage = "An error occurred while deleting the category.";
                return View("Error" + ex.Message);
                throw;
            }
        }

        // Edit GET
        public ActionResult Edit(int id)
        {
            try
            {
                var category = _categoryData.GetCategoryById(id); 
                if (category == null)
                {
                    return HttpNotFound();
                }

                
                var viewModel = new CategoryEditViewModel
                {
                    CategoryId = category.CategoryID,
                    CategoryName = category.CategoryName,
                    DepartmentName = category.DepartmentName,
                    Status = category.Status,
                    DepartmentList = _categoryData.GetAllDepartments() 
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
               
                ViewBag.ErrorMessage = "An error occurred while fetching category details.";
                return View("Error" + ex.Message);
                throw;
            }
        }

        // Edit POST
        [HttpPost]
        public ActionResult Edit(CategoryEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string updatedIP = Request.UserHostAddress; 
                    _categoryData.UpdateCategory(model, updatedIP);
                    return RedirectToAction("Index");
                }

                
                model.DepartmentList = _categoryData.GetAllDepartments();
                return View(model);
            }
            catch (Exception ex)
            {
               
                ViewBag.ErrorMessage = "An error occurred while updating the category.";
                return View("Error" + ex.Message);
                throw;
            }
        }
    }
}