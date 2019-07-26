using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAB2.Models;

namespace LAB2.Controllers
{
    public class LeaseController : Controller
    {
        // GET: Lease
        public ActionResult Index()
        {
            SlipDB slip = new SlipDB();
            ModelState.Clear();

            return View(SlipDB.GetAllAvailableDock());
        }
    }
}