using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using seoWebApplication.Data;
using Kendo.Mvc.UI;
using seoWebApplication.Service; 
using Kendo.Mvc.Extensions;

namespace seoWebApplication.Controllers
{
    public class OrdersController : Controller
    {
        private SeoWebAppEntities db = new SeoWebAppEntities();
        private OrderService _orderService = new OrderService(); 
      
        // GET: /Orders/
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Shipping).Include(o => o.Tax);
            return View(orders.ToList());
        }

        public ActionResult Orders_Read([DataSourceRequest]DataSourceRequest request)
        {
            //int clientId = Convert.ToInt32(Session["ClientId"]);
            var transformers = (from e in _orderService.GetOrders() 
                                select e).ToList();  

            DataSourceResult result = transformers.ToDataSourceResult(request);
            return Json(result);
        }

        // GET: /Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: /Orders/Create
        public ActionResult Create()
        {
            ViewBag.ShippingID = new SelectList(db.Shippings, "ShippingID", "ShippingType");
            ViewBag.TaxID = new SelectList(db.Taxes, "TaxID", "TaxType");
            return View();
        }

        // POST: /Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="OrderID,webstore_id,DateCreated,DateShipped,Verified,Completed,Canceled,Comments,CustomerName,CustomerEmail,ShippingAddress,CustomerID,Status,AuthCode,Reference,TaxID,ShippingID,total,cart_id,points")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShippingID = new SelectList(db.Shippings, "ShippingID", "ShippingType", order.ShippingID);
            ViewBag.TaxID = new SelectList(db.Taxes, "TaxID", "TaxType", order.TaxID);
            return View(order);
        }

        // GET: /Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShippingID = new SelectList(db.Shippings, "ShippingID", "ShippingType", order.ShippingID);
            ViewBag.TaxID = new SelectList(db.Taxes, "TaxID", "TaxType", order.TaxID);
            return View(order);
        }

        // POST: /Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="OrderID,webstore_id,DateCreated,DateShipped,Verified,Completed,Canceled,Comments,CustomerName,CustomerEmail,ShippingAddress,CustomerID,Status,AuthCode,Reference,TaxID,ShippingID,total,cart_id,points")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShippingID = new SelectList(db.Shippings, "ShippingID", "ShippingType", order.ShippingID);
            ViewBag.TaxID = new SelectList(db.Taxes, "TaxID", "TaxType", order.TaxID);
            return View(order);
        }

        // GET: /Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
