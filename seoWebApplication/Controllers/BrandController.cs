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
    public class BrandController : Controller
    {
        private BrandsService _brandsService = new BrandsService(); 
        // GET: /Category/
        public ActionResult Index()
        { 
            return View();
        }
         
        public ActionResult Categories_Read([DataSourceRequest]DataSourceRequest request, int id)
        {
            var cats = (from e in _brandsService.GetBrands()
                        where e.brand_id == id
                        select e).ToList();

            DataSourceResult result = cats.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult Brand_Read([DataSourceRequest]DataSourceRequest request)
        {
            var cats = (from e in _brandsService.GetBrands()
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
            var category = _brandsService.GetBrandById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: /Category/Create
        public ActionResult Create()
        { 
            return View();
        }

        // POST: /Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "category_id,department_id,webstore_id,name,description,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] seoWebApplication.Models.Brands brand)
        {
            if (ModelState.IsValid)
            {
                _brandsService.Create(brand);
                return RedirectToAction("Index");
            }

            return View(brand);
        }

        // GET: /Category/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brand = _brandsService.GetBrandByGuid(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "category_id,department_id,webstore_id,name,description,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] seoWebApplication.Models.Brands brand)
        {
            if (ModelState.IsValid)
            {
                _brandsService.Update(brand);
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        // GET: /Category/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brand = _brandsService.GetBrandByGuid(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _brandsService.Delete(id);
            return RedirectToAction("Index");
        }
         
    }
}
