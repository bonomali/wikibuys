using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using seoWebApplication.Service;
using seoWebApplication.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace seoWebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StoreController : Controller
    {
        private StoreService _StoreService = new StoreService();

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
            seoWebApplication.Models.mStores store = _StoreService.Getstore(id);
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
            return Json(_StoreService.Getstores().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, mStores store)
        {
            if (store != null && ModelState.IsValid)
            {
                _StoreService.Create(store);
            }

            return Json(new[] { store }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, mStores store)
        {
            if (store != null && ModelState.IsValid)
            {
                _StoreService.Update(store);
            }

            return Json(new[] { store }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, mStores store)
        {
            if (store != null)
            {
                _StoreService.Delete(store.Id);
            }

            return Json(new[] { store }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Stores_Read([DataSourceRequest]DataSourceRequest request)
        { 
            var stores = (from e in _StoreService.Getstores()
                            select e).ToList();

            DataSourceResult result = stores.ToDataSourceResult(request);
            return Json(result);
        }  

        // POST: /Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(mStores Store)
        {
            if (ModelState.IsValid)
            {
                _StoreService.Create(Store);
                return RedirectToAction("Index");
            }

            return View(Store);
        }

        // GET: /Store/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seoWebApplication.Models.mStores store = _StoreService.Getstore(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: /Store/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(mStores Store)
        {
            if (ModelState.IsValid)
            {
                _StoreService.Update(Store);
                return RedirectToAction("Index");
            }
            return View(Store);
        }

        // GET: /Store/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mStores store = _StoreService.Getstore(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: /Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _StoreService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
