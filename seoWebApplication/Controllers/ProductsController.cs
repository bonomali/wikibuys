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
    public class ProductsController : Controller
    {
        private StoreService _StoreService = new StoreService();
        private ProductService _productService = new ProductService();

        // GET: /Products/
        public ActionResult Index()
        { 
            return View();
        }

        // GET: /Products/Store/5
        public ActionResult Store(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var storeId = _StoreService.Getstores(id).Id;
            List<mProducts> products = _productService.GetProductsByStore(storeId);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }
        

         
    }
}
