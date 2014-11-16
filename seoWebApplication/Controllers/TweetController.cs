using Abot.Core;
using Abot.Crawler;
using Abot.Poco;
using seoWebApplication.Service;
using System; 
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using seoWebApplication.DAL;
using seoWebApplication.Models;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;


namespace seoWebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TweetController : Controller
    {
        // GET: Tweet
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tweet/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tweet/Crawl
        public ActionResult Crawl()
        {

            return View();
        }

        // POST: Tweet/Crawl
        [HttpPost]
        public ActionResult Crawl(string url)
        {
            CrawlPage(url, true);
            return View();
        }

        private void CrawlPage(string url, bool first)
        {
            CrawlConfiguration crawlConfig = AbotConfigurationSectionHandler.LoadFromXml().Convert();
            crawlConfig.CrawlTimeoutSeconds = 100;
            crawlConfig.MaxConcurrentThreads = 10;
            crawlConfig.MaxPagesToCrawl = 1000;
            crawlConfig.UserAgentString = "abot v1.0 http://code.google.com/p/abot";
            crawlConfig.ConfigurationExtensions.Add("SomeCustomConfigValue1", "1111");
            crawlConfig.ConfigurationExtensions.Add("SomeCustomConfigValue2", "2222");

            //Will use app.config for confguration
            PoliteWebCrawler crawler = new PoliteWebCrawler();

            crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
            if (first)
            {
                crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
            }
            else {
                crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted2;
            }
           
            crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
            crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;

            CrawlResult result = crawler.Crawl(new Uri(url));

            if (result.ErrorOccurred)
                Console.WriteLine("Crawl of {0} completed with error: {1}", result.RootUri.AbsoluteUri, result.ErrorException.Message);
            else
                Console.WriteLine("Crawl of {0} completed without error.", result.RootUri.AbsoluteUri);


        }

        void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            Console.WriteLine("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri, pageToCrawl.ParentUri.AbsoluteUri);
        }

        void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;

             

            foreach (var i in crawledPage.ParsedLinks)
            {
                if (i.AbsoluteUri.Contains("products")){ 

                    OGMeta meta = HtmlTool.FetchOG(i.AbsoluteUri.ToString());
                     
                    System.Drawing.Image image = DownloadImageFromUrl(meta.Image); 
                    string rootPath = Server.MapPath("~/ProductImages");
                    string slug = Helpers.GenerateSlug(meta.Title) + ".jpg";
                    string fileName = System.IO.Path.Combine(rootPath, slug);
                    image.Save(fileName);

                    ProductService PS = new ProductService();
                    mProducts mp = new mProducts();

                    mp.description = meta.Description;
                    mp.name = meta.Title;
                    mp.price = Convert.ToDecimal(meta.Price);
                    mp.IsActive = false;
                    mp.image = slug;
                    mp.thumbnail = slug;
                    string newUrl = "";
                    string[] paramArray = meta.offerlink.Split('&');
                    foreach (string word in paramArray)
                    {
                        if (word.Contains("urllink")) {
                            newUrl = word.Replace("urllink=", "");
                        }
                    }
                    mp.Url = newUrl;

                    PS.Create(mp);
                }
                 
            }
            
        }

        void crawler_ProcessPageCrawlCompleted2(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;



            foreach (var i in crawledPage.ParsedLinks)
            {
                if (i.AbsoluteUri.Contains("products"))
                {

                     
                }

            }

        }

        public System.Drawing.Image DownloadImageFromUrl(string imageUrl)
        {
            System.Drawing.Image image = null;

            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                System.Net.WebResponse webResponse = webRequest.GetResponse();

                System.IO.Stream stream = webResponse.GetResponseStream();

                image = System.Drawing.Image.FromStream(stream);

                webResponse.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return image;
        }

        public struct LinkItem
        {
            public string Href;
            public string Text;

            public override string ToString()
            {
                return Href + "\n\t" + Text;
            }
        }

        static class LinkFinder
        {
            public static List<LinkItem> Find(string file)
            {
                List<LinkItem> list = new List<LinkItem>();

                // 1.
                // Find all matches in file.
                MatchCollection m1 = Regex.Matches(file, @"(<a.*?>.*?</a>)",
                    RegexOptions.Singleline);

                // 2.
                // Loop over each match.
                foreach (Match m in m1)
                {
                    string value = m.Groups[1].Value;
                    LinkItem i = new LinkItem();

                    // 3.
                    // Get href attribute.
                    Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
                    RegexOptions.Singleline);
                    if (m2.Success)
                    {
                        i.Href = m2.Groups[1].Value;
                    }

                    // 4.
                    // Remove inner tags from text.
                    string t = Regex.Replace(value, @"\s*<.*?>\s*", "",
                    RegexOptions.Singleline);
                    i.Text = t;

                    list.Add(i);
                }
                return list;
            }
        }
       

        void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;
            Console.WriteLine("Did not crawl the links on page {0} due to {1}", crawledPage.Uri.AbsoluteUri, e.DisallowedReason);
        }

        void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            Console.WriteLine("Did not crawl page {0} due to {1}", pageToCrawl.Uri.AbsoluteUri, e.DisallowedReason);
        }

        // GET: Tweet/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Tweet/Create
        [HttpPost]
        public ActionResult Create(string Search)
        {
            try
            {
                // TODO: Add insert logic here
                TwitterService TS = new TwitterService();
                TS.Search_FilteredSearch(Search);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tweet/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tweet/Edit/5
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

        // GET: Tweet/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tweet/Delete/5
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
