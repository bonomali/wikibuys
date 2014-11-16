using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using seoWebApplication.Data;

namespace seoWebApplication.Admin.Controllers
{
    public class WebstoreCategoryController : Controller
    {
        private SeoWebAppEntities db = new SeoWebAppEntities();

        // GET: /WebstoreCategory/
        public ActionResult Index()
        {
          
            return View(db.WebstoreCuisines.ToList());
        }

        // GET: /WebstoreCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebstoreCuisine webstorecuisine = db.WebstoreCuisines.Find(id);
            if (webstorecuisine == null)
            {
                return HttpNotFound();
            }
            return View(webstorecuisine);
        }

        // GET: /WebstoreCategory/Create
        public ActionResult Create()
        {
            ViewBag.webstore_id = new SelectList(db.webstores, "webstore_id", "webstoreName");
            ViewBag.cuisine_id = new SelectList(db.cuisines, "cuisine_id", "name");
            return View();
        }

        // POST: /WebstoreCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="webstore_id,cuisine_id,Id")] WebstoreCuisine webstorecuisine)
        {
            if (ModelState.IsValid)
            {
                db.WebstoreCuisines.Add(webstorecuisine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.webstore_id = new SelectList(db.webstores, "webstore_id", "webstoreName", webstorecuisine.webstore_id);
            ViewBag.cuisine_id = new SelectList(db.cuisines, "cuisine_id", "name", webstorecuisine.cuisine_id);

            return View(webstorecuisine);
        }

        // GET: /WebstoreCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebstoreCuisine webstorecuisine = db.WebstoreCuisines.Find(id);
            if (webstorecuisine == null)
            {
                return HttpNotFound();
            } 
            ViewBag.webstore_id = new SelectList(db.webstores, "webstore_id", "webstoreName", webstorecuisine.webstore_id);
            ViewBag.cuisine_id = new SelectList(db.cuisines, "cuisine_id", "name", webstorecuisine.cuisine_id);
            return View(webstorecuisine);
        }

        // POST: /WebstoreCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="webstore_id,cuisine_id,Id")] WebstoreCuisine webstorecuisine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webstorecuisine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            } 
            ViewBag.webstore_id = new SelectList(db.webstores, "webstore_id", "webstoreName", webstorecuisine.webstore_id);
            ViewBag.cuisine_id = new SelectList(db.cuisines, "cuisine_id", "name", webstorecuisine.cuisine_id);
            return View(webstorecuisine);
        }

        // GET: /WebstoreCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebstoreCuisine webstorecuisine = db.WebstoreCuisines.Find(id);
            if (webstorecuisine == null)
            {
                return HttpNotFound();
            }
            return View(webstorecuisine);
        }

        // POST: /WebstoreCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WebstoreCuisine webstorecuisine = db.WebstoreCuisines.Find(id);
            db.WebstoreCuisines.Remove(webstorecuisine);
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
