using LAB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LAB2.Controllers
{
    public class DockController : Controller
    {
        // 1. *************RETRIEVE ALL DOCK DETAILS ******************
        // GET: All Dock details
        public ActionResult Index()
        {
            DockDB Dock = new DockDB();
            ModelState.Clear();
            return View(DockDB.GetAllDock());
        }

    }
}