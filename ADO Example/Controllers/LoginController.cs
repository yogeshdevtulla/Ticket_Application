using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using LoginForm.User;
using ADO_Example.User;



namespace ADO_Example.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserRepository userRepository;
        public LoginController()
        {
            userRepository = new UserRepository();
        }
        // Step 1: Display the Login Page
        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // Step 2: Authenticate Username

        // POST: Login/Create
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Authenticate(string username, string password)
        {
            if (!userRepository.ValidateUsername(username))
            {
                ViewBag.Error = "Invalid username";
                return View("Login");
            }
            if (!userRepository.ValidatePassword(username, password))
            {
                ViewBag.Error = "Invalid password";
                return View("Login");
            }
            // Successful login
            // Successful login, set session variables
            // Set session timeout to 10 minutes
            Session["User"] = username; // Store the username

            //return RedirectToAction("Success");

            //Redirection to the view of product
            return RedirectToAction("Index", "Dashboard");

        }


        //public ActionResult Success()
        //{
        //    return Content("Login Successful!");
        //}

        [AllowAnonymous]
        // Logout Action
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }


    }
}