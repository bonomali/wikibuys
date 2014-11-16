using System; 
using System.Linq; 
using seoWebApplication.st.SharkTankDAL;
using seoWebApplication.st.SharkTankDAL.dataObject;
using seoWebApplication.st.SharkTankDAL.Framework;
using seoWebApplication.Models;
using seoWebApplication.Service; 

namespace seoWebApplication.admin
{
    public partial class productAttributeValue : System.Web.UI.Page
    {
        private bool isSave = false;
         
        private const string VIEW_STATE_KEY_ProductAttributeValue = "ProductAttributeValue";
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SaveButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_SaveButton_Click);
            Master.CancelButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_CancelButton_Click); 
            Master.DeleteButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_DeleteButton_Click);
            
            int attId = 0;
            var dc = new ProductService();
            int id = commonClasses.GetId();
           
            if (!IsPostBack)
            {
                if (id > 0)
                {
                    LoadScreenFromObject(dc.GetProductAttribute(id, attId));
                }
                else
                {
                    loadDdlAtt();
                } 
            }
            else
            {
                attId = Convert.ToInt32(this.ddlAtt.SelectedValue);
                commonClasses.LoadDdlAttributeValue(ddlAttributes, Convert.ToInt32(ddlAtt.SelectedValue));            
            } 
        }

        void Master_DeleteButton_Click(object sender, EventArgs e)
        { 
            ENTValidationErrors validationErrors = new ENTValidationErrors();

            mProductAttributeValue pvalue = new mProductAttributeValue();
            LoadObjectFromScreen(pvalue);
            try
            {
                var dc = new ProductService();
                dc.DeleteAttrbuteValue(pvalue);
                GoToGridPage();
            }
            catch
            {
                GoToGridPage();
            }
        }
         
        void Master_CancelButton_Click(object sender, EventArgs e)
        {
            GoToGridPage();
        }

        void Master_SaveButton_Click(object sender, EventArgs e)
        { 
            ENTValidationErrors validationErrors = new ENTValidationErrors();
            mProductAttributeValue pvalue = new mProductAttributeValue();
            LoadObjectFromScreen(pvalue); 
            try
            { 
                var dc = new ProductService();
                dc.AddAttrbuteValue(pvalue);
                GoToGridPage();
            }
            catch
            {
                GoToGridPage();
            }
        }

        protected void LoadObjectFromScreen(mProductAttributeValue baseEO)
        {
            if (txtProductAttributeValueId.Text.Length > 0)
            {
                baseEO.ProductAttributeValueId = Convert.ToInt32(txtProductAttributeValueId.Text);
            }
            else {
                baseEO.ProductAttributeValueId = 0;
            }
           

            baseEO.AttributeValueID = Convert.ToInt32(ddlAttributes.SelectedValue);
            baseEO.product_id = Convert.ToInt32(commonClasses.GetP_order_id());
            baseEO.webstore_id = Convert.ToInt32(dBHelper.GetWebstoreId());
            baseEO.Name = this.ddlAtt.SelectedValue;
            baseEO.AttributeName = this.ddlAttributes.SelectedValue;
            baseEO.Value = Convert.ToDecimal(txtValue.Text);
        }

        protected void LoadScreenFromObject(mProductAttributeValue baseEO)
        { 
            txtProductAttributeValueId.Text = Convert.ToString(baseEO.ProductAttributeValueId);

            if (commonClasses.GetId().Equals(0))
            {
            txtproduct_id.Text = Convert.ToString(commonClasses.GetP_order_id());
            }
            else
            { 
            txtproduct_id.Text = Convert.ToString(baseEO.product_id);
            }
             
            commonClasses.LoadDdlAttributeValue(ddlAttributes, Convert.ToInt32(ddlAtt.SelectedValue), baseEO.AttributeValueID);     

            txtwebstore_id.Text = Convert.ToString(baseEO.webstore_id);

            txtValue.Text = Convert.ToString(baseEO.Value);

            txtInsertENTUserAccountId.Text = Convert.ToString(baseEO.InsertENTUserAccountId);  
        }

        protected void LoadControls()
        {
   
        }

        public void loadDdlAtt()
        {
            commonClasses.LoadAttributes(ddlAtt); 
        }
         
        protected void GoToGridPage()
        {
            Response.Redirect("product.aspx" + EncryptQueryString.Get("id=" + commonClasses.GetP_order_id()));
        }
         
        public string MenuItemName()
        {
            return "product";
        }
         
    }
}