using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using seoWebApplication.st.SharkTankDAL;
using seoWebApplication.st.SharkTankDAL.dataObject;
using seoWebApplication.st.SharkTankDAL.Framework;
using seoWebApplication.Models;
using seoWebApplication.Service; 

namespace seoWebApplication.admin.catalog
{
    public partial class attributeValue : System.Web.UI.Page
    {
        private const string VIEW_STATE_KEY_AttributeValue = "AttributeValue";
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SaveButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_SaveButton_Click);
            Master.CancelButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_CancelButton_Click);
            var dc = new AttributeValueService();
            int id = commonClasses.GetId();
            int attrId = commonClasses.GetP_order_id();
            if (!IsPostBack)
            {
                if (id > 0)
                {
                    var attribute = dc.GetAttribute(id);
                    LoadScreenFromObject(attribute); 
                }
                else
                {
                    txtAttributeID.Text = attrId.ToString(); 
                    txtAttributeValueID.Text = "0"; 
                }
            }
        }

        void Master_CancelButton_Click(object sender, EventArgs e)
        {
            GoToGridPage();
        }

        void Master_SaveButton_Click(object sender, EventArgs e)
        {
            ENTValidationErrors validationErrors = new ENTValidationErrors();  
            int id = commonClasses.GetId();
            var dc = new AttributeValueService();
            mAttributeValue Attribute = new mAttributeValue();

            LoadObjectFromScreen(Attribute);

            try
            { 
                dc.Create(Attribute);
                GoToGridPage();
            }
            catch
            {
                GoToGridPage();
            }
        }

        protected void LoadObjectFromScreen(mAttributeValue baseEO)
        {

            baseEO.AttributeValueID = Convert.ToInt32(txtAttributeValueID.Text);

            baseEO.AttributeID = Convert.ToInt32(txtAttributeID.Text);

            baseEO.Value = txtValue.Text;

            baseEO.Webstore_id = Convert.ToInt32(dBHelper.GetWebstoreId()); 
             

            if (baseEO.AttributeValueID == 0)
            {
                baseEO.InsertENTUserAccountId = Convert.ToInt32(Session["AdminUserId"]);
            }
            else
            {
                baseEO.UpdateENTUserAccountId = Convert.ToInt32(Session["AdminUserId"]);
            }
             
        }

        protected void LoadScreenFromObject(mAttributeValue baseEO)
        {
            if (baseEO.AttributeValueID <= 0)
            {
                txtAttributeValueID.Text = "0";
            }
            else {
                txtAttributeValueID.Text = Convert.ToString(baseEO.AttributeValueID);
            } 
            if (commonClasses.GetId().Equals(0))
            {
                txtAttributeID.Text = Convert.ToString(commonClasses.GetP_order_id());
            }
            else
            { 
            txtAttributeID.Text = Convert.ToString(baseEO.AttributeID);
            }
            

            txtValue.Text = Convert.ToString(baseEO.Value);

            txtwebstore_id.Text = Convert.ToString(baseEO.Webstore_id);

            if (baseEO.AttributeValueID == 0)
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
            Response.Redirect("attribute.aspx" + EncryptQueryString.Get("id=" + (txtAttributeID.Text)));
        } 
        public string MenuItemName()
        {
            return "category";
        }
    }
}