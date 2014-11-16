using System; 
using System.Linq; 
using System.Web.UI.WebControls;
using seoWebApplication.st.SharkTankDAL; 
using seoWebApplication.st.SharkTankDAL.Framework;
using seoWebApplication.Service;
using seoWebApplication.Models; 

namespace seoWebApplication.admin
{
    public partial class product : System.Web.UI.Page
    {
        public string Id;
        private const string VIEW_STATE_KEY_product = "product";
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SaveButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_SaveButton_Click);
            Master.CancelButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_CancelButton_Click);
            Master.DeleteButton_Click += new seoWebAppAdminEditPage.ButtonClickedHandler(Master_DeleteButton_Click);
            txtwebstore_id.Text = dBHelper.GetWebstoreId().ToString(); 

             int id = commonClasses.GetId();
             var dc = new ProductService();
             if (id > 0 && !IsPostBack)
             {
                 LoadScreenFromObject(dc.GetProduct(id));
             }

             if (id > 0 && IsPostBack) {
                 loadGrid(dc.GetProduct(id));
                 loadCatGrid();
             }
        }

        void Master_CancelButton_Click(object sender, EventArgs e)
        {
            GoToGridPage();
        }

        void Master_DeleteButton_Click(object sender, EventArgs e)
        {
            ENTValidationErrors validationErrors = new ENTValidationErrors();
            
            var dc = new ProductService();
           
            int id = commonClasses.GetId();
            if (id > 0)
            {
                dc.Delete(id);
            }
            else
            {
                commonClasses.LoadDdlDept(this.ddlCategory, 0);
            }
        }

        void Master_SaveButton_Click(object sender, EventArgs e)
        {
            ENTValidationErrors validationErrors = new ENTValidationErrors();
            mProducts product = new mProducts();
            LoadObjectFromScreen(product);
            try
            {
                if (product.product_id > 0)
                {
                    var dc = new ProductService();
                    dc.Update(product);
                }
                else { 
                     var dc = new ProductService();
                    dc.Create(product);
                }
               
                GoToGridPage();
            }
            catch
            {
                GoToGridPage();
            }
        }

        protected void LoadObjectFromScreen(mProducts baseEO)
        {

            baseEO.webstore_id = Convert.ToInt32(txtwebstore_id.Text);

            baseEO.name = txtname.Text;

            baseEO.description = txtdescription.Text;

            baseEO.price = Convert.ToDecimal(txtprice.Text);

            baseEO.thumbnail = txtthumbnail.Text;

            baseEO.image = txtimage.Text;

            baseEO.promofront = chkpromofront.Checked;

            baseEO.promodept = chkpromodept.Checked;

            baseEO.product_id = commonClasses.GetId();

            baseEO.defaultAttribute = chkdefaultAttribute.Checked;

            baseEO.defaultAttCat = chkdefaultAttCat.Checked;
        }

        protected void LoadScreenFromObject(mProducts baseEO)
        {
            

            txtwebstore_id.Text = Convert.ToString(dBHelper.GetWebstoreId());

            txtname.Text = Convert.ToString(baseEO.name);

            txtdescription.Text = Convert.ToString(baseEO.description);

            txtprice.Text = Convert.ToString(baseEO.price);

            txtthumbnail.Text = Convert.ToString(baseEO.thumbnail);

            txtimage.Text = Convert.ToString(baseEO.image);

            chkpromofront.Checked = baseEO.promofront;

            chkdefaultAttribute.Checked = baseEO.defaultAttribute;

            chkdefaultAttCat.Checked = baseEO.defaultAttCat;

            chkpromodept.Checked = baseEO.promodept;

            loadGrid(baseEO);
            commonClasses.LoadDdlCategory(ddlCategory, 0);
            loadCatGrid();
            this.AdminPictures.LoadProductPictures(baseEO.product_id);
            this.Id = baseEO.product_id.ToString();
        }

        protected void LoadControls()
        {
        }
        protected void GoToGridPage()
        {
            Response.Redirect("products.aspx");
        }

        public void ReloadPage()
        { 
            Response.Redirect("Product.aspx" + EncryptQueryString.Get("id=" + (commonClasses.GetId())));
        }

        public string MenuItemName()
        {
            return "product";
        }

        protected void menuTabs_MenuItemClick(object sender, MenuEventArgs e)
        {
            multiTabs.ActiveViewIndex = Int32.Parse(menuTabs.SelectedValue);

        }

        protected void upload1Button_Click(object sender, EventArgs e)
        {
            // proceed with uploading only if the user selected a file
            if (image1FileUpload.HasFile)
            {
                try
                {
                    string fileName = image1FileUpload.FileName;
                    string location = Server.MapPath("~/ProductImages/") + fileName;
                    // save image to server
                    image1FileUpload.SaveAs(location);

                    var dc = new ProductService();
                    dc.ProductThumbnailUpdate(commonClasses.GetId(), dBHelper.GetWebstoreId(), fileName);
                     
                    ReloadPage();
                     
                }
                catch
                {
                    statusLabel.Text = "Uploading image 1 failed";
                }
            }
        }

        protected void upload2Button_Click(object sender, EventArgs e)
        {
            // proceed with uploading only if the user selected a file
            if (image2FileUpload.HasFile)
            {
                try
                {
                    string fileName = image2FileUpload.FileName;
                    string location = Server.MapPath("~/ProductImages/") + fileName;
                    // save image to server
                    image2FileUpload.SaveAs(location);

                    var dc = new ProductService();
                    dc.ProductImageUpdate(commonClasses.GetId(), dBHelper.GetWebstoreId(), fileName);
                    // reload the page
                    ReloadPage();
                }
                catch
                {
                    statusLabel.Text = "Uploading image 2 failed";
                }
            }
        }



        protected void cgvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string cId, pid;
                Categories obj = (Categories)(e.Row.DataItem);
                cId = obj.category_id.ToString();
                pid = commonClasses.GetId().ToString();
                //Add the edit link to the action column.
                HyperLink editLink = new HyperLink();

                editLink.Text = "DEL";

                editLink.NavigateUrl = "deleteProductCategory.aspx" + EncryptQueryString.Get("id=" + cId + "&p_order_id=" + pid);

                e.Row.Cells[2].Controls.Add(editLink);

            }

        }

        protected void cgvAttributeValues_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string aId, pId;
                mProductAttributeValue obj = (mProductAttributeValue)(e.Row.DataItem);
                aId = obj.ProductAttributeValueId.ToString();
                pId = commonClasses.GetId().ToString();

                //Add the edit link to the action column.
                HyperLink editLink = new HyperLink();

                editLink.Text = "Edit";

                editLink.NavigateUrl = "productAttributeValue.aspx" + EncryptQueryString.Get("id=" + (aId) + "&p_order_id=" + (pId));

                e.Row.Cells[3].Controls.Add(editLink);

            }

        }

        public void loadGrid(mProducts p)
        {
            
                BoundField bf1 = new BoundField();
                bf1.DataField = "Value";
                bf1.HeaderText = "Value";

                BoundField bf2 = new BoundField();
                bf2.DataField = "Name";
                bf2.HeaderText = "Name";

                BoundField bf3 = new BoundField();
                bf3.DataField = "AttributeName";
                bf3.HeaderText = "Attribute Name";

                BoundField bf4 = new BoundField();
                bf4.DataField = "ProductAttributeValueId";
                bf4.HeaderText = "Action";

                

                if (IsPostBack)
                {
                    cgvAttributeValues.Columns.Clear();
                }


                cgvAttributeValues.Columns.Add(bf1);
                cgvAttributeValues.Columns.Add(bf2);
                cgvAttributeValues.Columns.Add(bf3);
                cgvAttributeValues.Columns.Add(bf4);


                cgvAttributeValues.AutoGenerateColumns = false;

                var dc = new ProductService();
                cgvAttributeValues.DataSource = p.Attributes;

                cgvAttributeValues.DataBind();
            
        }

        public void loadCatGrid()
        { 
                BoundField bf1 = new BoundField();
                bf1.DataField = "Name";
                bf1.HeaderText = "Name";

                BoundField bf2 = new BoundField();
                bf2.DataField = "Description";
                bf2.HeaderText = "Description";

                BoundField bf3 = new BoundField();
                bf3.DataField = "Category_id";
                bf3.HeaderText = "Action";
  

                if (IsPostBack)
                {
                    cgvCategory.Columns.Clear();
                }


                cgvCategory.Columns.Add(bf1);
                cgvCategory.Columns.Add(bf2);
                cgvCategory.Columns.Add(bf3);


                cgvCategory.AutoGenerateColumns = false;

                var dc = new ProductService();
          
                cgvCategory.DataSource = dc.ProductCategorySelect(Convert.ToInt32(commonClasses.GetId()));

                cgvCategory.DataBind();
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("productAttributeValue.aspx" + EncryptQueryString.Get("id=0&newRec=true&p_order_id=" + commonClasses.GetId()));
        
        }

        protected void btnDeletemProductscart_Click(object sender, EventArgs e)
        {
             
        }

        protected void btnCat_Click(object sender, EventArgs e)
        {
            var dc = new ProductService();
            dc.AddProductToCategory(commonClasses.GetId(), Convert.ToInt32(ddlCategory.SelectedValue.ToString()));
            ReloadPage();
        }

       

         
    }
}