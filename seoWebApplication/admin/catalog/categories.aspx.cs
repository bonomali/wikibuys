using System; 
using System.Data;
using System.Linq; 
using System.Web.UI.WebControls; 
using seoWebApplication.st.SharkTankDAL; 
using seoWebApplication.st.SharkTankDAL.Framework; 
using seoWebApplication.Service;
using seoWebApplication.Models;

namespace seoWebApplication.admin.catalog
{
    public partial class categories : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.AddButton_Click += new seoWebAppAdminEditGrid.ButtonClickedHandler(Master_AddButton_Click);
            Master.AddSearch_Click += new seoWebAppAdminEditGrid.ButtonClickedHandler(Master_AddSearchButton_Click);
            Master.ddlClickEvent_Click += new seoWebAppAdminEditGrid.SelectedIndexChanged(Master_AddSearchDdl_Click);
            if (!IsPostBack)
            {
                BindDDlFilter();
                 
                    BoundField bf2 = new BoundField();
                    bf2.DataField = "name";
                    bf2.HeaderText = "name";

                    BoundField bf3 = new BoundField();
                    bf3.DataField = "description";
                    bf3.HeaderText = "description";

                    BoundField bf1 = new BoundField();
                    bf1.DataField = "category_id";
                    bf1.HeaderText = "category_id";



                    cgvCategories.Columns.Add(bf2);
                    cgvCategories.Columns.Add(bf3);
                    cgvCategories.Columns.Add(bf1);


                    cgvCategories.AutoGenerateColumns = false;

                    var dc = new CategoriesService();
                    cgvCategories.DataSource = dc.GetCategories();

                    cgvCategories.DataBind();
                
            }

        }

        void BindGridDynamic(string tblName, string search)
        {

                var dc = new DepartmentService();
                var query = from d in dc.GetDepartments()
                            where d.webstore_id == dBHelper.GetWebstoreId()
                            orderby d.Name
                            select d;
                BoundField bf2 = new BoundField();
                bf2.DataField = "Name";
                bf2.HeaderText = "Name";

                BoundField bf3 = new BoundField();
                bf3.DataField = "Description";
                bf3.HeaderText = "Description";


                BoundField bf1 = new BoundField();
                bf1.DataField = "department_id";
                bf1.HeaderText = "department_id";



                cgvCategories.Columns.Add(bf2);
                cgvCategories.Columns.Add(bf3);
                cgvCategories.Columns.Add(bf1);


                cgvCategories.AutoGenerateColumns = false;
                cgvCategories.DataSource = query;
                cgvCategories.DataBind();
            
        }

        void BindDDlFilter()
        {
            getSchema gt = new getSchema();
            DataTable dt = gt.GetSchema(dBHelper.GetSeoWebAppConnectionString(), "vw_category");
            Master.SearchList.DataSource = dt;
            Master.SearchList.DataTextField = "ColumnName";
            Master.SearchList.DataValueField = "ColumnName";
            Master.SearchList.DataBind();

        }

        void Master_AddSearchDdl_Click(object sender, EventArgs e)
        {
            string name = Master.change_ddlArea;
            if (name == "name")
            {
                Master.displayBox = "textbox";
            }
        }

        void Master_AddButton_Click(object sender, EventArgs e)
        {

            Response.Redirect("category.aspx" + EncryptQueryString("id=0&newRec=true"));

        }

        void Master_AddSearchButton_Click(object sender, EventArgs e)
        {
            string search = Master.searchBox;
            string tblName = Master.change_ddlArea;
            BindGridDynamic(tblName, search);

        }

        public override string MenuItemName()
        {
            return "Department";
        }

        protected void cgvCategories_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string cId;
                Categories obj = (Categories)(e.Row.DataItem);
                cId = obj.category_id.ToString();




                //Add the edit link to the action column.
                HyperLink editLink = new HyperLink();

                editLink.Text = "Edit";

                editLink.NavigateUrl = "category.aspx" + EncryptQueryString("id=" + (cId));

                e.Row.Cells[2].Controls.Add(editLink);

            }
        }
    }
}