using seoWebApplication.Models;
using seoWebApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace seoWebApplication.Controllers
{
    public class DeviceController : Controller
    {
        private DeviceService _deviceService = new DeviceService();

        // GET: /Company/
        public ActionResult Index()
        {
            return View(_deviceService.GetDevices());
        }

        //
        // GET: /Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Company/Create
        public ActionResult Create(string id)
        {
            ViewBag.Id = id;
            return View(new Device());
        }

        //
        // POST: /Company/Create
        [HttpPost]
        public ActionResult Create(Device device)
        {
            if (ModelState.IsValid)
            { 
                device.CreatedDate = DateTime.Now;

                _deviceService.Create(device);

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
