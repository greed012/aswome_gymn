using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using awsome_gymn.Models;

namespace awsome_gymn.Controllers
{
    [AdminAuthorize]
    public class AttendancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attendances
        public ActionResult Index()
        {
            var attendances = db.Attendances.Include(a => a.TrainingSession).Include(a => a.User).Include(a => a.Class);
            return View(attendances.ToList());
        }

        // GET: Attendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: Attendances/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name");
            return View();
        }

        [HttpPost]
        public JsonResult GetTrainingSessions(int classId)
        {
            var sessions = db.TrainingSessions.Where(ts => ts.ClassId == classId)
                .Select(ts => new
                {
                    Id = ts.Id,
                    Time = ts.Time
                })
                .ToList();

            return Json(sessions);
        }

        [HttpPost]
        public JsonResult GetUsersInSession(int trainingSessionId)
        {
            var users = db.Enrollments
                .Where(e => e.TrainingSessionId == trainingSessionId)
                .Select(e => new
                {
                    UserId = e.UserId,
                    UserName = e.User.Email
                })
                .ToList();

            return Json(users);
        }

        // POST: Attendances/Create
        [HttpPost]
        public ActionResult Create(AttendanceCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Loop through the selected user IDs and create attendance records
                foreach (string userId in viewModel.SelectedUserIds)
                {
                    var attendance = new Attendance
                    {
                        UserId = userId,
                        ClassId = viewModel.ClassId,
                        TrainingSessionId = viewModel.TrainingSessionId,
                        AttendanceDate = viewModel.AttendanceDate,
                        Comments = viewModel.Comments,
                        IsPresent = viewModel.IsPresent
                    };
                    db.Attendances.Add(attendance);
                }

                db.SaveChanges();

                // Return a redirect URL to the Index action
                return Json(new { redirectTo = Url.Action("Index") });
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", viewModel.ClassId);
            return View(viewModel);
        }

        // GET: Attendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", attendance.ClassId);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,TrainingSessionId,ClassId,AttendanceDate,Comments,IsPresent")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", attendance.ClassId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendances.Find(id);
            db.Attendances.Remove(attendance);
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