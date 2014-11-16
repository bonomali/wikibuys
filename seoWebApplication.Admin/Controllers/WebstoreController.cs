using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using seoWebApplication.Data;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace seoWebApplication.Admin.Controllers
{
    public class WebstoreController : Controller
    {
        private SeoWebAppEntities db = new SeoWebAppEntities();

        // GET: /Webstore/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var webstores = db.webstores.Include(w => w.zone1);
            return View(webstores.ToList());
        }

        // GET: /Webstore/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            webstore webstore = db.webstores.Find(id);
            if (webstore == null)
            {
                return HttpNotFound();
            }
            return View(webstore);
        }

        // GET: /Webstore/Store/5 
        public ActionResult Store(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            webstore webstore = db.webstores.Find(id);
            ViewBag.Name = webstore.webstoreName;

            IEnumerable<product> product = (from prd in db.products where prd.webstore_id == id select prd).ToList();
            if (webstore == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Webstore/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.zone = new SelectList(db.zones, "idZone", "zoneName");
            return View();
        }

        // POST: /Webstore/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include="webstore_id,webstoreName,webstoreUrl,locationx,locationy,polygonArray,address,city,state,country,zip,zone,parentWebstoreId,email,ownerFName,ownerName,ownerNumber,managerFname,managerLname,managerNumber,storeNumber,apiKey,monday,monBreakfast,monLunch,monDinner,monBreakfastStart,monBreakfastEnd,monLunchStart,monLunchEnd,monDinnerStart,monDinnerEnd,tuesday,tuesBreakfast,tuesLunch,tuesDinner,tueBreakfastStart,tueBreakfastEnd,tueLunchStart,tueLunchEnd,tueDinnerStart,tueDinnerEnd,wednesday,wedBreakfast,wedLunch,wedDinner,wedBreakfastStart,wedBreakfastEnd,wedLunchStart,wedLunchEnd,wedDinnerStart,wedDinnerEnd,thursday,thrBreakfast,thrLunch,thrDinner,thrBreakfastStart,thrBreakfastEnd,thrLunchStart,thrLunchEnd,thrDinnerStart,thrDinnerEnd,friday,friBreakfast,friLunch,friDinner,friBreakfastStart,friBreakfastEnd,friLunchStart,friLunchEnd,friDinnerStart,friDinnerEnd,saturday,satBreakfast,satLunch,satDinner,satBreakfastStart,satBreakfastEnd,satLunchStart,satLunchEnd,satDinnerStart,satDinnerEnd,sunday,sunBreakfast,sunLunch,sunDinner,sunBreakfastStart,sunBreakfastEnd,sunLunchStart,sunLunchEnd,sunDinnerStart,sunDinnerEnd,thumbnail,image,seoDescription,seoTitle,seoKeywords,seoMeta")] webstore webstore)
        {
            if (ModelState.IsValid)
            {
                db.webstores.Add(webstore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.zone = new SelectList(db.zones, "idZone", "zoneName", webstore.zone);
            return View(webstore);
        }

        // GET: /Webstore/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            webstore webstore = db.webstores.Find(id);
            if (webstore == null)
            {
                return HttpNotFound();
            }
            ViewBag.zone = new SelectList(db.zones, "idZone", "zoneName", webstore.zone);
            return View(webstore);
        }

        // POST: /Webstore/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include="webstore_id,webstoreName,webstoreUrl,locationx,locationy,polygonArray,address,city,state,country,zip,zone,parentWebstoreId,email,ownerFName,ownerName,ownerNumber,managerFname,managerLname,managerNumber,storeNumber,apiKey,monday,monBreakfast,monLunch,monDinner,monBreakfastStart,monBreakfastEnd,monLunchStart,monLunchEnd,monDinnerStart,monDinnerEnd,tuesday,tuesBreakfast,tuesLunch,tuesDinner,tueBreakfastStart,tueBreakfastEnd,tueLunchStart,tueLunchEnd,tueDinnerStart,tueDinnerEnd,wednesday,wedBreakfast,wedLunch,wedDinner,wedBreakfastStart,wedBreakfastEnd,wedLunchStart,wedLunchEnd,wedDinnerStart,wedDinnerEnd,thursday,thrBreakfast,thrLunch,thrDinner,thrBreakfastStart,thrBreakfastEnd,thrLunchStart,thrLunchEnd,thrDinnerStart,thrDinnerEnd,friday,friBreakfast,friLunch,friDinner,friBreakfastStart,friBreakfastEnd,friLunchStart,friLunchEnd,friDinnerStart,friDinnerEnd,saturday,satBreakfast,satLunch,satDinner,satBreakfastStart,satBreakfastEnd,satLunchStart,satLunchEnd,satDinnerStart,satDinnerEnd,sunday,sunBreakfast,sunLunch,sunDinner,sunBreakfastStart,sunBreakfastEnd,sunLunchStart,sunLunchEnd,sunDinnerStart,sunDinnerEnd,thumbnail,image,seoDescription,seoTitle,seoKeywords,seoMeta")] webstore webstore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webstore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.zone = new SelectList(db.zones, "idZone", "zoneName", webstore.zone);
            return View(webstore);
        }

        // GET: /Webstore/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            webstore webstore = db.webstores.Find(id);
            if (webstore == null)
            {
                return HttpNotFound();
            }
            return View(webstore);
        }

        // POST: /Webstore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            webstore webstore = db.webstores.Find(id);
            db.webstores.Remove(webstore);
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

        public ActionResult Webstore_Read([DataSourceRequest]DataSourceRequest request)
        {


            var webstores = db.webstoreSelectAll(1).ToList();
             

            DataSourceResult result = webstores.ToDataSourceResult(request);
             
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Webstore_Destroy([DataSourceRequest] DataSourceRequest request, webstore webstore)
        {
            if (webstore != null)
            {
                webstore webstores = db.webstores.Find(webstore.webstore_id); 
                db.webstores.Remove(webstores);
                db.SaveChanges();

            }


            //Return any validation errors if any
            return Json(ModelState.ToDataSourceResult());


        }
    }
}
