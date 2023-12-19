using awsome_gymn.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace awsome_gymn.Models
{
    [AdminAuthorize]
    public class ClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Classes
        [AdminAuthorize]        
        public ActionResult Index()
        {
            return View(db.Classes.ToList());
        }

        // GET: Classes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // GET: Classes/Create
        [AdminAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        [AdminAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Image")] Class @class, HttpPostedFileBase ImageFile)
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

                        @class.Image = fileData;
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = "Error uploading file: " + ex.Message;
                        return View(@class);
                    }
                }

                db.Classes.Add(@class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@class);
        }

        // GET: Classes/Edit/5
        [AdminAuthorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: Classes/Edit/5
        [AdminAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Class @class, HttpPostedFileBase ImageFile)
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
                        @class.Image = fileData; // Update the image data with the new image
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = "Error uploading file: " + ex.Message;
                        return View(@class);
                    }
                }

                db.Entry(@class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@class);
        }

        // GET: Classes/Delete/5
        [AdminAuthorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: Classes/Delete/5
        [AdminAuthorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class @class = db.Classes.Find(id);
            db.Classes.Remove(@class);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
public ActionResult AccessDenied()
{
  return View(); 
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
