using System;
using System.Collections.Generic;
using seoWebApplication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using seoWebApplication.Data;
using seoWebApplication.Models;
using seoWebApplication.Service;
using seoWebApplication.Framework;
using seoWebApplication.DAL;

namespace seoWebApplication.Controllers
{
    public class SearchController : BaseController
    {
        private ProductService _productService = new ProductService();

        public SearchController()
        {
        }
        public SearchController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Search
        public ActionResult Index()
        { 
            return View();
        }
        [ValidateInput(false)]
        // GET: Search/Details/5
        public ActionResult Details(string id, int? page)
        {  
            ViewBag.catalogTitleLabel = "Product Search";
            ViewBag.catalogDescriptionLabel = "You searched for \"" + id + "\"";
            // set the title of the page
            ViewBag.Title = seoWebAppConfiguration.SiteName +
            " : Product Search : " + id; 
            var listPaged = GetPagedSearch(page, id); // GetPagedNames is found in BaseController
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

        // GET: Search/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
