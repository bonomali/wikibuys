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
using seoWebApplication.st.SharkTankDAL.dataObject;
using seoWebApplication.st.SharkTankDAL.Framework;  
using System.IO;
using seoWebApplication.Service;
using seoWebApplication.Models;
 

namespace seoWebApplication.UserControls
{
    public partial class CategoriesList : System.Web.UI.UserControl
    {
        private CategoriesService _categoriesService = new CategoriesService();
        protected void Page_Load(object sender, EventArgs e)
        {
            
                // Obtain the ID of the selected department
                string department_id = Request.QueryString["department_id"];
                
                // Continue only if department_id exists in the query string
                if (department_id != null)
                {
                    try
                    {
                            list.DataSource = _categoriesService.GetCategoriesById(Convert.ToInt32(department_id));
                            list.DataBind(); 
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Exception Occured:   " + ex);
                    }

                     
            }
 
        }

        protected void R1_ItemCreated(Object Sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                Categories obj = (Categories)(e.Item.DataItem);
                string department_id = Request.QueryString["department_id"];
                string category_id = Request.QueryString["category_id"];
                HyperLink catHyper = (HyperLink)e.Item.FindControl("catHyperLink");

                catHyper.CssClass = "class_menuitem_categorytype";
                catHyper.NavigateUrl = LinkMaker.ToCategory(department_id.ToString(), obj.category_id.ToString());
                catHyper.Text = HttpUtility.HtmlEncode(obj.Name.ToString());
                catHyper.ToolTip = HttpUtility.HtmlEncode(obj.Description.ToString());

                var liCatView = e.Item.FindControl("liCatView") as HtmlGenericControl;

                if (obj.category_id == Convert.ToInt32(category_id))
                {
                    if (liCatView != null)
                    {
                        liCatView.Attributes["class"] = "active";
                    }
                }
 
            }
        } 
    }
}