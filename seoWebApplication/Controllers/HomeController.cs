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
    public class HomeController : Controller
    {
        private ProductService _productService = new ProductService();

        public HomeController()
        {
        }
        public HomeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        // GET: /Home/
        public ActionResult Index()
        { 
            return Redirect("default.aspx"); 
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