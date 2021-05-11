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
    public class FlightPersonnelController : Controller
    {
        private FlightPersonnelDAL flightpersonnelContext = new FlightPersonnelDAL();
        // GET: FlightPersonnelController
        public ActionResult Index()
        {
            List<FlightPersonnel> flightpersonnelList = flightpersonnelContext.GetAllFlightPersonnel();
            return View(flightpersonnelList);
        }

        // GET: FlightPersonnelController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FlightPersonnelController/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult _ViewFlightPersonnel()
        {
            return View();
        }

        // POST: FlightPersonnelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FlightPersonnel FlightPersonnel)
        {
            //in case of the need to return to Create.cshtml view

            if (ModelState.IsValid)
            {
                //Add staff record to database
                FlightPersonnel.StaffId = flightpersonnelContext.Add(FlightPersonnel);
                //Redirect user to FlightSchedule/Index view
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message                 
                return View(FlightPersonnel);
            }
        }

        // GET: FlightPersonnelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FlightPersonnelController/Edit/5
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

        // GET: FlightPersonnelController/Delete/5
        public ActionResult Delete(int? id)
        {
            //Stop accessing the action if not logged in
            // or account not in the "Staff" role
            //if ((HttpContext.Session.GetString("Role") == null) ||
            //    (HttpContext.Session.GetString("Role") != "Staff"))
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            if (id == null)
            {
                //Query string parameter not provided
                //Return to listing page, not allowed to edit                 
                return RedirectToAction("Index");
            }

            FlightPersonnel flightpersonnel = flightpersonnelContext.GetDetails(id.Value);
            if (flightpersonnel == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            return View(flightpersonnel);
        }

        // POST: FlightPersonnelController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FlightPersonnel flightpersonnel)
        {
            // Delete the staff record from database     
            flightpersonnelContext.Delete(flightpersonnel.StaffId);
            return RedirectToAction("Index");
        }
    }
}
