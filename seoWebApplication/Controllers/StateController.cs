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
    public class StateController : Controller
    {
        private StateService _StateService = new StateService();
        private ProductService _productService = new ProductService();

        // GET: /Store/
        public ActionResult Index()
        {
            List<Region> _Region = new List<Region>();
            _Region = new RegionService().GetRegions().ToList();
            _Region.Insert(0, new Region { Name = "Choose..." });

            ViewData["regions"] = _Region;
            return View();
        }

       

        // GET: /Store/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seoWebApplication.Models.State store = _StateService.GetState(id);
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
            return Json(_StateService.GetStates().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, State state)
        {
            if (state != null && ModelState.IsValid)
            {
                _StateService.Create(state);
            }

            return Json(new[] { state }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, State state)
        {
            if (state != null && ModelState.IsValid)
            {
                _StateService.Update(state);
            }

            return Json(new[] { state }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, State state)
        {
            if (state != null)
            {
                _StateService.Delete(state.Id);
            }

            return Json(new[] { state }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult States_Read([DataSourceRequest]DataSourceRequest request)
        {
            var stores = (from e in _StateService.GetStates()
                            select e).ToList();

            DataSourceResult result = stores.ToDataSourceResult(request);
            return Json(result);
        }  

        // POST: /Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(State state)
        {
            if (ModelState.IsValid)
            {
                _StateService.Create(state);
                return RedirectToAction("Index");
            }

            return View(state);
        }

        // GET: /Store/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seoWebApplication.Models.State state = _StateService.GetState(id);
            if (state == null)
            {
                return HttpNotFound();
            }
            return View(state);
        }

        // POST: /Store/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(State state)
        {
            if (ModelState.IsValid)
            {
                _StateService.Update(state);
                return RedirectToAction("Index");
            }
            return View(state);
        }

        // GET: /Store/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            State state = _StateService.GetState(id);
            if (state == null)
            {
                return HttpNotFound();
            }
            return View(state);
        }

        // POST: /Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _StateService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
