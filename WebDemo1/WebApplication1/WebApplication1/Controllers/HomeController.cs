using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcThrottle;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }

        [EnableThrottling(PerSecond = 2, PerMinute = 5, PerHour = 30, PerDay = 300)]
        public ActionResult Other()
        {
            return View();
        }

        [HttpPost]

        [EnableThrottling(PerSecond = 2, PerMinute = 5, PerHour = 30, PerDay = 300)]
        public ActionResult GetSth()
        {
            return Json(new { msg = true });
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

        public ActionResult Welcome(string name, int numTimes = 1)
        {
            ViewBag.Message = "Hello " + name;
            ViewBag.NumTimes = numTimes;

            return View();
        }


    }
}