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
    public class DepartmentController : Controller
    {
        private SeoWebAppEntities db = new SeoWebAppEntities();

        // GET: /Department/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.departments.ToList());
        }

        // GET: /Webstore/
        [Authorize(Roles = "Admin")]
        public ActionResult Webstores()
        {
            var webstores = db.webstores.Include(w => w.zone1);
            return View(webstores.ToList());
        }

        // GET: /DepartmentWebstore/
        [Authorize(Roles = "Admin")]
        public ActionResult DepartmentWebstore(int id)
        {
            ViewBag.Id = id;
            var dept = (from de in db.departments where de.webstore_id == id select de).ToList();
            return View(dept);
        }

        public JsonResult GetDepartments(int id)
        {
            List<department> departments = (from de in db.departments where de.webstore_id == id select de).ToList();

            return Json(
    departments.Select(x => new {
        department_id = x.department_id,
        name = x.Name
    }), JsonRequestBehavior.AllowGet); 

            //return Json(departments, JsonRequestBehavior.AllowGet); 
        }
     

        // GET: /Department/ShowMenu/5
        public ActionResult ShowMenu(int id)
        {
            List<department> departments = (from mitem in db.departments where mitem.webstore_id == id select mitem).ToList();
            return PartialView(departments.ToList());
        }

        // GET: /Department/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            department department = db.departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: /Department/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        // POST: /Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include="department_id,webstore_id,Description,Name,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] department department)
        {
            if (ModelState.IsValid)
            {
                db.departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("DepartmentWebstore/" + department.webstore_id);
            }

            return View(department);
        }

        // GET: /Department/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            department department = db.departments.Find(id);
            ViewBag.Id = department.webstore_id;
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: /Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include="department_id,webstore_id,Description,Name,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: /Department/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            department department = db.departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: /Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            department department = db.departments.Find(id);
            db.departments.Remove(department);
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
