using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using seoWebApplication.st.SharkTankDAL;
using seoWebApplication.st.SharkTankDAL.dataObject;
using seoWebApplication.st.SharkTankDAL.entObject;
using seoWebApplication.Service;
using seoWebApplication.Models;
using PayPal;
using PayPal.Api.Payments;

namespace seoWebApplication
{
    public partial class OrderVerify : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the title of the page
            this.Title = seoWebAppConfiguration.SiteName + " : Verify Order";
            if (!User.Identity.IsAuthenticated) 
            {
                Response.Redirect("/Account/Login");
            }
            // populate the control only on the initial page load
            if (!IsPostBack)
                PopulateControls(0);
        }

        // fill shopping cart controls with data
        private void PopulateControls(int points)
        {
            // get the items in the shopping cart
            IList<mShoppingCart> dt = ShoppingCartAccess.GetItems();
            // populate the list with the shopping cart contents
            grid.DataSource = dt;
            grid.DataBind();
            grid.Visible = true;
            // display the total amount
            decimal amount = ShoppingCartAccess.GetTotalAmount();
            totalAmountLabel.Text = String.Format("{0:c}", amount);    
            // check customer details

          

              //get customer id
           
            //custId = new customerEO().getUserId(Session["UserName"].ToString());
            CustomerService _customerService = new CustomerService();
            var customer  = _customerService.GetCustomerByUserName(User.Identity.Name);
            var custId = customer.CellPhone;
            int pointsAvail = Convert.ToInt32(customer.RewardPoints);
            //load total customer points
            lblPoints.Text = pointsAvail.ToString();

            int nwPnts = Convert.ToInt32(0);
            decimal newPoints = Convert.ToDecimal(0);

            decimal appliedPoints = Convert.ToDecimal(0);
            decimal pointMultiplier = Decimal.Round(pointsEO.getPointMultiplier(), 2);
            if (points > 0)
            {
                //check if points to apply is less than points available
                if (points > pointsAvail)
                {
                    points = pointsAvail;
                    txtPoints.Text = points.ToString();

                }

                appliedPoints = points / pointMultiplier;
                lblPointsApplied.Text = Decimal.Round(appliedPoints, 2).ToString();
                lblNewTotal.Text = Decimal.Round((amount - appliedPoints), 2).ToString();

            }
            else
            {
                lblPointsApplied.Text = Decimal.Round(0, 2).ToString();
                lblNewTotal.Text = Decimal.Round((amount), 2).ToString();

                newPoints = Convert.ToDecimal(amount * pointsEO.getPointPercentage());
                nwPnts = Convert.ToInt32(newPoints * pointsEO.getPointMultiplier());
            }

            if (appliedPoints > 0)
            {
                nwPnts = 0;
            }



            //load Points Name Message
            lblOrderPointsMsg.Text = "This order will get " + nwPnts.ToString() + " " + pointsEO.getPointName();

            //load points name 
            lblPointName.Text = pointsEO.getPointName() + ": ";
  
          

            // Populate shipping selection

            //using (seowebappDataContextDataContext db = new seowebappDataContextDataContext(dBHelper.GetSeoWebAppConnectionString()))
            //{
            //    this.ddlShippingDetails.DataSource = db.getShippingRegionByUsername(Session["UserName"].ToString(), Convert.ToInt32(dBHelper.GetWebstoreId()));
            //    this.ddlShippingDetails.DataTextField = "ShippingType";
            //    this.ddlShippingDetails.DataValueField = "ShippingID";
            //    this.ddlShippingDetails.DataBind();
            //}  
             
        }
 

        protected void updateButton_Click(object sender, EventArgs e)
        {
            // Number of rows in the GridView
            int rowsCount = grid.Rows.Count;
            // Will store a row of the GridView
            GridViewRow gridRow;
            // Will reference a quantity TextBox in the GridView
            TextBox quantityTextBox;
            // Variables to store product ID and quantity
            string productId;
            int quantity;
            // Was the update successful?
            bool success = true;
            // Go through the rows of the GridView
            for (int i = 0; i < rowsCount; i++)
            {
            // Get a row
            gridRow = grid.Rows[i];
            // The ID of the product being deleted
            productId = grid.DataKeys[i].Value.ToString();
            // Get the quantity TextBox in the Row
            quantityTextBox = (TextBox)gridRow.FindControl("editQuantity");
            // Get the quantity, guarding against bogus values
            if (Int32.TryParse(quantityTextBox.Text, out quantity))
            {
                // Update product quantity
                success = success && ShoppingCartAccess.UpdateItem(productId, quantity);
            }
            else
            {
                // if TryParse didn't succeed
                success = false;
            }
            // Display status message
            statusLabel.Text = success ?
            "Your shopping cart was successfully updated!" :
            "Some quantity updates failed! Please verify your cart!";
            }
            // Repopulate the control
            PopulateControls(0);
        }

        protected void placeOrderButton_Click(object sender, EventArgs e)
        {
            string cartId;
            cartId = new ShoppingCartEO().shoppingCartId;

            int custId;
            // custId = new customerEO().getUserId(Session["UserName"].ToString());

            CustomerService _customerService = new CustomerService();
            var customer = _customerService.GetCustomerByUserName(User.Identity.Name);
            custId = Convert.ToInt32(customer.CellPhone);
            //string shippingRegion;
            //shippingRegion = new customerEO().getShippingId(Session["UserName"].ToString());

            //int shippingId;
            //shippingId = new ShippingRegionEO().getShippingId(shippingRegion);

            decimal shippingAmount;
            shippingAmount = Convert.ToDecimal(ddlShippingDetails.SelectedValue.ToString());
           
            // display the total amount
             
             decimal amount = ShoppingCartAccess.GetTotalAmount();

            

            // Get tax ID or default to "No tax"

            //int taxId;
            //switch (shippingRegion)
            //{
            //    case "2":
            //        taxId = 1;
            //        break;
            //    default:
            //        taxId = 2;
            //        break;
            //}

            amount += shippingAmount;

            // Create the order and store the order ID  
            var dc = new OrderService();
            int orderId = dc.CreateOrder(cartId, custId, 9, 1, amount, ShoppingCartAccess.GetItems(), customer);

            //update cart total
            dc.UpdateOrderTotal(cartId, Convert.ToDecimal(lblNewTotal.Text), Convert.ToInt32(txtPoints.Text));

            Dictionary<string, string> sdkConfig = new Dictionary<string, string>();
            sdkConfig.Add("mode", "live");
            string accessToken = new OAuthTokenCredential(seoWebAppConfiguration.PaypalClientId, seoWebAppConfiguration.PaypalClientSecret, sdkConfig).GetAccessToken();
             
            APIContext apiContext = new APIContext(accessToken);
            apiContext.Config = sdkConfig; 
            Amount amnt = new Amount();
            amnt.currency = "USD";
            amnt.total = amount.ToString();


            List<Transaction> transactionList = new List<Transaction>();
            Transaction tran = new Transaction();
            tran.description = "Payment for Order ID:" + orderId;
            tran.amount = amnt;

            tran.item_list = new ItemList(); 
            tran.item_list.items = new List<Item>();
            //ItemList order = new ItemList();
            foreach(var i in ShoppingCartAccess.GetItems()){
                Item item = new Item { name = i.name, price = i.price.ToString(), quantity = i.quantity.ToString(), sku = i.name, currency = seoWebAppConfiguration.PaypalCurrency };
            
            tran.item_list.items.Add(item);

            }
             
            ShippingAddress sa = new ShippingAddress();
             
            sa.recipient_name = customer.FirstName + " " + customer.LastName;
            sa.line1 = customer.Address1;
            sa.line2 = customer.Address2;
            sa.state = customer.State;
            sa.city = customer.City;
            sa.country_code = customer.Country;
            sa.phone = customer.CellPhone;
            sa.postal_code = customer.Zip;

            tran.item_list.shipping_address = sa;
            transactionList.Add(tran);

            Payer payr = new Payer();
            payr.payment_method = "paypal";

            RedirectUrls redirUrls = new RedirectUrls();
            redirUrls.cancel_url = seoWebAppConfiguration.StoreUrl + seoWebAppConfiguration.PaypalCancelUrl;
            redirUrls.return_url = seoWebAppConfiguration.StoreUrl + seoWebAppConfiguration.PaypalReturnUrl;

            Payment pymnt = new Payment();
            pymnt.intent = "sale";
            pymnt.payer = payr;
            pymnt.transactions = transactionList;
            pymnt.redirect_urls = redirUrls;

            Payment createdPayment = pymnt.Create(apiContext);
           
            // Redirect to the conformation page
            Response.Redirect(createdPayment.links[1].href.ToString());

             
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //get points to use and update order total
            int pointsToUse = Convert.ToInt32(txtPoints.Text);

            //update controls
            PopulateControls(pointsToUse);
        }

         
        }
    }

