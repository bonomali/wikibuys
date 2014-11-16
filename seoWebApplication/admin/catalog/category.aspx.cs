using System; 
using System.Linq; 
using seoWebApplication.st.SharkTankDAL; 
using seoWebApplication.st.SharkTankDAL.Framework;
using seoWebApplication.Service;
using seoWebApplication.Models; 

namespace seoWebApplication.admin.catalog
{
    public partial class category : System.Web.UI.Page
    {
        private const string VIEW_STATE_KEY_category = "category";
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SaveButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_SaveButton_Click);
            Master.CancelButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_CancelButton_Click);
            Master.DeleteButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_DeleteButton_Click);
            txtwebstore_id.Text = dBHelper.GetWebstoreId().ToString();
            txtInsertENTUserAccountId.Text = "1";
            var dc = new CategoriesService();
            int id = commonClasses.GetId();
            if (!IsPostBack) { 
                if (id > 0)
                {
                    LoadScreenFromObject(dc.GetCategoryById(id));
                }
                else {
                    commonClasses.LoadDdlDept(ddlDepartments, 0);
                }
            } 
        }

        void Master_DeleteButton_Click(object sender, EventArgs e)
        {
            ENTValidationErrors validationErrors = new ENTValidationErrors();
            var dc = new CategoriesService();
            dc.Delete(commonClasses.GetId());
            GoToGridPage();
        }

        void Master_CancelButton_Click(object sender, EventArgs e)
        {
            GoToGridPage();
        }

        void Master_SaveButton_Click(object sender, EventArgs e)
        {
            ENTValidationErrors validationErrors = new ENTValidationErrors();
            Categories categories = new Categories();
            
            categories.webstore_id = dBHelper.GetWebstoreId();
            LoadObjectFromScreen(categories);
            categories.department_id = Convert.ToInt32(ddlDepartments.SelectedValue.ToString());
            try
            {
                var dc = new CategoriesService();
                dc.Create(categories);
                GoToGridPage();
            }
            catch
            {
                GoToGridPage();
            }
        }

        protected void LoadObjectFromScreen(Categories baseEO)
        { 
            baseEO.department_id = Convert.ToInt32(this.ddlDepartments.SelectedValue); 

            baseEO.Name = txtname.Text;

            baseEO.Description = txtdescription.Text;

            baseEO.category_id = commonClasses.GetId();

            if (baseEO.category_id == 0)
            {
                baseEO.InsertENTUserAccountId = Convert.ToInt32(Session["AdminUserId"]);
            }
            else
            {
                baseEO.UpdateENTUserAccountId = Convert.ToInt32(Session["AdminUserId"]);
            }
             
        }

        protected void LoadScreenFromObject(Categories baseEO)
        {  
            commonClasses.LoadDdlDept(ddlDepartments, baseEO.department_id);

            txtwebstore_id.Text = Convert.ToString(dBHelper.GetWebstoreId());

            txtname.Text = Convert.ToString(baseEO.Name);

            txtdescription.Text = Convert.ToString(baseEO.Description);

            if (baseEO.category_id == 0)
            {
                baseEO.InsertENTUserAccountId = Convert.ToInt32(Session["AdminUserId"]);
            }
            else
            {
                baseEO.UpdateENTUserAccountId = Convert.ToInt32(Session["AdminUserId"]);
            }

            txtInsertENTUserAccountId.Text = Convert.ToString(baseEO.InsertENTUserAccountId); 
        }

        protected void LoadControls()
        {
        }
        protected void GoToGridPage()
        {
            Response.Redirect("categories.aspx");
        }

        public void ReloadPage()
        { 
            Response.Redirect("category.aspx" + EncryptQueryString.Get("id=" + (commonClasses.GetId())));
        }

        public string MenuItemName()
        {
            return "category";
        }
    }
}