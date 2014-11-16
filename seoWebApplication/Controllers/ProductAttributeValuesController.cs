using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using seoWebApplication.Data;
using seoWebApplication.Service;
using seoWebApplication.Models;

namespace seoWebApplication.Controllers
{
    public class ProductAttributeValuesController : Controller
    {
        private SeoWebAppEntities db = new SeoWebAppEntities();

        // GET: ProductAttributeValues
        public ActionResult Index()
        {
            var productAttributeValues = db.ProductAttributeValues.Include(p => p.AttributeValue).Include(p => p.product);
            return View(productAttributeValues.ToList());
        }

        // GET: ProductAttributeValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributeValue productAttributeValue = db.ProductAttributeValues.Find(id);
            if (productAttributeValue == null)
            {
                return HttpNotFound();
            }
            return View(productAttributeValue);
        }

        // GET: ProductAttributeValues/Create
        public ActionResult Create(int id)
        {
            var AttributeValues = new AttributeValueService().GetAllAttributes();
            ViewBag.AttributeValueID = new SelectList(AttributeValues, "AttributeValueID", "Value");
            ViewBag.product_id = id.ToString();
            ViewBag.webstore_id = seoWebApplication.st.SharkTankDAL.dBHelper.GetWebstoreId().ToString();
            
            return PartialView();
        }

        // POST: ProductAttributeValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductAttributeValueId,product_id,AttributeValueID,webstore_id,Value,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] ProductAttributeValue productAttributeValue)
        {
            
                mProductAttributeValue pvalue = new mProductAttributeValue();
                pvalue.Value = productAttributeValue.Value; 
                pvalue.AttributeValueID = productAttributeValue.AttributeValueID; 
                pvalue.product_id = productAttributeValue.product_id;
                pvalue.Name = new AttributeValueService().GetAttribute(pvalue.AttributeValueID).Value;

                new ProductService().AddAttrbuteValue(pvalue);
                return RedirectToAction("edit", "product", new { id = pvalue.product_id });
          
        }

        // GET: ProductAttributeValues/Edit/5
        public ActionResult Edit(int id, int attrId)
        {
            var AttributeValues = new AttributeValueService().GetAllAttributes();
            ViewBag.AttributeValueID = new SelectList(AttributeValues, "AttributeValueID", "Value");
            ViewBag.product_id = id.ToString();
            ViewBag.webstore_id = seoWebApplication.st.SharkTankDAL.dBHelper.GetWebstoreId().ToString();

            mProductAttributeValue productAttributeValue = new ProductService().GetProductAttribute(id, attrId); 

            if (productAttributeValue == null)
            {
                return HttpNotFound();
            }
            return PartialView(productAttributeValue); 
        }

        // POST: ProductAttributeValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductAttributeValueId,product_id,AttributeValueID,webstore_id,Value,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] mProductAttributeValue pval)
        {
            if (ModelState.IsValid)
            {
                mProductAttributeValue pvalue = new ProductService().GetProductAttribute(pval.product_id, pval.AttributeValueID);

                pvalue.Value = pval.Value;
                new ProductService().UpdateAttrbuteValue(pvalue);
                return RedirectToAction("edit", "product", new { id = pvalue.product_id });
            }
            return RedirectToAction("Index");
        }

        // GET: ProductAttributeValues/Delete/5
        public ActionResult Delete(int id, int attrId)
        { 
            mProductAttributeValue productAttributeValue = new ProductService().GetProductAttribute(id, attrId); ;
            if (productAttributeValue == null)
            {
                return HttpNotFound();
            }
            return PartialView(productAttributeValue);
        }

        // POST: ProductAttributeValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(mProductAttributeValue pvalue)
        {  
            new ProductService().DeleteAttrbuteValue(pvalue);
            return RedirectToAction("edit", "product", new { id = pvalue.product_id });
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
