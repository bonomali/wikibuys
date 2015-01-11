using PagedList;
using seoWebApplication.Models;
using seoWebApplication.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace seoWebApplication.Controllers
{
    public abstract class BaseController : Controller
    {
        private StoreService _StoreService = new StoreService();
        private ProductService _productService = new ProductService();

        protected IPagedList<mProducts> GetPagedNames(int? page, string name)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            var storeId = _StoreService.Getstores(name).Id;
            List<mProducts> products = _productService.GetProductsByStore(storeId);

            // page the list
            const int pageSize = 20;
            var listPaged = products.ToPagedList(page ?? 1, pageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;

            return listPaged;
        }

        protected IPagedList<mProducts> GetPagedSearch(int? page, string name)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;
             
            List<mProducts> products = _productService.GetProductsBySearch(name);

            // page the list
            const int pageSize = 20;
            var listPaged = products.ToPagedList(page ?? 1, pageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;

            return listPaged;
        }

        protected IPagedList<mProducts> GetPagedUser(int? page, string id)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;
             
            IList<mProducts> products = _productService.GetProductsByUserId(id);

            // page the list
            const int pageSize = 20;
            var listPaged = products.ToPagedList(page ?? 1, pageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;

            return listPaged;
        }

        // in this case we return IEnumerable<string>, but in most
        // - DB situations you'll want to return IQueryable<string>
        protected IEnumerable<string> GetStuffFromDatabase()
        {
            var sampleData = new StreamReader(Server.MapPath("~/App_Data/Names.txt")).ReadToEnd();
            return sampleData.Split('\n');
        }
    }
}