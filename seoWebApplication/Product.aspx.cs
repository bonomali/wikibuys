using System; 
using System.Web; 
using System.Web.UI; 
using System.Web.UI.WebControls; 
using seoWebApplication.st.SharkTankDAL; 
using seoWebApplication.Data;
using seoWebApplication.Service;
using System.Net;
using System.IO;
using System.Text; 

namespace seoWebApplication
{
    public partial class Product : System.Web.UI.Page
    {
        public bool loggedIn;
        public string storeName;
        public string seoDesc;
        public string seoKeywords;
        public string seoTitle;
        public string imgLogo;
        public int webstoreId; 
        public string facebookAppId;
        public string address;
        public string city2;
        public string phone;
        public string url;
        public string RedirectUrl;
        public string host;
        public string price; 
        public string fbUrl;
        protected void Page_Load(object sender, EventArgs e)
        {
                        // don't repopulate control on postbacks
            if (!IsPostBack)
            {
                // Retrieve product_id from the query string
                string product_id = Request.QueryString["idproduct"];

                try
                {
                    //this.Pictures.LoadProductPictures(Convert.ToInt32(product_id));
                    //this.PicturesModals.LoadProductModals(Convert.ToInt32(product_id));
                    webstoreId = seoWebAppConfiguration.IdWebstore;

                    facebookAppId = seoWebAppConfiguration.FacebookAppId;
                    storeName = seoWebAppConfiguration.StoreName;
                    seoDesc = seoWebAppConfiguration.StoreDesc + " at " + storeName;
                    seoKeywords = seoWebAppConfiguration.StoreKeywords + " at " + storeName;
                    seoTitle = seoWebAppConfiguration.StoreTitle;
                    address = seoWebAppConfiguration.StoreAddress;
                    city2 = seoWebAppConfiguration.StoreCity;
                    phone = seoWebAppConfiguration.StorePhone;
                    imgLogo = seoWebAppConfiguration.StoreImgLogo;
                    url = Linkor.ToProduct(product_id).ToString();
                    host = HttpContext.Current.Request.Url.Host;
                    fbUrl = seoWebAppConfiguration.FacebookUrl;

                    // 301 redirect to the proper URL if necessary
                    //Linkor.CheckProductUrl(Request.QueryString["product_id"]);
                }
                catch
                {

                }
                ProductDetails pd = catalogAccesor.GetProductDetails(product_id);
                // Does the product exist?
                if (pd.name != null)
                {
                    PopulateControls(pd);
                }
                else
                {
                    Server.Transfer("~/NotFound.aspx");
                }

            }
            // Retrieves product details
          
        }

        public static string HtmlEncode(string text)
        {
            char[] chars = HttpUtility.HtmlEncode(text).ToCharArray();
            StringBuilder result = new StringBuilder(text.Length + (int)(text.Length * 0.1));

            foreach (char c in chars)
            {
                int value = Convert.ToInt32(c);
                if (value > 127)
                    result.AppendFormat("&#{0};", value);
                else
                    result.Append(c);
            }

            return result.ToString();
        }
        // Fill the control with data
        private void PopulateControls(ProductDetails pd)
        {
        // Display product recommendations
        string productId = pd.product_id.ToString();
        ////recommendations.LoadProductRecommendations(productId);
        //this.ProductRecommendations1.LoadProductRecommendations(productId);
        //load the product attributes
        this.ProductCustomAttributes1.LoadProductAttributes(Convert.ToInt32(productId));
        this.ProductAttributes1.LoadProductAttributes(Convert.ToInt32(productId));
        this.ProductAttributesRadio1.LoadProductAttributesRadio(Convert.ToInt32(productId));
        // Display product details
        titleLabel.Text = pd.name;
        string shortDesc = pd.description.ToString();

        litDescription.Text = HttpUtility.HtmlDecode(shortDesc);
        priceLabel.Text += String.Format("{0:c}", pd.price);
        price = String.Format("{0:c}", pd.price);

        var product = new ProductService(); 
        RedirectUrl = product.GetProduct(Convert.ToInt32(productId)).Url;
        string fileName = pd.image;
        if (fileName.Length <= 0)
        {
            fileName = "Coming-Soon.gif";
        }

        productImage.ImageUrl = "ProductImages/" + fileName;
        // Set the title of the page
        this.Title = seoWebAppConfiguration.SiteName + " " + pd.name;
             
        seoDesc = seoWebAppConfiguration.SiteName + " " + pd.name;
        seoKeywords = seoWebAppConfiguration.SiteName + " " + pd.name;
        seoTitle = seoWebAppConfiguration.SiteName + " " + pd.name;
        imgLogo = "/ProductImages/" + fileName; 

        using (var dc = new seowebappDataContextDataContext())
        {
          var list =  dc.AttributeSelectByWId(dBHelper.GetWebstoreId());
        }
           

        }

        
 
}

 
}
   
