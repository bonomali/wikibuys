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
    public class SubcategoryController : Controller
    {
        private SubcategoriesService _categoriesService = new SubcategoriesService();
        private CategoriesService ds = new CategoriesService(); 
        // GET: /Category/
        public ActionResult Index()
        { 
            return View();
        }
         
        public ActionResult Categories_Read([DataSourceRequest]DataSourceRequest request, Guid id)
        {
            var cats = (from e in _categoriesService.GetSubcategories()
                        where e.category_id == id
                        select e).ToList();

            DataSourceResult result = cats.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult Category_Read([DataSourceRequest]DataSourceRequest request)
        {
            var cats = (from e in _categoriesService.GetSubcategories()
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
            var category = _categoriesService.GetSubcategoriesById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: /Category/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(ds.GetCategories(), "category_id", "Description");
            return View();
        }

        // POST: /Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "category_id,department_id,webstore_id,name,description,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] seoWebApplication.Models.Subcategories category)
        {
            if (ModelState.IsValid)
            {
                _categoriesService.Create(category);
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(ds.GetCategories(), "category_id", "Description", category.category_id);
            return View(category);
        }

        // GET: /Category/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _categoriesService.GetCategoryByGuid(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(ds.GetCategories(), "category_id", "Description", category.category_id);
            return View(category);
        }

        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "category_id,department_id,webstore_id,name,description,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] seoWebApplication.Models.Subcategories category)
        {
            if (ModelState.IsValid)
            {
                _categoriesService.Update(category);
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(ds.GetCategories(), "category_id", "Description", category.category_id);
            return View(category);
        }

        // GET: /Category/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _categoriesService.GetCategoryByGuid(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _categoriesService.Delete(id);
            return RedirectToAction("Index");
        }
         
    }
}
