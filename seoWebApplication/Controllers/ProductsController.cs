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
