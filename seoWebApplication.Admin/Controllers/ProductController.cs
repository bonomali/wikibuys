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
    public class ProductController : Controller
    {
        private SeoWebAppEntities db = new SeoWebAppEntities();

        // GET: /Product/
        public ActionResult Index()
        {
            return View(db.products.ToList());
        }
         
         public ActionResult Categories(int dept)
        { 
            var products = (from cat in db.categories where cat.department_id == dept select cat).ToList();
            return View(products);
        }

         public ActionResult Departments(int id)
         {
             ViewBag.Id = id;
             var prods = (from prod in db.products
                          join pc in db.productcategories on prod.product_id equals pc.product_id
                          join cat in db.categories on pc.category_id equals cat.category_id
                          join depts in db.departments on cat.department_id equals depts.department_id
                          where depts.department_id == id
                          select prod).ToList();
             return View(prods);
         }
          
        // GET: /Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="product_id,webstore_id,name,description,price,thumbnail,image,promofront,promodept,defaultAttribute,defaultAttCat,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Display(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = product.webstore_id;
            return View(product);
        }

        // GET: /Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="product_id,webstore_id,name,description,price,thumbnail,image,promofront,promodept,defaultAttribute,defaultAttCat,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
        
        public ActionResult Product_Read([DataSourceRequest]DataSourceRequest request)
        {


            var products = db.productSelectAll().ToList(); 
            DataSourceResult result = products.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);

             

        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Product_Destroy([DataSourceRequest] DataSourceRequest request, product Products)
        {
            if (Products != null)
            {
                product product = db.products.Find(Products.product_id);
                db.products.Remove(product);
                db.SaveChanges();

            }


            //Return any validation errors if any
            return Json(ModelState.ToDataSourceResult());


        }
    }
}
