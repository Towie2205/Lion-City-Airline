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
    public class FlightRouteController : Controller
    {
        private FlightRouteDAL flightrouteContext = new FlightRouteDAL();
        // GET: FlightRouteController
        public ActionResult Index()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            //if ((HttpContext.Session.GetString("Role") == null) ||
            //    (HttpContext.Session.GetString("Role") != "Admin"))
            //{
            //    return RedirectToAction("Homepage", "Home");
            //}

            List<FlightRoute> FlightRouteList = flightrouteContext.GetAllFlightRoute();
            return View(FlightRouteList);
        }

        // GET: FlightRouteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FlightRouteController/Create
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

        // POST: FlightRouteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FlightRoute FlightRoute)
        {
            
            //in case of the need to return to Create.cshtml view
            
            if (ModelState.IsValid)
            {
                //Add staff record to database
                FlightRoute.RouteID = flightrouteContext.Add(FlightRoute);
                //Redirect user to FlightRoute/Index view
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message                 
                return View(FlightRoute);
            }
        }

        // GET: FlightRouteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FlightRouteController/Edit/5
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

        // GET: FlightRouteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FlightRouteController/Delete/5
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
