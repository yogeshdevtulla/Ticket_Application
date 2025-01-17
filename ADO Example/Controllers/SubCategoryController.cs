using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ADO_Example.Data;
using ADO_Example.Models;
using ADOExample.Data;

namespace ADO_Example.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly SubCategory_Data _subcategoryData;

        // Constructor to initialize the data layer
        public SubCategoryController()
        {
            _subcategoryData = new SubCategory_Data();
        }

        // GET: SubCategory/Index
        public ActionResult Index()
        {
            try
            {
                var subcategories = _subcategoryData.GetAllSubCategories(); // Fetch all subcategories
                return View(subcategories);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the subcategories.";
                return RedirectToAction("Error" + ex.Message);
                throw;
            }
        }

        // GET: SubCategory/Add
        public ActionResult Add()
        {
            try
            {
                // Populate dropdown data
                ViewBag.Categories = _subcategoryData.GetAllCategories();
                ViewBag.Depratments = _subcategoryData.GetAllDepartments();
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while preparing the Add page.";
                return RedirectToAction("Error" + ex.Message);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Add(SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _subcategoryData.InsertSubCategory(subCategory);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, "An error occurred while adding the subcategory: " + ex.Message);
                    throw;
                }
            }

            ViewBag.Categories = _subcategoryData.GetAllCategories();
            ViewBag.Depratments = _subcategoryData.GetAllDepartments();

            return View(subCategory);
        }

        [HttpPost]
        public ActionResult Delete(int subCategoryID)
        {
            _subcategoryData.DeleteSubCategory(subCategoryID);
            return RedirectToAction("Index");

        }



        //edit actiion
        
        public ActionResult Edit(int id)
        {
            var subCategory = _subcategoryData.GetSubCategoryById(id);

            if (subCategory == null)
            {
                return HttpNotFound();
            }

            // Populate dropdown data
            ViewBag.Categories = _subcategoryData.GetAllCategories();
            ViewBag.Departments = _subcategoryData.GetAllDepartments();

            return View(subCategory);
        }

        // POST: SubCategory/Edit
        [HttpPost]
        public ActionResult Edit(SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _subcategoryData.UpdateSubCategory(subCategory);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the subcategory: " + ex.Message);
                    throw;
                }
            }

            ViewBag.Categories = _subcategoryData.GetAllCategories();
            ViewBag.Departments = _subcategoryData.GetAllDepartments();

            return View(subCategory);
        }






    }
}