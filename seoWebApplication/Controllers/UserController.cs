using seoWebApplication.DAL;
using seoWebApplication.Framework;
using seoWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace seoWebApplication.Controllers
{
    public class UserController : Controller
    {
        
        //
        // GET: /Login/

        public ActionResult Index()
        {
            ViewBag.ReturnUrl = Request.FilePath;
            return View();
        }

        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["id"] = "";
            Session["username"] = "";
            Session.Clear();
            return RedirectToAction("Index", "../");
        }

        // GET: /UserAccount/Login

        [AllowAnonymous]
        public ActionResult Login()
        { 
            return View();
        }

        // POST: /UserAccount/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            string returnUrl = "/Dashboard/";
            ;
            User user = new UserDal().GetByUserName(model.UserName);
            string hashPw = phasher.Hash(model.Password);
            if (user.UserName == model.UserName && user.Password == hashPw)
            {    
                Session["username"] = model.UserName;
                Session["ClientId"] = user.ClientId;
                Session["Id"] = user.Id;               
             
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return RedirectToLocal("../ClientLogin/");
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

    }
}
