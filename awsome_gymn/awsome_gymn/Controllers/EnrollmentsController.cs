using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using awsome_gymn.Models;
using Microsoft.AspNet.Identity;

namespace awsome_gymn.Controllers
{
    public class EnrollmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page or another appropriate action
                return RedirectToAction("Login", "Account");
            }

            // Check if the user is in the "Admin" role
            if (User.IsInRole("admin"))
            {
                // Admins can view all enrollments
                var enrollments = db.Enrollments.Include(e => e.Class).Include(e => e.User).Include(e => e.TrainingSession);
                return View(enrollments.ToList());
            }
            else
            {
                // Regular users can only view their own enrollments
                var userId = User.Identity.GetUserId();
                var enrollments = db.Enrollments
                    .Where(e => e.UserId == userId)
                    .Include(e => e.Class)
                    .Include(e => e.User)
                    .Include(e => e.TrainingSession);

                return View(enrollments.ToList());
            }
        }

        // GET: Enrollments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page or another appropriate action
                return RedirectToAction("Login", "Account");
            }

            // Get the current user
            var userId = User.Identity.GetUserId();
            var currentUser = db.Users.Find(userId);

            if (currentUser == null)
            {
                // Handle the case where the authenticated user is not found
                return RedirectToAction("Index", "Home"); // Redirect to the home page or another appropriate action
            }

            if (User.IsInRole("Admin"))
            {
                // Admins can select any user when creating an enrollment
                ViewBag.Users = new SelectList(db.Users, "Id", "Email");
            }
            else
            {
                // Non-admin users can only register for themselves
                ViewBag.Users = new SelectList(db.Users.Where(u => u.Id == userId), "Id", "Email");
            }

            // Create a new Enrollment with pre-filled values
            var enrollment = new Enrollment
            {
                UserId = userId,
                EnrollmentDate = DateTime.Now
            };

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name");

            // Create an empty SelectList for training sessions initially
            ViewBag.TrainingSessions = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "Time");

            return View(enrollment);
        }


        // POST: Enrollments/GetTrainingSessions
        [HttpPost]
        public ActionResult GetTrainingSessions(int classId)
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

        // POST: Enrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ClassId,TrainingSessionId,EnrollmentDate")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", enrollment.ClassId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", enrollment.UserId);

            // Repopulate the TrainingSessions dropdown in case of validation errors
            ViewBag.TrainingSessions = new SelectList(
                db.TrainingSessions.Where(ts => ts.ClassId == enrollment.ClassId),
                "Id",
                "Time"
            );

            return View(enrollment);
        }

        // Other action methods (Edit, Delete, etc.) remain the same
        public ActionResult Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page or another appropriate action
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find the enrollment
            Enrollment enrollment = db.Enrollments.Find(id);

            if (enrollment == null)
            {
                return HttpNotFound();
            }

            // Check if the user is in the "Admin" role or the enrollment belongs to the user
            if (!User.IsInRole("Admin") && enrollment.UserId != User.Identity.GetUserId())
            {
                // User is not authorized to edit this enrollment
                return RedirectToAction("Index");
            }

            // Continue with the edit action
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", enrollment.ClassId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", enrollment.UserId);
            ViewBag.TrainingSessionId = new SelectList(db.TrainingSessions, "Id", "Time", enrollment.TrainingSessionId);

            return View(enrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ClassId,TrainingSessionId,EnrollmentDate")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                // Check if the user is in the "Admin" role or the enrollment belongs to the user
                if (!User.IsInRole("Admin") && enrollment.UserId != User.Identity.GetUserId())
                {
                    // User is not authorized to edit this enrollment
                    return RedirectToAction("Index");
                }

                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", enrollment.ClassId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", enrollment.UserId);
            ViewBag.TrainingSessionId = new SelectList(db.TrainingSessions, "Id", "Time", enrollment.TrainingSessionId);

            return View(enrollment);
        }

        public ActionResult Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page or another appropriate action
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Find the enrollment
            Enrollment enrollment = db.Enrollments.Find(id);

            if (enrollment == null)
            {
                return HttpNotFound();
            }

            // Check if the user is in the "Admin" role or the enrollment belongs to the user
            if (!User.IsInRole("Admin") && enrollment.UserId != User.Identity.GetUserId())
            {
                // User is not authorized to delete this enrollment
                return RedirectToAction("Index");
            }

            return View(enrollment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Find the enrollment
            Enrollment enrollment = db.Enrollments.Find(id);

            // Check if the user is in the "Admin" role or the enrollment belongs to the user
            if (!User.IsInRole("Admin") && enrollment.UserId != User.Identity.GetUserId())
            {
                // User is not authorized to delete this enrollment
                return RedirectToAction("Index");
            }

            db.Enrollments.Remove(enrollment);
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
