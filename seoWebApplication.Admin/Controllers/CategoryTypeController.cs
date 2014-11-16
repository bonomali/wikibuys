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
    public class CategoryTypeController : Controller
    {
        private SeoWebAppEntities db = new SeoWebAppEntities();

        // GET: /CategoryType/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.cuisines.ToList());
        }

        // GET: /CategoryType/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cuisine cuisine = db.cuisines.Find(id);
            if (cuisine == null)
            {
                return HttpNotFound();
            }
            return View(cuisine);
        }


        // GET: /CategoryType/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult WebstoreCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<WebstoreCuisine> type = (from a in db.WebstoreCuisines where a.cuisine_id == id select a).ToList();
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // GET: /CategoryType/Details/5
        public ActionResult Types(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cuisine cuisine = db.cuisines.Find(id);

            ViewBag.Name = cuisine.name;
            ViewBag.Title = cuisine.SeoTitle;
            ViewBag.Description = cuisine.SeoDescription;
            ViewBag.Keywords = cuisine.SeoKeywords;

            //IEnumerable<webstore> stores = (from store in db.webstoreSelectByCId(id));
             IEnumerable<webstore> stores = (from store in db.webstores join wc in db.WebstoreCuisines
            on store.webstore_id equals wc.webstore_id where wc.cuisine_id == id
            select store).ToList(); 

            if (cuisine == null)
            {
                return HttpNotFound();
            }
            return View(stores);
        }

        // GET: /CategoryType/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CategoryType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include="cuisine_id,name,SeoDescription,SeoTitle,SeoKeywords,webstore_id")] cuisine cuisine)
        {
            if (ModelState.IsValid)
            {
                db.cuisines.Add(cuisine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cuisine);
        }

        // GET: /CategoryType/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cuisine cuisine = db.cuisines.Find(id);
            if (cuisine == null)
            {
                return HttpNotFound();
            }
            return View(cuisine);
        }

        // POST: /CategoryType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include="cuisine_id,name,SeoDescription,SeoTitle,SeoKeywords,webstore_id")] cuisine cuisine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuisine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cuisine);
        }

        // GET: /CategoryType/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cuisine cuisine = db.cuisines.Find(id);
            if (cuisine == null)
            {
                return HttpNotFound();
            }
            return View(cuisine);
        }

        // POST: /CategoryType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            cuisine cuisine = db.cuisines.Find(id);
            db.cuisines.Remove(cuisine);
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
