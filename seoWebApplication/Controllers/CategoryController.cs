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
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace seoWebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    { 
        private CategoriesService _categoriesService = new CategoriesService();
        private DepartmentService ds = new DepartmentService();
        // GET: /Category/
        public ActionResult Index()
        { 
            return View();
        }

        public ActionResult Categories_Read([DataSourceRequest]DataSourceRequest request, int id)
        {
            var cats = (from e in _categoriesService.GetCategories()
                        where e.department_id == id
                        select e).ToList();

            DataSourceResult result = cats.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult Category_Read([DataSourceRequest]DataSourceRequest request)
        {
            var cats = (from e in _categoriesService.GetCategories()
                         select e).ToList();

            DataSourceResult result = cats.ToDataSourceResult(request);
            return Json(result);
        }

        // GET: /Category/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _categoriesService.GetCategoryById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: /Category/Create
        public ActionResult Create()
        {
            ViewBag.department_id = new SelectList(ds.GetDepartments(), "department_id", "Description");
            return View();
        }

        // POST: /Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "category_id,department_id,webstore_id,name,description,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] seoWebApplication.Models.Categories category)
        {
            if (ModelState.IsValid)
            {
                _categoriesService.Create(category);
                return RedirectToAction("Index");
            }

            ViewBag.department_id = new SelectList(ds.GetDepartments(), "department_id", "Description", category.department_id);
            return View(category);
        }

        // GET: /Category/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _categoriesService.GetCategoryById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.department_id = new SelectList(ds.GetDepartments(), "department_id", "Description", category.department_id);
            return View(category);
        }

        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "category_id,department_id,webstore_id,name,description,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] seoWebApplication.Models.Categories category)
        {
            if (ModelState.IsValid)
            {
                _categoriesService.Update(category);
                return RedirectToAction("Index");
            }
            ViewBag.department_id = new SelectList(ds.GetDepartments(), "department_id", "Description", category.department_id);
            return View(category);
        }

        // GET: /Category/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _categoriesService.GetCategoryById(id);
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
            _categoriesService.Delete(id);
            return RedirectToAction("Index");
        }
         
    }
}
