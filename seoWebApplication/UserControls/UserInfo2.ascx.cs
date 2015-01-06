using System; 
using System.Linq; 

namespace seoWebApplication.UserControls
{
    public partial class UserInfo2 : System.Web.UI.UserControl
    {
        public bool loggedIn = false;
        protected void Page_Load(object sender, EventArgs e)
        { 
            //imgFb.Text = "<img id='fbImage' src='http://graph.facebook.com/" + Session["ProviderKey"] + "/picture?type=small' style='border-width:0px;float:right;'>";
           
            if (Session["UserName"] != null) {
                loggedIn = true;
            }
        }
    }
}