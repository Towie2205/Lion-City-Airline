using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web2020apr_p08_t5.Models;
using web2020apr_p08_t5.DAL;
using System.Reflection.Metadata;

namespace web2020apr_p08_t5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult StaffLogin()
        {
            return View();
        }

        public IActionResult StaffMain()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            // Clear all key-values pairs stored in session state
            HttpContext.Session.Clear();
            // Call the Index action of Home controller
            return RedirectToAction("Index");
        }

        public ActionResult Homepage()
        {
            return View("HomePage","Home");
        }

        public ActionResult CustomerMain()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        public ActionResult CustomerLogIn()

        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Package2()
        {
            return View();
        }

        private StaffDAL staffContext = new StaffDAL();

        [HttpPost]
        public ActionResult StaffMain(IFormCollection formData)
        {
            List<Staff> staffList = staffContext.GetAllStaff();

            string email = formData["txtemail"].ToString().ToLower();
            string password = formData["txtPassword"].ToString();

            foreach (Staff s in staffList)
            {
                if (email == s.EmailAddr && password == s.Password)
                {
                    HttpContext.Session.SetString("Email", email);
                    HttpContext.Session.SetString("Password", password);

                    return RedirectToAction("StaffMain");
                }
                
            }
            
            @TempData["Message"] = "Incorrect credentials";
            return RedirectToAction("StaffLogin");
        }

    }
}
