using HtmlAgilityPack;
using seoWebApplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace seoWebApplication.DAL
{
    public class HtmlTool
    { 
        public static OGMeta FetchAmazon(string url) {
            OGMeta meta = new OGMeta();

            //var getHtmlWeb = new HtmlWeb(); 
          

            var doc = Download(url);
            //var doc = getHtmlWeb.Load(url);


              
            meta.Description = doc.DocumentNode.SelectSingleNode("//div[@id='feature-bullets']").InnerHtml.Replace("\n","").Replace("\t","");

            //var releaseYearNode = doc.DocumentNode.SelectNodes("//*[contains(@class,'productDescriptionWrapper')]");

            meta.Title = doc.DocumentNode.SelectSingleNode("//span[@id='productTitle']").InnerHtml.Replace("\n", "").Replace("\t", "");
                

            string imageWrapper = doc.DocumentNode.SelectSingleNode("//div[@id='imgTagWrapperId']").InnerHtml;
            //data-old-hires
            var imageStr = "";
            var imageStr2 = "";
            var pairs = imageWrapper.Split(' ');
            foreach (var pair in pairs)
            {
                var index2 = pair.Split('='); 
              
                if (index2[0] == "data-old-hires") { 
                    imageStr = index2[1].Replace("\"", "");
                }
                else if (index2[0] == "data-a-dynamic-image")
                {
                    imageStr2 = index2[1].Replace("\"", "");
                }
            }

            if (imageStr == "") {
                var inBetween = GetBetween(imageStr2, "http", ".jpg");
                imageStr = "http" + inBetween + ".jpg";
            }

            meta.Image = imageStr;

            string _price = ""; 

            var node = doc.DocumentNode.SelectSingleNode("//td[@id='priceblock_dealprice']//span[1]");

            if (node != null)
            {
                //do something with node
                _price = node.InnerHtml.Replace("$", "");
            }
            else
            {
                var ourprice = doc.DocumentNode.SelectSingleNode("//span[@id='priceblock_ourprice']");
                if (ourprice != null)
                {
                    _price = ourprice.InnerHtml.Replace("$", "");
                }
                else
                {
                    var saleprice = doc.DocumentNode.SelectSingleNode("//span[@id='priceblock_saleprice']");
                    if (saleprice != null)
                    {
                        _price = saleprice.InnerHtml.Replace("$", "");
                    }
                    else
                    {
                        var dealprice = doc.DocumentNode.SelectSingleNode("//span[@id='priceblock_dealprice']");
                        if (dealprice != null)
                        {
                            _price = dealprice.InnerHtml.Replace("$", "");
                        }
                        else
                        {
                            _price = "0".ToString();
                        }
                    }
                }
            }
            
            meta.Price = _price;

            return meta;
        }

        public static string GetBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        public static HtmlDocument Download(string url)
        {
            HtmlDocument hdoc = new HtmlDocument();
            HtmlNode.ElementsFlags.Remove("option");
            HtmlNode.ElementsFlags.Remove("select");
            Stream read = null;
            // Create web client.
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11";
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            client.Headers[HttpRequestHeader.Accept] = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"; 
            client.Headers[HttpRequestHeader.AcceptLanguage] = "en-GB,en-US;q=0.8,en;q=0.6";
            client.Headers[HttpRequestHeader.AcceptCharset] = "ISO-8859-1,utf-8;q=0.7,*;q=0.3";

            // Download string.
            string value = "";
            try
            {
                value = client.DownloadString(url); 
            }
            catch (ArgumentException)
            {
                read = client.OpenRead(WebUtility.UrlEncode(url));
            }

            string newVal = value.Replace("\n", "").Replace("\t", "");
            hdoc.LoadHtml(newVal);


            return hdoc;
        }

        public class CookieWebClient : WebClient
        {

            public CookieContainer m_container = new CookieContainer();
            public WebProxy proxy = null;

            protected override WebRequest GetWebRequest(Uri address)
            {
                try
                {
                    ServicePointManager.DefaultConnectionLimit = 1000000;
                    WebRequest request = base.GetWebRequest(address);
                    request.Proxy = proxy;

                    HttpWebRequest webRequest = request as HttpWebRequest;
                    webRequest.Pipelined = true;
                    webRequest.KeepAlive = true;
                    if (webRequest != null)
                    {
                        webRequest.CookieContainer = m_container;
                    }

                    return request;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static OGMeta FetchOG(string url)
        {
            OGMeta meta = new OGMeta();

            string html = FetchHtml(url);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);


            var offerlink = doc.DocumentNode.SelectNodes("//a");
            foreach (var node in offerlink)
            {
                if (node.HasAttributes)
                {
                    if (node.Attributes["class"] != null)
                    {
                        if (node.Attributes["class"].Value.Contains("offerlink"))
                        {
                            meta.offerlink = node.Attributes["href"].Value;
                        }
                    }
                }
            }
             
            var priceList = doc.DocumentNode.SelectNodes("//span");
            foreach (var node in priceList)
            {
                if (node.HasAttributes)
                {
                    if (node.Attributes["class"] != null)
                    {
                        if (node.Attributes["class"].Value == "price")
                        {
                            meta.Price = node.InnerText.Replace("$", "");
                        } 
                    }
                    if (node.Attributes["id"] != null)
                    { 
                        if (node.Attributes["id"].Value == "priceblock_ourprice")
                        {
                            meta.Price = node.InnerText.Replace("$", "");
                        }
                    }
                }
            }

            var list = doc.DocumentNode.SelectNodes("//meta");
            foreach (var node in list)
            {
                if (node.HasAttributes)
                {
                    if (node.Attributes["property"] != null && node.Attributes["content"] != null)
                    {
                        switch (node.Attributes["property"].Value)
                        {
                            case "fb:app_id": meta.AppId = node.Attributes["content"].Value;
                                break;
                            case "og:site_name": meta.SiteName = node.Attributes["content"].Value;
                                break;
                            case "og:locale": meta.Locale = node.Attributes["content"].Value;
                                break;
                            case "og:type": meta.Type = node.Attributes["content"].Value;
                                break;
                            case "og:title": meta.Title = node.Attributes["content"].Value;
                                break;
                            case "og:description": meta.Description = node.Attributes["content"].Value;
                                break;
                            case "description":
                                if (meta.Description != null && meta.Description.Length < 1)
                                {
                                    meta.Description = node.Attributes["content"].Value;
                                }
                                break;
                            case "og:url": meta.Url = node.Attributes["content"].Value;
                                break;
                            case "og:image": meta.Image = node.Attributes["content"].Value;
                                break;
                            case "og:audio": meta.Audio = node.Attributes["content"].Value;
                                break;
                            case "og:video": meta.Video.Content = node.Attributes["content"].Value;
                                break;
                            case "og:video:type": meta.Video.Type = node.Attributes["content"].Value;
                                break;
                            case "og:video:width": meta.Video.Width = node.Attributes["content"].Value;
                                break;
                            case "og:video:height": meta.Video.Height = node.Attributes["content"].Value;
                                break;
                            case "og:video:tag": meta.Video.Tag = node.Attributes["content"].Value;
                                break;
                            case "og:video:secure_url": meta.Video.SecureUrl = node.Attributes["content"].Value;
                                break;
                        }
                    }

                    if (node.Attributes["name"] != null && node.Attributes["content"] != null)
                    {
                        switch (node.Attributes["name"].Value)
                        {

                            case "description":
                                if (meta.Description != null && meta.Description.Length < 1)
                                {
                                    meta.Description = node.Attributes["content"].Value;
                                }
                                break;
                        }
                    }

                  
                }
            }

            return meta;
        }

        public static string FetchHtml(string url)
        {
            string o = "";

            try
            {
                HttpWebRequest oReq = (HttpWebRequest)WebRequest.Create(url);
                oReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                HttpWebResponse resp = (HttpWebResponse)oReq.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                o = reader.ReadToEnd();

                var getHtmlWeb = new HtmlWeb();
                var document = getHtmlWeb.Load(url);
                var aTags = document.DocumentNode.SelectNodes("//a");
                int counter = 1;
                string OutputLabelText = "";
                if (aTags != null)
                {
                    foreach (var aTag in aTags)
                    {
                        OutputLabelText += counter + ". " + aTag.InnerHtml + " - " + aTag.Attributes["href"].Value + "\t" + "<br />";
                        counter++;
                    }
                }

                // ParseErrors is an ArrayList containing any errors from the Load statement
                if (document.ParseErrors != null && document.ParseErrors.Count() > 0)
                {
                    // Handle any parse errors as required

                }
                else
                {

                    if (document.DocumentNode != null)
                    {
                        HtmlAgilityPack.HtmlNode bodyNode = document.DocumentNode.SelectSingleNode("//body");

                        if (bodyNode != null)
                        {
                            // Do something with bodyNode
                        }
                    }
                }
            }
            catch (Exception ex) { }

            return o;
        }


        internal static OGMeta FetchEbay(string url)
        {
            OGMeta meta = new OGMeta();

            //var getHtmlWeb = new HtmlWeb(); 
             
            var doc = Download(url);
            //var doc = getHtmlWeb.Load(url);

            HtmlNode descriptionNode = doc.DocumentNode.SelectSingleNode("//div[@id='x-tm-desc']");

            string _description = "";
            if (descriptionNode != null)
            {
                //do something with node
                _description = descriptionNode.InnerHtml.Replace("\n", "").Replace("\t", "");
            }
            else if (descriptionNode == null)
            {
                //do something with node
                HtmlNode descriptionNode2 = doc.DocumentNode.SelectSingleNode("//div[@class='prodDetailSec']");
                if (descriptionNode2 != null)
                {
                    //do something with node
                    _description = descriptionNode2.InnerHtml.Replace("\n", "").Replace("\t", "");
                }
                else if (descriptionNode2 == null)
                {
                    HtmlNode descriptionNode3 = doc.DocumentNode.SelectSingleNode("//div[@id='vi-desc-maincntr']");
                    if (descriptionNode3 != null)
                    {
                        //do something with node
                        _description = descriptionNode3.InnerHtml.Replace("\n", "").Replace("\t", "");
                    }
                    else {
                        _description = "";
                    }
                }
            }


            meta.Description = _description;

            meta.Title = doc.DocumentNode.SelectSingleNode("//h1[@id='itemTitle']").InnerHtml.Replace("\n", "").Replace("\t", "").Replace("<span class=\"g-hdn\">Details about  &nbsp;</span>","");

             

            HtmlNode imageNode = doc.DocumentNode.SelectSingleNode("//img[@id='icImg']");
            HtmlAttribute src = imageNode.Attributes["src"];
             
            string imageStr = src.Value;
  
            meta.Image = imageStr;

            string _price = "";

            var node = doc.DocumentNode.SelectSingleNode("//span[@id='prcIsum_bidPrice']");

            if (node != null)
            {
                //do something with node
                _price = node.InnerHtml.Replace("$", "");
            }
            else
            {
                var ourprice = doc.DocumentNode.SelectSingleNode("//span[@id='prcIsum']");
                if (ourprice != null)
                {
                    _price = ourprice.InnerHtml.Replace("$", "");
                }
                else
                {
                    var saleprice = doc.DocumentNode.SelectSingleNode("//span[@id='priceblock_saleprice']");
                    if (saleprice != null)
                    {
                        _price = saleprice.InnerHtml.Replace("$", "");
                    }
                    else
                    {
                        var dealprice = doc.DocumentNode.SelectSingleNode("//span[@id='priceblock_dealprice']");
                        if (dealprice != null)
                        {
                            _price = dealprice.InnerHtml.Replace("$", "");
                        }
                        else
                        {
                            _price = "0".ToString();
                        }
                    }
                }
            }

            meta.Price = _price.Replace("US", ""); 

            return meta;
        }

        internal static OGMeta FetchNomorerack(string url)
        {
            OGMeta meta = new OGMeta();

            //var getHtmlWeb = new HtmlWeb(); 

            var doc = Download(url);
            //var doc = getHtmlWeb.Load(url);

            HtmlNode descriptionNode = doc.DocumentNode.SelectSingleNode("//p[@class='description']");

            HtmlNode mdnode = doc.DocumentNode.SelectSingleNode("//meta[@name='description']");
            var _description = "";
            var _title = "";
            if (mdnode != null)
            {
                HtmlAttribute desc;

                desc = mdnode.Attributes["content"];
                _description = desc.Value; 
            }

            HtmlNode mdtitle = doc.DocumentNode.SelectSingleNode("//meta[@property='og:title']");

            if (mdtitle != null)
            {
                HtmlAttribute title;

                title = mdtitle.Attributes["content"];
                _title = title.Value;
            }
             
            meta.Description = _description;

            meta.Title = _title;

             HtmlNode mdimage = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']");

             HtmlNode imageNode = doc.DocumentNode.SelectSingleNode("//div[@class='main']//img");
             HtmlAttribute src = imageNode.Attributes["src"];
                 
            var _image = "";
            if (imageNode != null)
            {
                HtmlAttribute image;

                image = imageNode.Attributes["src"]; ;
                _image = image.Value;
            }

            meta.Image = _image;

            string _price = "";

            HtmlNode mdprice = doc.DocumentNode.SelectSingleNode("//meta[@property='og:price:amount']");

            if (mdprice != null)
            {
                HtmlAttribute price;

                price = mdprice.Attributes["content"];
                _price = price.Value;
            }


            meta.Specifications = doc.DocumentNode.SelectSingleNode("//p[@class='description']").InnerHtml;
            meta.Price = _price.Replace("US", "");

            return meta;
        }
    }
}
