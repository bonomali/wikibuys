using seoWebApplication.DAL;
using seoWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace seoWebApplication.Controllers
{
    public class NoteController : Controller
    {
        //
        private NoteDal dal = new NoteDal();
        private bool disposed = false;
        // GET: /Note/
        public ActionResult Index()
        {
            return View(dal.GetAllNotes());
        }

        //
        // GET: /Note/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Note/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Task/Create

        [HttpPost]
        public ActionResult Create(Note note)
        {
            try
            {
                dal.CreateNote(note);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Note/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Note/Edit/5
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

        //
        // GET: /Note/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Note/Delete/5
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
