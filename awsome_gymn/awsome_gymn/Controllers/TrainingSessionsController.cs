using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using awsome_gymn.Models; // Change to the correct namespace

namespace awsome_gymn.Controllers
{
    [AdminAuthorize] // Add this attribute to enforce admin authorization for all actions in this controller
    public class TrainingSessionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrainingSessions
        public ActionResult Index()
        {
            var trainingSessions = db.TrainingSessions.Include(t => t.Class).Include(t => t.Trainer);
            return View(trainingSessions.ToList());
        }

        // GET: TrainingSessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingSession trainingSession = db.TrainingSessions.Find(id);
            if (trainingSession == null)
            {
                return HttpNotFound();
            }
            return View(trainingSession);
        }

        // GET: TrainingSessions/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name");
            ViewBag.TrainerId = new SelectList(db.Trainers, "Id", "Name");
            return View();
        }

        // POST: TrainingSessions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Time,Group,Duration,ClassId,TrainerId")] TrainingSession trainingSession)
        {
            if (ModelState.IsValid)
            {
                db.TrainingSessions.Add(trainingSession);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", trainingSession.ClassId);
            ViewBag.TrainerId = new SelectList(db.Trainers, "Id", "Name", trainingSession.TrainerId);
            return View(trainingSession);
        }

        // GET: TrainingSessions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingSession trainingSession = db.TrainingSessions.Find(id);
            if (trainingSession == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", trainingSession.ClassId);
            ViewBag.TrainerId = new SelectList(db.Trainers, "Id", "Name", trainingSession.TrainerId);
            return View(trainingSession);
        }

        // POST: TrainingSessions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Time,Group,Duration,ClassId,TrainerId")] TrainingSession trainingSession)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainingSession).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", trainingSession.ClassId);
            ViewBag.TrainerId = new SelectList(db.Trainers, "Id", "Name", trainingSession.TrainerId);
            return View(trainingSession);
        }

        // GET: TrainingSessions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingSession trainingSession = db.TrainingSessions.Find(id);
            if (trainingSession == null)
            {
                return HttpNotFound();
            }
            return View(trainingSession);
        }

        // POST: TrainingSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainingSession trainingSession = db.TrainingSessions.Find(id);
            db.TrainingSessions.Remove(trainingSession);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
