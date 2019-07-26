using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LAB2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dock()
        {
            ViewBag.Message = "Inland Marina Dock Info.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Inland Marina Info.";

            return View();
        }

        public ActionResult Leasing()
        {
            ViewBag.Message = "Inland Marina Leasing.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "What do you think?";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(string message)
        {
            //TO DO: save this and act on it

            ViewBag.Message = "Thanks for the feedback.";

            return View();
        }

    }
}