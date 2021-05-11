using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web2020apr_p08_t5.DAL;
using web2020apr_p08_t5.Models;


namespace web2020apr_p08_t5.Controllers
{
    public class StaffController : Controller
    {
        private StaffDAL staffContext = new StaffDAL();

        // GET: StaffController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PersonnelHomepage()
        {
            return View();
        }

        // GET: StaffController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StaffController/Create
        public ActionResult CreatePersonnel()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePersonnel(Staff staff)
        {
            if (ModelState.IsValid)
            {
                staff.Password = "p@55Staff";
                staff.DateEmployed = DateTime.Now;
                staff.StaffName = staffContext.Add(staff);
                TempData["Success"] = "New Staff was created";
                return RedirectToActionPermanent("Index", "Staff");
            }
            else
            {
                TempData["unsuccessful"] = "User was not able to be created";
                return RedirectToAction("Create");
            }
        }

        // POST: StaffController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StaffController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StaffController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StaffController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StaffController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
