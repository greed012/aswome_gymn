using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using awsome_gymn.Models;

namespace awsome_gymn.Controllers
{
    [AdminAuthorize]
    public class membershipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: memberships
        public ActionResult Index()
        {
            return View(db.memberships.ToList());
        }

        // GET: memberships/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            membership membership = db.memberships.Find(id);
            if (membership == null)
            {
                return HttpNotFound();
            }
            return View(membership);
        }

        // GET: memberships/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: memberships/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Image,Price,DurationMonths")] membership membership, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    using (var binaryReader = new BinaryReader(imageFile.InputStream))
                    {
                        membership.Image = binaryReader.ReadBytes(imageFile.ContentLength);
                    }
                }

                db.memberships.Add(membership);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(membership);
        }

        // GET: memberships/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            membership membership = db.memberships.Find(id);
            if (membership == null)
            {
                return HttpNotFound();
            }
            return View(membership);
        }

        // POST: memberships/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Image,Price,DurationMonths")] membership membership, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    using (var binaryReader = new BinaryReader(imageFile.InputStream))
                    {
                        membership.Image = binaryReader.ReadBytes(imageFile.ContentLength);
                    }
                }

                db.Entry(membership).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(membership);
        }

        // GET: memberships/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            membership membership = db.memberships.Find(id);
            if (membership == null)
            {
                return HttpNotFound();
            }
            return View(membership);
        }

        // POST: memberships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            membership membership = db.memberships.Find(id);
            db.memberships.Remove(membership);
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
