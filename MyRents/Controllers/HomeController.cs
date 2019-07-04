using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MyRents.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // Action Filter - It will be executed before and/or after the action depending how it was implemented
        // Enabling caching for this page
        [OutputCache (Duration = 50, Location = OutputCacheLocation.Server,VaryByParam = "genre")]  
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}