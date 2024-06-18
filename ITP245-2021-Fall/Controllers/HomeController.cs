using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITP245_2021_Fall.Models;

namespace ITP245_2021_Fall.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult About()
        {
            
            ViewBag.Message = "Rebecca Gilliam | ";
            ViewBag.CurrentTime = DateTime.Now;
            return View();


            List<AboutModel> listcollege = new List<AboutModel>();
            AboutModel colleges = new AboutModel();


            colleges.college = "Virginia Commonwealth University";
            colleges.degree = "Masters of Decision Analytics";
            colleges.years = "2017 - 2019";

            colleges = new AboutModel();
            colleges.college = "Virginia Community College System";
            colleges.degree = "Non-Degree Seeking";
            colleges.years = "2013 - Present";

            colleges = new AboutModel();
            colleges.college = "Christopher Newport University";
            colleges.degree = "Non-Degree Seeking";
            colleges.years = "2005 - 2006";

            colleges = new AboutModel();
            colleges.college = "Virginia Commonwealth University";
            colleges.degree = "Non-Degree Seeking";
            colleges.years = "2003 - 2005";

            colleges = new AboutModel();
            colleges.college = "Mary Baldwin University";
            colleges.degree = "B.S. Chemistry, Minor Leadership";
            colleges.years = "1999 - 2003";


            return View(colleges);
        }
    

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
           
    }
}