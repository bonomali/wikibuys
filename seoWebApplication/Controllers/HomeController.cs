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
using System.Text.RegularExpressions;

namespace seoWebApplication.Controllers
{
    public class HomeController : BaseController
    {
        private ProductService _productService = new ProductService();
        private UserService _userService = new UserService();

        public HomeController()
        {
        }

        public ActionResult Html(string page)
        {
            Regex regex = new Regex("^.*-p([0-9]+)?$");
            var v = regex.Match(page);
            string match = v.Groups[1].ToString();

            if (match != null)
            {
               
                seoWebApplication.Models.mProducts product = _productService.GetProduct(Convert.ToInt32(match));
                if (product == null)
                    return HttpNotFound();
                 
                // set the title of the page
                ViewBag.Title = seoWebAppConfiguration.SiteName + product.name;

                ViewBag.Name = product.name;

                ViewBag.Title = product.name;

                ViewBag.seoTitle = product.name; 
                ViewBag.seoDesc = product.description;
                ViewBag.seoKeywords = product.description;

                ViewBag.Url = page + ".html";
                return View("../product/Details", product);
            }
            else {
                return View();
            }
        }
        public HomeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        // GET: /Home/
        public ActionResult Index(int? page)
        { 
            //return Redirect("default.aspx"); 
            ViewBag.catalogTitleLabel = "Welcome to WikiDeals.Co";
            ViewBag.catalogDescriptionLabel = seoWebAppConfiguration.SiteName + " WikiDeals.Co is a wiki about deals. Its a site that allows companies to post deals online.";
            // set the title of the page
            ViewBag.Title = seoWebAppConfiguration.SiteName +
            " WikiDeals.Co is a wiki about deals. Its a site that allows companies to post deals online";
            var listPaged = GetPromoPage(page); // GetPagedNames is found in BaseController
            if (listPaged == null)
                return HttpNotFound();
            ViewBag.Name = "Welcome to WikiDeals.Co"; ;
 

            ViewBag.seoTitle = "Welcome to WikiDeals.Co";
            ViewBag.storeName = seoWebAppConfiguration.SiteName;
            ViewBag.seoDesc = seoWebAppConfiguration.SiteName + " is a wiki about deals. Its a site that allows companies to post deals online"; ;
            ViewBag.seoKeywords = seoWebAppConfiguration.SiteName + " is a wiki about deals. Its a site that allows companies to post deals online"; ;
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