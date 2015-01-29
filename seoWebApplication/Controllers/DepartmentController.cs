using seoWebApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace seoWebApplication.Controllers
{
    public class DepartmentController : BaseController
    {
        private DepartmentService _departmentService = new DepartmentService();
      
        // GET: Department
        public ActionResult Index()
        { 
            return View();                  
        }

        // GET: Department/Details/5
        public ActionResult Details(string id, int? page)
        {
            var listPaged = GetPagedDepartments(page, id.ToLower()); // GetPagedNames is found in BaseController
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

        // GET: Department/Menu
        public ActionResult Menu()
        {
            var depts = (from e in _departmentService.GetDepartments()
                         select e).ToList();

            return PartialView(depts);
        }

        // GET: Department/SideNavMenu
        public ActionResult SideNavMenu()
        {
            var depts = (from e in _departmentService.GetDepartments()
                         select e).ToList();

            return PartialView(depts);
        }

        // POST: Department/Create
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

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Department/Edit/5
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

        // GET: Department/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Department/Delete/5
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
