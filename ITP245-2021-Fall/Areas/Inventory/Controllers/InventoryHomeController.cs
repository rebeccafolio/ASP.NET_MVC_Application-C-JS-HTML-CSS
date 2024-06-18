using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITP245_2021_Fall.Areas.Inventory.Controllers
{
    public class InventoryHomeController : Controller
    {
        
        // GET: Inventory/Home
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Sales")]
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}