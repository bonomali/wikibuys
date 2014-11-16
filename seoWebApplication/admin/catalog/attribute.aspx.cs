using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using seoWebApplication.st.SharkTankDAL;
using seoWebApplication.st.SharkTankDAL.dataObject;
using seoWebApplication.st.SharkTankDAL.Framework;
using System.Data;
using seoWebApplication.Service;
using seoWebApplication.Models; 

namespace seoWebApplication.admin
{
    public partial class attribute : System.Web.UI.Page
    {
        private const string VIEW_STATE_KEY_Attribute = "Attribute";

        enum ControlType
        {
            Checkbox,
            Select,
            Radio,
            Textbox            
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SaveButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_SaveButton_Click);
            Master.CancelButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_CancelButton_Click);
            var dc = new AttributeService();
            var dc2 = new AttributeValueService();
            int id = commonClasses.GetId();
            if (!IsPostBack)
            {
                if (id > 0)
                {
                    var attribute = dc.GetAttribute(id);
                    LoadScreenFromObject(attribute);
                    LoadGrid(dc2.GetAttributes(id));
                }
                else
                {
                    Array itemValues = System.Enum.GetValues(typeof(ControlType));
                    Array itemNames = System.Enum.GetNames(typeof(ControlType));

                    foreach (var value in itemValues)
                    {
                        ListItem item = new ListItem(value.ToString(), value.ToString());
                        ddlControl.Items.Add(item);
                    }

                    ddlControl.DataBind();

                    this.txtAttributeID.Text = "0";

                }
            }
            else {
                if (id > 0)
                { 
                    LoadGrid(dc2.GetAttributes(id));
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
            txtInsertENTUserAccountId.Text = "1";
            mAttribute Attribute = new mAttribute();

            LoadObjectFromScreen(Attribute);
            
            try
            {
                var dc = new AttributeService();
                dc.Create(Attribute);
                GoToGridPage();
            }
            catch
            {
                GoToGridPage();
            }
        }

        protected void LoadObjectFromScreen(mAttribute baseEO)
        {

            baseEO.AttributeID = Convert.ToInt32(txtAttributeID.Text);

            baseEO.Name = txtName.Text;

            baseEO.ControlType = ddlControl.SelectedValue;

            baseEO.Webstore_id = Convert.ToInt32(dBHelper.GetWebstoreId());

            baseEO.ApplyToAllProducts = chkapplyToAllProducts.Checked;

            baseEO.ApplyToCategory = chkapplyToCategory.Checked;
        }

        protected void LoadScreenFromObject(mAttribute baseEO)
        {
          
            txtAttributeID.Text = Convert.ToString(baseEO.AttributeID);

            txtName.Text = Convert.ToString(baseEO.Name);

            commonClasses.LoadDdlAttributes(ddlControl, baseEO.ControlType); 

            txtwebstore_id.Text = Convert.ToString(baseEO.Webstore_id);

            chkapplyToAllProducts.Checked = baseEO.ApplyToAllProducts;

            chkapplyToCategory.Checked = baseEO.ApplyToCategory;

            txtInsertENTUserAccountId.Text = Convert.ToString(baseEO.InsertENTUserAccountId); 
        }


        public void LoadGrid(IList<mAttributeValue> attributeValues)
        {
            

                BoundField bf2 = new BoundField();
                bf2.DataField = "AttributeValueID";
                bf2.HeaderText = "AttributeValueID";

                BoundField bf3 = new BoundField();
                bf3.DataField = "Value";
                bf3.HeaderText = "Value";

                if (IsPostBack)
                {
                    cgvAttributeValues.Columns.Clear();
                }
               

                cgvAttributeValues.Columns.Add(bf3);
                cgvAttributeValues.Columns.Add(bf2);

              

                cgvAttributeValues.AutoGenerateColumns = false;

                cgvAttributeValues.DataSource = attributeValues;

                cgvAttributeValues.DataBind(); 
        }

       
        protected void GoToGridPage()
        {
            Response.Redirect("attributes.aspx");
        }

        public void ReloadPage()
        {
            AttributeEO Attribute = (AttributeEO)ViewState[VIEW_STATE_KEY_Attribute];
            Response.Redirect("attribute.aspx" + EncryptQueryString.Get("id=" + (Attribute.AttributeID)));
        }

        public string MenuItemName()
        {
            return "product";
        }

        protected void menuTabs_MenuItemClick(object sender, MenuEventArgs e)
        {
            multiTabs.ActiveViewIndex = Int32.Parse(menuTabs.SelectedValue);

        }

        protected void cgvAttributeValues_RowDataBound(object sender, GridViewRowEventArgs e)
        {
             if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string aId, attrId;
                mAttributeValue obj = (mAttributeValue)(e.Row.DataItem);
                aId = obj.AttributeValueID.ToString(); 
                attrId = obj.AttributeID.ToString(); 
                //Add the edit link to the action column.
                HyperLink editLink = new HyperLink();

                editLink.Text = "Edit";

                editLink.NavigateUrl = "attributeValue.aspx" + EncryptQueryString.Get("id=" + (aId) + "&p_order_id=" + attrId);

                e.Row.Cells[1].Controls.Add(editLink);

            }
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("attributeValue.aspx" + EncryptQueryString.Get("id=0&newRec=true&p_order_id=" + txtAttributeID.Text));
        }

        
    }
}