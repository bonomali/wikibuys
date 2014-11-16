using System; 
using System.Linq; 
using seoWebApplication.st.SharkTankDAL; 
using seoWebApplication.st.SharkTankDAL.dataObject;
using System.Web;
using seoWebApplication.Models; 
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AspNet.Identity.MongoDB;
using System.Net.Http;

namespace seoWebApplication 
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
         
        protected bool AuthenticateUser(String strUserName, String strPassword)
        {

            try
            {
                UserAccountEO userAccount = new UserAccountEO();
                return userAccount.Login(strUserName, strPassword);
            }
            catch
            {
                // Handle an error by displaying the information.
                return false;
            }
             

        }

        public void OnLogin(Object src, EventArgs e)
        {
            if (AuthenticateUser(m_textboxUserName.Text, m_textboxPassword.Text))
            {
                Session["AdminUser"] = "true";
                Session["webstore_id"] = dBHelper.GetWebstoreId();
                Session["AdminUserName"] = m_textboxUserName.Text;
                Session["AdminUserId"] = 1;
                InitializeIdentity();
                Response.Redirect("default.aspx");
            }
            else
            {
                Response.Write("Invalid login: You don't belong here...");
            }
        }

       

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }


        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentity()
        {
            var userManager =
                HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var roleManager =
                HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            string SuperUser = seoWebAppConfiguration.SuperUser;
            string SuperPassword = seoWebAppConfiguration.SuperPassword;
            string name = SuperUser;
            string password = SuperPassword;
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);

            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);

            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);

                // Set Email confirmation property (see note above):
                user.EmailConfirmed = true;
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);

            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}
