using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAB2.Models;

namespace LAB2.Controllers
{
    public class SlipAndLeaseController : Controller
    {
        // GET: SlipAndLease
        public ActionResult Index()
        {
            Models.SlipDB spd = new Models.SlipDB();
            ModelState.Clear();
            return View(SlipDB.GetAllHoldLease());
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            SlipAndLease currentSlipAndLease = SlipDB.GetSlipAndLeaseByID(id);
            if (currentSlipAndLease != null)
            {
                //ViewBag.ImagePath = "~/Content/Images/" + currentProduct.ImageFile;
                return View(currentSlipAndLease);
            }
            else
            {
                return View();
            }
        }

    }
}