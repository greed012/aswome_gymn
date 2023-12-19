using System;
using System.Collections.Generic;
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
    public class TrainersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trainers
        [AdminAuthorize]
        public ActionResult Index()
        {
            return View(db.Trainers.ToList());
        }

        // GET: Trainers/Details/5
        [AdminAuthorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // GET: Trainers/Create
        [AdminAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trainers/Create
        [HttpPost]
        [AdminAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Image,FbLink,InstaLink,TwitterLink")] Trainer trainer, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    try
                    {
                        byte[] fileData = null;
                        using (var binaryReader = new BinaryReader(ImageFile.InputStream))
                        {
                            fileData = binaryReader.ReadBytes(ImageFile.ContentLength);
                        }

                        trainer.Image = fileData; // Set the image data
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = "Error uploading file: " + ex.Message;
                        return View(trainer);
                    }
                }

                db.Trainers.Add(trainer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trainer);
        }

        // GET: Trainers/Edit/5
        [AdminAuthorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainers/Edit/5
        [HttpPost]
        [AdminAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Image,FbLink,InstaLink,TwitterLink")] Trainer trainer, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                // Check if a new image has been uploaded
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    try
                    {
                        byte[] fileData = null;
                        using (var binaryReader = new BinaryReader(ImageFile.InputStream))
                        {
                            fileData = binaryReader.ReadBytes(ImageFile.ContentLength);
                        }

                        trainer.Image = fileData; // Update the image data with the new image
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = "Error uploading file: " + ex.Message;
                        return View(trainer);
                    }
                }

                db.Entry(trainer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainer);
        }

        // GET: Trainers/Delete/5
        [AdminAuthorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainers/Delete/5
        [AdminAuthorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainer trainer = db.Trainers.Find(id);
            db.Trainers.Remove(trainer);
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
