using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using seoWebApplication.Service;
using seoWebApplication.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;

namespace seoWebApplication.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RegionController : Controller
    {
        private RegionService _RegionService = new RegionService();
        private ProductService _productService = new ProductService();

        // GET: /Store/
        public ActionResult Index()
        {
            return View();
        }

       

        // GET: /Store/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seoWebApplication.Models.Region store = _RegionService.GetRegion(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // GET: /Store/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult EditingPopup_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_RegionService.GetRegions().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, Region region)
        {
            if (region != null && ModelState.IsValid)
            {
                _RegionService.Create(region);
            }

            return Json(new[] { region }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, Region region)
        {
            if (region != null && ModelState.IsValid)
            {
                _RegionService.Update(region);
            }

            return Json(new[] { region }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, Region region)
        {
            if (region != null)
            {
                _RegionService.Delete(region.Id);
            }

            return Json(new[] { region }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Region_Read([DataSourceRequest]DataSourceRequest request)
        { 
            var regions = (from e in _RegionService.GetRegions()
                            select e).ToList();

            DataSourceResult result = regions.ToDataSourceResult(request);
            return Json(result);
        }  

        // POST: /Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Region region)
        {
            if (ModelState.IsValid)
            {
                _RegionService.Create(region);
                return RedirectToAction("Index");
            }

            return View(region);
        }

        // GET: /Store/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seoWebApplication.Models.Region region = _RegionService.GetRegion(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: /Store/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Region region)
        {
            if (ModelState.IsValid)
            {
                _RegionService.Update(region);
                return RedirectToAction("Index");
            }
            return View(region);
        }

        // GET: /Store/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = _RegionService.GetRegion(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: /Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _RegionService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
