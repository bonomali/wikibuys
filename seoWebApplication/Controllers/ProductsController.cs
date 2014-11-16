using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using seoWebApplication.Data;
using seoWebApplication.DocumentDbServices;

namespace seoWebApplication.Controllers
{
    public class ProductsController : Controller
    {
        private SeoWebAppEntities db = new SeoWebAppEntities();

        // GET: /Products/
        public ActionResult Index()
        {
            List<seoWebApplication.Data.product> list = new List<seoWebApplication.Data.product>();
            if (seoWebAppConfiguration.UseDocumentDb)
            {
               list = ProductsService.GetProducts();
            }
            else if (seoWebAppConfiguration.UseSqlServerDb)
            {
                list = db.products.ToList();
            }
            else if (seoWebAppConfiguration.UseRavenDb)
            {
                list = db.products.ToList();
            }
            return View(list);
        }
        

        // GET: /Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seoWebApplication.Data.product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,webstore_id,name,description,price,thumbnail,image,promofront,promodept,defaultAttribute,defaultAttCat,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] seoWebApplication.Data.product product)
        {
            if (ModelState.IsValid)
            {
                if (seoWebAppConfiguration.UseDocumentDb) {
                      ProductsService.CreateItemAsync(product);
                }
                else if (seoWebAppConfiguration.UseSqlServerDb) { 
                db.products.Add(product);
                db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seoWebApplication.Data.product product = new seoWebApplication.Data.product();
            if (seoWebAppConfiguration.UseDocumentDb)
            {
                product = ProductsService.GetProduct(product.product_id.ToString());
            }
            else if (seoWebAppConfiguration.UseSqlServerDb)
            {
                product = db.products.Find(id);
           
            }
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,webstore_id,name,description,price,thumbnail,image,promofront,promodept,defaultAttribute,defaultAttCat,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] seoWebApplication.Data.product product)
        {
            if (ModelState.IsValid)
            {
                if (seoWebAppConfiguration.UseDocumentDb)
                {
                    ProductsService.UpdateProductAsync(product);
                }
                else if (seoWebAppConfiguration.UseSqlServerDb)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
               
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seoWebApplication.Data.product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            seoWebApplication.Data.product product = db.products.Find(id);
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
    }
}
