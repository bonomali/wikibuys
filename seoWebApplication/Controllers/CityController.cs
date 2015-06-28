using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using seoWebApplication.Service;
using seoWebApplication.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Web.UI.MobileControls;
using System.Collections;

namespace seoWebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CityController : Controller
    {
        private CityService _CityService = new CityService();
        private ProductService _productService = new ProductService();

        // GET: /Store/
        public ActionResult Index()
        {
            List<State> _state = new List<State>(); 
            _state = new StateService().GetStates().ToList();
            _state.Insert(0, new State { Name = "Choose..." });
            ViewData["states"] = _state;
            return View();
        }

       

        // GET: /Store/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seoWebApplication.Models.City store = _CityService.Getcity(id);
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
            return Json(_CityService.Getcitys().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, City city)
        {
            if (city != null && ModelState.IsValid)
            {
                _CityService.Create(city);
            }

            return Json(new[] { city }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, City city)
        {
            if (city != null && ModelState.IsValid)
            {
                _CityService.Update(city);
            }

            return Json(new[] { city }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, City city)
        {
            if (city != null)
            {
                _CityService.Delete(city.Id);
            }

            return Json(new[] { city }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Cities_Read([DataSourceRequest]DataSourceRequest request)
        { 
            var stores = (from e in _CityService.Getcitys()
                            select e).ToList();

            DataSourceResult result = stores.ToDataSourceResult(request);
            return Json(result);
        }  

        // POST: /Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(City City)
        {
            if (ModelState.IsValid)
            {
                _CityService.Create(City);
                return RedirectToAction("Index");
            }

            return View(City);
        }

        // GET: /Store/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seoWebApplication.Models.City city = _CityService.Getcity(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: /Store/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(City city)
        {
            if (ModelState.IsValid)
            {
                _CityService.Update(city);
                return RedirectToAction("Index");
            }
            return View(city);
        }

        // GET: /Store/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = _CityService.Getcity(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: /Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _CityService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
