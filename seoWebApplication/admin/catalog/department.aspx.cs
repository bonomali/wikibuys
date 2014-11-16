using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using seoWebApplication.st.SharkTankDAL;
using seoWebApplication.st.SharkTankDAL.dataObject;
using seoWebApplication.st.SharkTankDAL.Framework;
using seoWebApplication.Service;
using seoWebApplication.Models; 

namespace seoWebApplication.admin.catalog
{
    public partial class department : System.Web.UI.Page
    {
        private const string VIEW_STATE_KEY_department = "department";
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SaveButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_SaveButton_Click);
            Master.CancelButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_CancelButton_Click);
            Master.DeleteButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_DeleteButton_Click);

            txtwebstore_id.Text = dBHelper.GetWebstoreId().ToString();
            txtInsertENTUserAccountId.Text = "1"; 
            var dc = new DepartmentService();
            var id = commonClasses.GetId();
            if (id > 0) { 
            LoadScreenFromObject(dc.GetDepartmentsById(id));
            }
            

            if (!IsPostBack)
            {
            }
        }

        void Master_DeleteButton_Click(object sender, EventArgs e)
        {
            ENTValidationErrors validationErrors = new ENTValidationErrors();
            

            var dc = new DepartmentService();
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
            Departments department = new Departments();
            LoadObjectFromScreen(department);
            try
            {
                var dc = new DepartmentService();
                dc.Create(department);
                GoToGridPage();
            }
            catch
            {
                GoToGridPage();
            }
        }

        protected void LoadObjectFromScreen(Departments baseEO)
        {

            baseEO.webstore_id = Convert.ToInt32(txtwebstore_id.Text);

            baseEO.Description = txtDescription.Text;

            baseEO.Name = txtName.Text;

            var dc = new DepartmentService();
            baseEO.department_id = dc.GetLastId();

            if (baseEO.Id == null)
            {
                baseEO.InsertENTUserAccountId = Convert.ToInt32(Session["AdminUserId"]);
            }
            else
            {
                baseEO.UpdateENTUserAccountId = Convert.ToInt32(Session["AdminUserId"]);
            }
        }

        protected void LoadScreenFromObject(Departments baseEO)
        {

            txtwebstore_id.Text = Convert.ToString(dBHelper.GetWebstoreId());

            txtDescription.Text = Convert.ToString(baseEO.Description);

            txtName.Text = Convert.ToString(baseEO.Name);

            if (baseEO.Id == null)
            {
                baseEO.InsertENTUserAccountId = Convert.ToInt32(Session["AdminUserId"]);
            }
            else
            {
                baseEO.UpdateENTUserAccountId = Convert.ToInt32(Session["AdminUserId"]);
            }

            txtInsertENTUserAccountId.Text = Convert.ToString(baseEO.InsertENTUserAccountId);
            //Put the object in the view state so it can be attached back to the data context. 
        }

        protected void LoadControls()
        {
        }
        protected void GoToGridPage()
        {
            Response.Redirect("departments.aspx");
        }

        public void ReloadPage()
        {
            Departments department = (Departments)ViewState[VIEW_STATE_KEY_department];
            Response.Redirect("department.aspx" + EncryptQueryString.Get("id=" + (department.department_id)));
        }

        public string MenuItemName()
        {
            return "department";
        }
    }
}