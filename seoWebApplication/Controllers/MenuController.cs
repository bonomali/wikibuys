using seoWebApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace seoWebApplication.Controllers
{
    public class MenuController : Controller
    {
        private SubcategoriesService _subcategoriesService = new SubcategoriesService();
        private CategoriesService _categoriesService = new CategoriesService();
        private DepartmentService _departmentService = new DepartmentService();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        // GET: Department/SideNavMenu
        public ActionResult Category(string id)
        {
            int _id = _departmentService.GetDepartmentsByName(id).department_id;
            var depts = (from e in _categoriesService.GetCategories()
                         where e.department_id == _id
                         select e).ToList();

            return PartialView(depts);
        }

        // GET: Department/SideNavMenu
        public ActionResult Subcategory(string id)
        {
            Guid _id = _categoriesService.GetCategoryByName(id).Id;
            var depts = (from e in _subcategoriesService.GetSubcategories()
                         where e.category_id == _id
                         select e).ToList();

            return PartialView(depts);
        }

        // GET: Menu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Menu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menu/Create
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

        // GET: Menu/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Menu/Edit/5
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

        // GET: Menu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Menu/Delete/5
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
