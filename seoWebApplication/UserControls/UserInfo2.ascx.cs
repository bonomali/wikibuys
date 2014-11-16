using System; 
using System.Linq; 

namespace seoWebApplication.UserControls
{
    public partial class UserInfo2 : System.Web.UI.UserControl
    {
        public bool loggedIn = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null) {
                loggedIn = true;
            }
        }
    }
}