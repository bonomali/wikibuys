using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using seoWebApplication.st.SharkTankDAL;
using seoWebApplication.st.SharkTankDAL.entObject;
using seoWebApplication.st.SharkTankDAL.dataObject;
using seoWebApplication.st.SharkTankDAL.Framework;
using System.Data.SqlClient;
using seoWebApplication.Service;
using seoWebApplication.Models;

namespace seoWebApplication.admin.catalog
{
    public partial class departments : BasePage
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
                    bf1.DataField = "department_id";
                    bf1.HeaderText = "department_id";



                    cgvDepartments.Columns.Add(bf2);
                    cgvDepartments.Columns.Add(bf3); 
                    cgvDepartments.Columns.Add(bf1);


                    cgvDepartments.AutoGenerateColumns = false;
                    var dc = new DepartmentService();
                    cgvDepartments.DataSource = dc.GetDepartments();

                    cgvDepartments.DataBind();
                
            }

        }

        void BindGridDynamic(string tblName, string search)
        {
           
                var dc = new DepartmentService();
                var query = from d in dc.GetDepartments() 
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



                cgvDepartments.Columns.Add(bf2);
                cgvDepartments.Columns.Add(bf3); 
                cgvDepartments.Columns.Add(bf1);


                cgvDepartments.AutoGenerateColumns = false;
                cgvDepartments.DataSource = query;
                cgvDepartments.DataBind();
          
        }

        void BindDDlFilter()
        {
            getSchema gt = new getSchema();
            DataTable dt = gt.GetSchema(dBHelper.GetSeoWebAppConnectionString(), "vw_department");
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

            Response.Redirect("department.aspx" + EncryptQueryString("id=0&newRec=true"));

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

        protected void cgvDepartments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string dId;
                Departments obj = (Departments)(e.Row.DataItem);
                dId = obj.department_id.ToString();




                //Add the edit link to the action column.
                HyperLink editLink = new HyperLink();

                editLink.Text = "Edit";

                editLink.NavigateUrl = "department.aspx" + EncryptQueryString("id=" + (dId));

                e.Row.Cells[2].Controls.Add(editLink);
                 
            }
        }
    }
}