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
using seoWebApplication.st.SharkTankDAL;
using Kendo.Mvc.UI; 
using Kendo.Mvc.Extensions;
using seoWebApplication.Models;

namespace seoWebApplication.Controllers
{
    public class DepartmentsController : Controller
    {
        private SeoWebAppEntities db = new SeoWebAppEntities();
        private DepartmentService _departmentService = new DepartmentService();
        // GET: /Departments/
        public ActionResult Index()
        { 
            return View();
        }

        public ActionResult Departments_Read([DataSourceRequest]DataSourceRequest request)
        { 
            var depts = (from e in _departmentService.GetDepartments()
                            select e).ToList();

            DataSourceResult result = depts.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult Menu(string Id) { 
            return PartialView(_departmentService.GetDepartments());
        }

        // GET: /Departments/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = _departmentService.GetDepartmentsById(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: /Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "department_id,webstore_id,Description,Name,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] Departments department)
        {
            if (ModelState.IsValid)
            {
                _departmentService.Create(department); 
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: /Departments/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = _departmentService.GetDepartmentsById(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: /Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "department_id,webstore_id,Description,Name,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] Departments department)
        {
            if (ModelState.IsValid)
            {
                _departmentService.Update(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: /Departments/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = _departmentService.GetDepartmentsById(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: /Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _departmentService.Delete(id); 
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
