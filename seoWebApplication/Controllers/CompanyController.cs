 
using seoWebApplication.Models;
using seoWebApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace seoWebApplication.Controllers
{
    public class CompanyController : Controller
    {
        private CompanyService _companyService = new CompanyService();

        // GET: /Company/
        public ActionResult Index()
        {
            return View(_companyService.GetCompanys());
        }

        //
        // GET: /Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Company/Create
        public ActionResult Create()
        {
            return View(new Company());
        }

        //
        // POST: /Company/Create
        [HttpPost]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                company.IncorporatedDate = DateTime.Now;
                company.CreatedDate = DateTime.Now;

                _companyService.Create(company);

                return RedirectToAction("Index");
            }

            return View();
        }

        //
        // GET: /Company/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Company/Edit/5
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
        // GET: /Company/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Company/Delete/5
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
