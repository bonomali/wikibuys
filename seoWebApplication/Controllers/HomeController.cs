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
    public class HomeController : BaseController
    {
        private ProductService _productService = new ProductService();
        private UserService _userService = new UserService();

        public HomeController()
        {
        }
        public HomeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        // GET: /Home/
        public ActionResult Index(int? page)
        { 
            //return Redirect("default.aspx"); 
            ViewBag.catalogTitleLabel = "Welcome to WikiBuys.com";
            ViewBag.catalogDescriptionLabel = seoWebAppConfiguration.SiteName + " LEARN ABOUT THE BEST PRODUCTS ON AMAZON, WE ARE A CURATED WIKI OF GREAT BUYS";
            // set the title of the page
            ViewBag.Title = seoWebAppConfiguration.SiteName +
            " LEARN ABOUT THE BEST PRODUCTS ON AMAZON, WE ARE A CURATED WIKI OF GREAT BUYS";
            var listPaged = GetPromoPage(page); // GetPagedNames is found in BaseController
            if (listPaged == null)
                return HttpNotFound();
            ViewBag.Name = "Welcome to WikiBUys.com"; ;

            ViewBag.Title = "LEARN ABOUT THE BEST PRODUCTS ON AMAZON, WE ARE A CURATED WIKI OF GREAT BUYS";

            ViewBag.seoTitle = "Welcome to WikiBUys.com";
            ViewBag.storeName = seoWebAppConfiguration.SiteName;
            ViewBag.seoDesc = seoWebAppConfiguration.SiteName + " LEARN ABOUT THE BEST PRODUCTS ON AMAZON, WE ARE A CURATED WIKI OF GREAT BUYS"; ;
            ViewBag.seoKeywords = seoWebAppConfiguration.SiteName + " LEARN ABOUT THE BEST PRODUCTS ON AMAZON, WE ARE A CURATED WIKI OF GREAT BUYS"; ;
            return View(listPaged); 
        }


         
        public ActionResult About()
        {
            return View();
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

       

        public ActionResult Contact()
        {
            return View();
        }

       
	}
}