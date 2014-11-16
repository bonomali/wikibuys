using seoWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace seoWebApplication.UserControls
{
    public partial class LogIn : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
         
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Account/LoginFromControl/?Email=" + this.Email.Text + "&Password=" + this.Password.Text + "&RememberMe=" + this.RememberMe.Checked);

        }
    }
}