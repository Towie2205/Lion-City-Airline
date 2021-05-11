using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace web2020apr_p08_t5.Controllers
{
    public class CustomerHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
