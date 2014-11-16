using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using seoWebApplication.Models;
using seoWebApplication.DAL; 

namespace seoWebApplication.Controllers
{
    public class SubscribeController : Controller
    {
        private UserDal dal = new UserDal();
        private bool disposed = false;
        // GET: /Subscribe/
        public ActionResult Index()
        {
            return View(dal.GetAll());
        }

        

        // GET: /Subscribe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Subscribe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User subscriber)
        { 
                try
                {
                    dal.Create(subscriber);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                } 
        }
         
        # region IDisposable

        new protected void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        new protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.dal.Dispose();
                }
            }

            this.disposed = true;
        }

        # endregion
    }
}
