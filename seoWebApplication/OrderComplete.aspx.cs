using seoWebApplication.Framework;
using seoWebApplication.Service;
using seoWebApplication.st.SharkTankDAL.dataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace seoWebApplication
{
    public partial class OrderComplete : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
        // Uncomment to enforce SSL (as explained in Chapter 16)
        // (Master as BalloonShop).EnforceSSL = true;
        base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        // Set the title of the page
        this.Title = seoWebAppConfiguration.SiteName +
        " : Order Complete"; 
             string cartId;
             cartId = new ShoppingCartEO().shoppingCartId;
        var dc = new OrderService();
        //update order complete
        dc.UpdateOrderComplete(cartId, Request.QueryString["token"].ToString(), Request.QueryString["PayerID"].ToString());

        CustomerService _customerService = new CustomerService();
        var customer = _customerService.GetCustomerByUserName(User.Identity.Name);
        //send email verification
        string desc = "Dear " + customer.FirstName +  "\r\n";
        desc += @"Thank you for your order.  We appreciate your business.  We will send you an email when your order has shipped." + "\r\n" ;
            desc += @"-The Living Hale Team";
            emailSend.send(customer.Email, "Order Complete", desc);
        }
    }
}