using awsome_gymn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace awsome_gymn.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
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

        public ActionResult Class_List()
        {
            ViewBag.Message = "Your class_list page.";
            return View(db.Classes.ToList());
        }
        public ActionResult Membership_List()
        {
            ViewBag.Message = "Your class_list page.";
            return View(db.memberships.ToList());
        }
        public ActionResult Trainer()
        {
            ViewBag.Message = "Your trainer page.";

            return View(db.Trainers.ToList());
        }
        public ActionResult Schedule()
        {
            ViewBag.Message = "Your schedule page.";
            var trainingSessions = db.TrainingSessions.Include(t => t.Class).Include(t => t.Trainer);
            return View(trainingSessions.ToList());

        }
    }
}