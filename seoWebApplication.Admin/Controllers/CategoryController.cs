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
    public class CategoryController : Controller
    {
        private SeoWebAppEntities db = new SeoWebAppEntities();

        // GET: /Category/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var categories = db.categories.Include(c => c.department);
            return View(categories.ToList());
        }

        // GET: /Webstore/
        [Authorize(Roles = "Admin")]
        public ActionResult Webstores()
        {
            var webstores = db.webstores.Include(w => w.zone1);
            return View(webstores.ToList());
        }

        // GET: /CategoryWebstore/
        [Authorize(Roles = "Admin")]
        public ActionResult CategoryWebstore(int id)
        {
            ViewBag.Id = id;
            var cat = (from de in db.categories where de.webstore_id == id select de).ToList();
            return View(cat);
        }

        // GET: /Category/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: /Category/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int id)
        {
            ViewBag.Id = id;
            var dept = (from d in db.departments where d.webstore_id == id select d).ToList();
            ViewBag.department_id = new SelectList(dept, "department_id", "Description");
            return View();
        }

        // POST: /Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include="category_id,department_id,webstore_id,name,description,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] category category)
        {
            if (ModelState.IsValid)
            {
                db.categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("CategoryWebstore/" + category.webstore_id);
            }

            ViewBag.department_id = new SelectList(db.departments, "department_id", "Description", category.department_id);
            return View(category);
        }

        // GET: /Category/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = category.webstore_id;
            ViewBag.department_id = new SelectList(db.departments, "department_id", "Description", category.department_id);
            return View(category);
        }

        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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

        public ActionResult ShowMenu(int id)
        {
            List<category> categorys = (from mitem in db.categories where mitem.department_id == id select mitem).ToList();
            return PartialView(categorys.ToList());
        }

        // GET: /Category/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            category category = db.categories.Find(id);
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
