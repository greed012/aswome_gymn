using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using awsome_gymn.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace awsome_gymn.Controllers
{
    public class membership_enrollmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager;

        public membership_enrollmentController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: membership_enrollment
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page or another appropriate action
                return RedirectToAction("Login", "Account");
            }

            var userId = User.Identity.GetUserId();

            if (userManager.IsInRole(userId, "Admin"))
            {
                // Admins can view all enrollments
                var enrollments = db.membership_enrollment.Include(e => e.Membership).Include(e => e.User);
                return View(enrollments.ToList());
            }
            else
            {
                // Non-admin users can only view their own enrollments
                var enrollments = db.membership_enrollment
                    .Where(e => e.UserId == userId)
                    .Include(e => e.Membership)
                    .Include(e => e.User);

                return View(enrollments.ToList());
            }
        }

        // GET: membership_enrollment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            membership_enrollment membership_enrollment = db.membership_enrollment.Find(id);
            if (membership_enrollment == null)
            {
                return HttpNotFound();
            }
            return View(membership_enrollment);
        }

        // GET: membership_enrollment/Create
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

            if (userManager.IsInRole(userId, "Admin"))
            {
                // Admins can select any user when creating a membership enrollment
                ViewBag.Users = new SelectList(db.Users, "Id", "Email");
            }
            else
            {
                // Non-admin users can only enroll themselves
                ViewBag.Users = new SelectList(db.Users.Where(u => u.Id == userId), "Id", "Email");
            }

            // Create a new membership_enrollment with pre-filled values
            var membership_enrollment = new membership_enrollment
            {
                UserId = userId,
                EnrollmentDate = DateTime.Now
            };

            // Provide a list of available memberships
            ViewBag.Memberships = new SelectList(db.memberships, "Id", "Name");

            return View(membership_enrollment);
        }

        // POST: membership_enrollment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,MembershipId,EnrollmentDate")] membership_enrollment membership_enrollment)
        {
            if (ModelState.IsValid)
            {
                db.membership_enrollment.Add(membership_enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // If the model is not valid, repopulate the Users and Memberships dropdowns
            if (userManager.IsInRole(User.Identity.GetUserId(), "Admin"))
            {
                ViewBag.Users = new SelectList(db.Users, "Id", "Email");
            }
            else
            {
                ViewBag.Users = new SelectList(db.Users.Where(u => u.Id == membership_enrollment.UserId), "Id", "Email");
            }

            ViewBag.Memberships = new SelectList(db.memberships, "Id", "Name");

            return View(membership_enrollment);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            membership_enrollment membership_enrollment = db.membership_enrollment.Find(id);

            if (membership_enrollment == null)
            {
                return HttpNotFound();
            }

            if (!userManager.IsInRole(User.Identity.GetUserId(), "Admin") && membership_enrollment.UserId != User.Identity.GetUserId())
            {
                // User is not authorized to edit this enrollment
                return RedirectToAction("Index");
            }

            // Automatically set the Enrollment Date
            membership_enrollment.EnrollmentDate = DateTime.Now;

            // Restrict the Users dropdown to the current user
            ViewBag.Users = new SelectList(db.Users.Where(u => u.Id == membership_enrollment.UserId), "Id", "Email");

            ViewBag.Memberships = new SelectList(db.memberships, "Id", "Name", membership_enrollment.MembershipId);

            return View(membership_enrollment);
        }

        // GET: membership_enrollment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            membership_enrollment membership_enrollment = db.membership_enrollment.Find(id);
            if (membership_enrollment == null)
            {
                return HttpNotFound();
            }
            return View(membership_enrollment);
        }

        // POST: membership_enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            membership_enrollment membership_enrollment = db.membership_enrollment.Find(id);

            if (!userManager.IsInRole(User.Identity.GetUserId(), "Admin") && membership_enrollment.UserId != User.Identity.GetUserId())
            {
                // User is not authorized to delete this enrollment
                return RedirectToAction("Index");
            }

            db.membership_enrollment.Remove(membership_enrollment);
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
