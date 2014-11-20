using System; 
using System.Linq;
using System.Net; 
using System.Web.Mvc; 
using seoWebApplication.Service;
using seoWebApplication.Models;
using Kendo.Mvc.UI; 
using Kendo.Mvc.Extensions;

namespace seoWebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    { 
        private ProductService _productService = new ProductService(); 
        
         // GET: /Product/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Product/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seoWebApplication.Models.mProducts product = _productService.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Product/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Products_Read([DataSourceRequest]DataSourceRequest request)
        {
            //int clientId = Convert.ToInt32(Session["ClientId"]);
            var products = (from e in _productService.GetProducts()
                                select e).ToList();

            DataSourceResult result = products.ToDataSourceResult(request);
            return Json(result);
        }

        // GET: /SetPromoDefault/
        public ActionResult SetPromoDefault(int Id)
        {
            _productService.SetPromoDefault(Id);

            return RedirectToAction("Index", "Product");
        }

        // GET: /RemovePromoDefault/
        public ActionResult RemovePromoDefault()
        {
            _productService.RemovePromoDefault();

            return RedirectToAction("Index", "Product");
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,webstore_id,name,description,price,thumbnail,image,promofront,promodept,defaultAttribute,defaultAttCat,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version,IsSpecial,Url,Specifications")] mProducts product)
        {
            if (ModelState.IsValid)
            {
                _productService.Create(product); 
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Product/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seoWebApplication.Models.mProducts product = _productService.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,webstore_id,name,description,price,thumbnail,image,promofront,promodept,defaultAttribute,defaultAttCat,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version,IsSpecial,Url,Specifications")] mProducts product)
        {
            if (ModelState.IsValid)
            {
                _productService.Update(product); 
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Product/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mProducts product = _productService.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _productService.Delete(id); 
            return RedirectToAction("Index");
        }
         
    }
}
