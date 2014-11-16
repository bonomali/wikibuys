using seoWebApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace seoWebApplication.Admin.Controllers
{
    public class HomeController : Controller
    {
        private SeoWebAppEntities db = new SeoWebAppEntities();

        public ActionResult Index()
        {
            return View(db.cuisines.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}