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
    public class FlightScheduleController : Controller
    {
        private FlightScheduleDAL flightscheduleContext = new FlightScheduleDAL();
        // GET: FlightScheduleController
        public ActionResult Index()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            //if ((HttpContext.Session.GetString("Role") == null) ||
            //    (HttpContext.Session.GetString("Role") != "Admin"))
            //{
            //    return RedirectToAction("Homepage", "Home");
            //}

            List<FlightSchedule> FlightScheduleList = flightscheduleContext.GetAllFlightSchedule();
            return View(FlightScheduleList);
        }

        // GET: FlightScheduleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FlightScheduleController/Create
        public ActionResult Create()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            //if ((HttpContext.Session.GetString("Role") == null) ||
            //    (HttpContext.Session.GetString("Role") != "Admin"))
            //{
            //    return RedirectToAction("Homepage", "Home");
            //}


            return View();
        }

        // POST: FlightScheduleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FlightSchedule FlightSchedule)
        {
            //in case of the need to return to Create.cshtml view

            if (ModelState.IsValid)
            {
                //Add staff record to database
                FlightSchedule.ScheduleID = flightscheduleContext.Add(FlightSchedule);
                //Redirect user to FlightSchedule/Index view
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message                 
                return View(FlightSchedule);
            }
        }

        // GET: FlightScheduleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FlightScheduleController/Edit/5
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

        // GET: FlightScheduleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FlightScheduleController/Delete/5
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
