using System;
using System.Web;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using seoWebApplication.Data;
/// <summary>
/// Link factory class
/// </summary>

 
    public class Link
    {

        

        // regular expression that removes characters that aren't a-z, 0-9, dash,
        // underscore or space
        private static Regex purifyUrlRegex = new Regex("[^-a-zA-Z0-9_ ]", RegexOptions.Compiled);
        // regular expression that changes dashes, underscores and spaces to dashes
        private static Regex dashesRegex = new Regex("[-_ ]+", RegexOptions.Compiled);

        // prepares a string to be included in an URL
        private static string PrepareUrlText(string urlText)
        {
            // remove all characters that aren't a-z, 0-9, dash, underscore or space
            urlText = purifyUrlRegex.Replace(urlText, "");
            // remove all leading and trailing spaces
            urlText = urlText.Trim();
            // change all dashes, underscores and spaces to dashes
            urlText = dashesRegex.Replace(urlText, "-");
            // return the modified string
            return urlText;
        }
        // Builds an absolute URL
        private static string BuildAbsolute(string relativeUri)
        {
            // get current uri
            Uri uri = HttpContext.Current.Request.Url;
            // build absolute path
            string app = HttpContext.Current.Request.ApplicationPath;
            if (!app.EndsWith("/")) app += "/";
            relativeUri = relativeUri.TrimStart('/');
            // return the absolute path
            return HttpUtility.UrlPathEncode(
            String.Format("http://{0}:{1}{2}{3}",
            uri.Host, uri.Port, app, relativeUri));
        }
        // Generate a department URL
      

        public static string ToCategoryType(int id)
        {
            // prepare category URL name 
            string UrlName;
            UrlName = "";
            try
            { 
                SeoWebAppEntities db = new SeoWebAppEntities();
                cuisine cuisine = db.cuisines.Find(id);
                UrlName = PrepareUrlText(cuisine.SeoTitle);
            }
            catch
            { 
            
            }
            // build URL 
            return BuildAbsolute(String.Format("ct/{0}/{1}/", id, UrlName)); 
        }

        public static string ToStore(int id)
        {
            // prepare category URL name 
            string UrlName;
            UrlName = "";
            string hasUrl;
            hasUrl = "";
            string retUrl;
            retUrl = "";
            try
            {
                SeoWebAppEntities db = new SeoWebAppEntities();
                webstore webstore = db.webstores.Find(id);
                UrlName = PrepareUrlText(webstore.webstoreName);
                hasUrl = webstore.webstoreUrl;
            }
            catch
            {

            }
            // build URL 
            if (hasUrl == null) { 
            retUrl = BuildAbsolute(String.Format("store/{0}/{1}/", id, UrlName));
            }
            else{
                retUrl = "http://" + hasUrl;
            }

            return retUrl;
        }

        public static string ToProduct(int id)
        {
            // prepare category URL name 
            string UrlName;
            UrlName = "";
            int webstoreId = 0;
            try
            {
                SeoWebAppEntities db = new SeoWebAppEntities();
                product product = db.products.Find(id);
                UrlName = PrepareUrlText(product.name);
                webstoreId = product.webstore_id;
            }
            catch
            {

            }
            // build URL 
            return BuildAbsolute(String.Format("store/{0}/product/{1}/{2}", webstoreId, id, UrlName));
        }

        public static string ToDepartment(int id)
        {
            // prepare category URL name 
            string UrlName;
            UrlName = "";
            int webstoreId = 0;
            try
            {
                SeoWebAppEntities db = new SeoWebAppEntities();
                department department = db.departments.Find(id);
                UrlName = PrepareUrlText(department.Name);
                webstoreId = department.webstore_id;
            }
            catch
            {

            }
            // build URL 
            return BuildAbsolute(String.Format("store/{0}/d/{1}/{2}", webstoreId, id, UrlName));
        }

        public static string ToCategory(int id)
        {
            // prepare category URL name 
            string UrlName;
            UrlName = "";
            int webstoreId = 0;
            try
            {
                SeoWebAppEntities db = new SeoWebAppEntities();
                category category = db.categories.Find(id);
                UrlName = PrepareUrlText(category.name);
                webstoreId = category.webstore_id;
            }
            catch
            {

            }
            // build URL 
            return BuildAbsolute(String.Format("store/{0}/c/{1}/{2}", webstoreId, id, UrlName));
        }
         

        // Generate a department URL for the first page
        
        public static string ToSearch(string searchString, bool allWords, string page)
        {
            if (page == "1")
                return BuildAbsolute(
                String.Format("/Search.aspx?Search={0}&AllWords={1}",
                searchString, allWords.ToString()));
            else
                return BuildAbsolute(
                String.Format("/Search.aspx?Search={0}&AllWords={1}&Page={2}",
                searchString, allWords.ToString(), page));
        }
 
 
    }
 
