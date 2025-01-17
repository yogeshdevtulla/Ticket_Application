using System;
using System.Linq;
using System.Web.Mvc;
using ADO_Example.Data;
using ADO_Example.Models;

namespace ADO_Example.Controllers
{
    public class ProductController : Controller
    {
        Product_Data _product_data = new Product_Data(); //constructor of a product claaa in model
        // GET: Product
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Index()
        {
            var productList = _product_data.GetAllProducts();
            if (productList.Count == 0)
            {
                TempData["infoMessage"] = "currently no data is available in the database";
            }
            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();

        }


        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            bool IsInserted = false;
            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _product_data.InsertProduct(product);
                    if (IsInserted)
                    {
                        TempData["SucessMessage"] = "Product details inserted sucessfully.....!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Save Data. Pleaser Try Again .....!";
                    }


                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();

            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var products = _product_data.GetProductByID(id).FirstOrDefault();
            if (products == null)
            {
                TempData["infoMessage"] = "Product Not Available with Id" + id.ToString();
                return RedirectToAction("Index");

            }
            return View(products);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _product_data.UpdateProduct(product);
                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Product details updated successfully!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Product already exists or unable to update details.";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(product);
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}