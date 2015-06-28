using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using seoWebApplication.Data; 
using seoWebApplication.Models;
using seoWebApplication.Service;

namespace seoWebApplication.Controllers
{
    public class ProductsController : BaseController
    {
        private StoreService _StoreService = new StoreService();
        private ProductService _productService = new ProductService();

        // GET: /Products/
        public ActionResult Index()
        { 
            return View();
        }

        public ActionResult Import()
        {
            var stores = new StoreService().Getstores();
            ViewBag.store = new SelectList(stores, "Id", "Name");


            return View();
        }

        // POST: /Products/Import
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Import([Bind(Include = "product_id,webstore_id,name,description,price,thumbnail,image,promofront,promodept,defaultAttribute,defaultAttCat,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version,IsSpecial,Url,Specifications,store")] mProducts product)
        {

            if (ModelState.IsValid)
            {
                product.description = WebUtility.HtmlDecode(product.description);
                product.Specifications = WebUtility.HtmlDecode(product.Specifications);
                _productService.Create(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Products/Store/5
        public ActionResult Store(string id, int? page)
        {  
            var listPaged = GetPagedNames(page, id); // GetPagedNames is found in BaseController
            if (listPaged == null)
                return HttpNotFound();
            ViewBag.Name = id;

            ViewBag.Title = id;

            ViewBag.seoTitle  = id;
            ViewBag.storeName = id;
            ViewBag.seoDesc = id;
            ViewBag.seoKeywords = id;

            return View(listPaged);
        }

        public ActionResult Brand(string id, int? page)
        {
            
            var listPaged = GetPagedBrands(page, id.ToLower()); // GetPagedNames is found in BaseController
            if (listPaged == null)
                return HttpNotFound();
            ViewBag.Name = id;

            ViewBag.Title = id;

            ViewBag.seoTitle = id;
            ViewBag.storeName = id;
            ViewBag.seoDesc = id;
            ViewBag.seoKeywords = id;

            return View(listPaged);
        }
         
    }
}
