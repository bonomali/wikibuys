using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using seoWebApplication.Data;
using seoWebApplication.Service;

namespace seoWebApplication.Controllers
{
    public class CategoryController : Controller
    {
        private SeoWebAppEntities db = new SeoWebAppEntities();
        private CategoriesService _categoriesService = new CategoriesService();
        // GET: /Category/
        public ActionResult Index()
        {
            foreach (seoWebApplication.Data.category cat in db.categories)
            {
                Models.Categories cats = new Models.Categories();
                cats.category_id = cat.category_id;
                cats.department_id = cat.department_id;
                cats.Description = cat.description;
                cats.Name = cat.name; 
                _categoriesService.Create(cats);
            }
            var categories = db.categories.Include(c => c.department);
            return View(categories.ToList());
        }

        // GET: /Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seoWebApplication.Data.category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: /Category/Create
        public ActionResult Create()
        {
            ViewBag.department_id = new SelectList(db.departments, "department_id", "Description");
            return View();
        }

        // POST: /Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "category_id,department_id,webstore_id,name,description,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] seoWebApplication.Data.category category)
        {
            if (ModelState.IsValid)
            {
                db.categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.department_id = new SelectList(db.departments, "department_id", "Description", category.department_id);
            return View(category);
        }

        // GET: /Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seoWebApplication.Data.category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.department_id = new SelectList(db.departments, "department_id", "Description", category.department_id);
            return View(category);
        }

        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="category_id,department_id,webstore_id,name,description,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.department_id = new SelectList(db.departments, "department_id", "Description", category.department_id);
            return View(category);
        }

        // GET: /Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seoWebApplication.Data.category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            seoWebApplication.Data.category category = db.categories.Find(id);
            db.categories.Remove(category);
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
