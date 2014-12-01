using System;
using System.Collections.Generic;
using System.IO;
using CsQuery;
using CsQuery.ExtensionMethods;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using seoWebApplication.Models;
using seoWebApplication.Service;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using seoWebApplication.DAL;

namespace seoWebApplication.Controllers
{
    public class ScrapeController : Controller
    {
        private static JToken _dataToScrape;
        private static IList<ScrapeProperties> _scrapeElements;
        private ScrapeService _scrapeService = new ScrapeService(); 

        // GET: Scrape
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Scrape_Read([DataSourceRequest]DataSourceRequest request)
        { 
            var products = (from e in _scrapeService.GetmScrapes()
                            select e).ToList();

            DataSourceResult result = products.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult ScrapeProperties_Read([DataSourceRequest]DataSourceRequest request, Guid Id)
        {   
            DataSourceResult result = _scrapeService.GetmScrapeById(Id).Properties.ToDataSourceResult(request);
            return Json(result);
        }

        // GET: Scrape/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Scrape/Create
        public ActionResult Scrape(Guid id)
        {
            var json = _scrapeService.GetmScrapeById(id);
            ScrapePage(json);
            return View(json);
        }

        public ActionResult DeleteScrapeProperties(Guid Id, Guid Pid)
        {
            _scrapeService.DeleteScrapeProperties(Id, Pid);
            return RedirectToAction("Edit", "Scrape", new { Id = Pid });
        }

        public ActionResult AddKeyValues(Guid Id)
        {
            ViewBag.Id = Id; 
            return PartialView();
        }

        // POST: Scrape/Create
        [HttpPost]
        public ActionResult AddKeyValues(ScrapeProperties pvals)
        {
            Guid id = pvals.Id;
            _scrapeService.AddScrapeProperties(pvals);
            return RedirectToAction("Edit", "Scrape", new { Id = id });
        }
  
        // GET: Scrape/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Scrape/Create
        [HttpPost]
        public ActionResult Create(mScrape mScrape)
        {
            _scrapeService.Create(mScrape);
            return RedirectToAction("Index", "Scrape");
        }

        private void ScrapePage(mScrape json)
        {
            OGMeta meta = HtmlTool.FetchAmazon(json.Url);
        }

        private static Dictionary<string, string> ScrapeOnePage(string url)
        {

           

            var listOfPageElements = new List<Dictionary<string, string>>();
            Console.WriteLine("Scraping: " + url);

            CQ doc = CQ.CreateFromUrl(url);

            var pageScrape2 = new Dictionary<string, string>();

            foreach (var item in _scrapeElements)
            {
               
                pageScrape2.Add(item.Class, GetElementByIdPattern(doc, item.Class).ToString());
            }

            return pageScrape2;
        }

        private static Dictionary<string, string> ScrapeAllElements(object elementsSelector, CQ doc)
        {  
            var pageScrape = new Dictionary<string, string>();

            foreach (var item in _scrapeElements)
            {
                pageScrape.Add(item.Class, GetElementByIdPattern(doc, item.Class).ToString());
            }

            return pageScrape;
        }

        protected static CQ GetElementByIdPattern(CQ doc, string contains)
        {
            string select;
            if (contains.Contains("."))
            {
                var name = contains.Replace(".", "");
                select = string.Format("*[class*={0}]", name);
            }
            else {
                var name = contains.Replace("#", "");
                select = string.Format("*[id*={0}]", name);
            }
            
            return doc.Select(select).First();
        }

        // GET: Scrape/Edit/5
        public ActionResult Edit(Guid id)
        { 
            var json = _scrapeService.GetmScrapeById(id);
            ViewBag.Id = id;
            return View(json);
        }

        // POST: Scrape/Edit/5
        [HttpPost]
        public ActionResult Edit(mScrape mScrape)
        {
            try
            {
                // TODO: Add update logic here 
                _scrapeService.Edit(mScrape);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Scrape/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Scrape/Delete/5
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
