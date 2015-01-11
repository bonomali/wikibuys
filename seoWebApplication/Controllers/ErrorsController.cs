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

namespace seoWebApplication.Controllers
{
    public class ErrorsController : BaseController
    {
        private ProductService _productService = new ProductService();
        private UserService _userService = new UserService();
          public ErrorsController()
        {
        }
          public ErrorsController(ApplicationUserManager userManager)
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
        public ActionResult NotFound()
        {
            string id = Request.Url.PathAndQuery.Replace("/", "").ToLower();
            int page = 1; 
            ActionResult result;
            if (!Request.IsAjaxRequest())
            { 
                ApplicationUser model = _userService.GetUser(id);
                if (model != null)
                {
                    ViewBag.catalogTitleLabel = "Product Search";
                    ViewBag.catalogDescriptionLabel = "You searched for \"" + id + "\"";
                    // set the title of the page
                    ViewBag.Title = seoWebAppConfiguration.SiteName +
                    " : Product Search : " + id;
                    var listPaged = GetPagedUser(page, id); // GetPagedNames is found in BaseController
                    if (listPaged == null)
                        return HttpNotFound();
                    ViewBag.Name = id;

                    ViewBag.Title = id;

                    ViewBag.seoTitle = id;
                    ViewBag.storeName = id;
                    ViewBag.seoDesc = id;
                    ViewBag.seoKeywords = id;
                    return View("../User/Details", listPaged); 
                 
                   
                }
                else {
                    ViewBag.Id = id;
                    return View();
                }
            }
            else
                return PartialView("_NotFound", id);
           
        }
        // GET: Errors
        public ActionResult Index()
        {
            return View();
        }

        // GET: Errors/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Errors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Errors/Create
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

        // GET: Errors/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Errors/Edit/5
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

        // GET: Errors/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Errors/Delete/5
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
