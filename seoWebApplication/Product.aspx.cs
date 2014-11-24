using System; 
using System.Web; 
using System.Web.UI; 
using System.Web.UI.WebControls; 
using seoWebApplication.st.SharkTankDAL; 
using seoWebApplication.Data; 

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

        public string address;
        public string city2;
        public string phone;
        public string url;
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

                    storeName = seoWebAppConfiguration.StoreName;
                    seoDesc = seoWebAppConfiguration.StoreDesc + " at " + storeName;
                    seoKeywords = seoWebAppConfiguration.StoreKeywords + " at " + storeName;
                    seoTitle = seoWebAppConfiguration.StoreTitle;
                    address = seoWebAppConfiguration.StoreAddress;
                    city2 = seoWebAppConfiguration.StoreCity;
                    phone = seoWebAppConfiguration.StorePhone;
                    imgLogo = seoWebAppConfiguration.StoreImgLogo;
                    url = HttpContext.Current.Request.Url.AbsoluteUri;
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
        descriptionLabel.Text = pd.description;
        priceLabel.Text += String.Format("{0:c}", pd.price);
        price = String.Format("{0:c}", pd.price);
        this.isActive.Checked = pd.isActive;
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

        

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // Retrieve ProductID from the query string
            string pid = Request.QueryString["idProduct"];

            string[] words = pid.Split(',');

            string productId = words[1];
            // Retrieves product details
            ProductDetails pd = catalogAccesor.GetProductDetails(productId);
            // Retrieve the selected product options
            string options = "";
            //add dropdown attributes
            foreach (Control cnt in this.ProductAttributes1.Controls)
            {
                if (cnt is PlaceHolder)
                {
                    PlaceHolder placeHolder1 = (PlaceHolder)ProductAttributes1.FindControl("attrPlaceHolder");

                    foreach (Control cnt2 in placeHolder1.Controls)
                    {
                        if (cnt2 is Literal)
                        {
                            Literal attrLabel = (Literal)cnt2;
                            if (!attrLabel.Text.Contains("<"))
                            {
                                options += attrLabel.Text;
                            }

                        }
                        if (cnt2 is DropDownList)
                        {
                            DropDownList attrDropDown = (DropDownList)cnt2;
                            options += attrDropDown.Items[attrDropDown.SelectedIndex] + "; ";
                        }
                    }

                }
            }

            //add radio attributes
            foreach (Control cnt in this.ProductAttributesRadio1.Controls)
            {
                if (cnt is PlaceHolder)
                {
                    PlaceHolder placeHolder1 = (PlaceHolder)ProductAttributesRadio1.FindControl("attrPlaceHolderRadio");

                    foreach (Control cnt2 in placeHolder1.Controls)
                    {
                        if (cnt2 is Literal)
                        {
                            Literal attrLabel = (Literal)cnt2;
                            if (!attrLabel.Text.Contains("<"))
                            {
                                options += attrLabel.Text;
                            }

                        }
                        if (cnt2 is RadioButtonList)
                        {
                            RadioButtonList attrDropDown = (RadioButtonList)cnt2;
                            options += attrDropDown.Items[attrDropDown.SelectedIndex] + "; ";
                        }
                    }

                }
            }

            //add radio attributes
            foreach (Control cnt in this.ProductCustomAttributes1.Controls)
            {
                if (cnt is PlaceHolder)
                {
                    PlaceHolder placeHolder1 = (PlaceHolder)ProductCustomAttributes1.FindControl("attrPlaceHolder");

                    foreach (Control cnt2 in placeHolder1.Controls)
                    {
                        if (cnt2 is Literal)
                        {
                            Literal attrLabel = (Literal)cnt2;
                            if (!attrLabel.Text.Contains("<"))
                            {
                                options += attrLabel.Text;
                            }

                        }
                        if (cnt2 is DropDownList)
                        {
                            DropDownList attrDropDown = (DropDownList)cnt2;
                            options += attrDropDown.Items[attrDropDown.SelectedIndex] + "; ";
                        }
                    }

                }
            }
            // The Add to Cart link
            // Add the product to the shopping cart
            ShoppingCartAccess.AddItem(productId, options);
            Response.Redirect("/shoppingcart.aspx");
        }
}

 
}
   
