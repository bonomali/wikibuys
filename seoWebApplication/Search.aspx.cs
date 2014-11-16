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

namespace seoWebApplication
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // fill the table contents
            string searchString = Request.QueryString["Search"];
            this.catalogTitleLabel.Text = "Product Search";
            this.catalogDescriptionLabel.Text = "You searched for \"" + searchString + "\"";
            // set the title of the page
            this.Title = seoWebAppConfiguration.SiteName +
            " : Product Search : " + searchString;
            // don't reload data during postbacks
            if (!IsPostBack)
            {
                try
                {
                    PopulateControls();
                }
                catch (Exception ex)
                {
                    Response.Write("Exception Occured:   " + ex);
                }
            }
        }

        // Fill the page with data
        private void PopulateControls()
        {
            // Retrieve department_id from the query string
            string department_id = Request.QueryString["department_id"];
            // Retrieve category_id from the query string
            string category_id = Request.QueryString["category_id"];
            // Retrieve Page from the query string
            string page = Request.QueryString["Page"];
            // Retrieve Search string from query string
            string searchString = Request.QueryString["Search"];
             
            if (page == null) page = "1";
            // How many pages of products?
            int howManyPages = 1;
            // pager links format
            string firstPageUrl = "";
            string pagerFormat = "";
            // If performing a product search
            if (searchString != null)
            {
            // Retrieve AllWords from query string
            string allWords = Request.QueryString["AllWords"];
            // Perform search
            list.DataSource = catalogAccesor.Search(searchString, allWords, page, out howManyPages);
            list.DataBind();
            // Display pager
            firstPageUrl = Linkor.ToSearch(searchString, allWords.ToUpper() == "TRUE", "1");
            pagerFormat = Linkor.ToSearch(searchString, allWords.ToUpper() == "TRUE", "{0}");
            }
            // If browsing a category...
            else if (category_id != null)
            {



                // department data, which is read in the ItemTemplate of the DataList
                // Retrieve list of products in a category

                list.DataSource = catalogAccesor.GetProductsInCategory(category_id, page, out howManyPages);
                list.DataBind();
                // get first page url and pager format
                firstPageUrl = Linkor.ToCategory(department_id, category_id, "1");
                pagerFormat = Linkor.ToCategory(department_id, category_id, "{0}");

                using (var dc = new seowebappDataContext())
                {




                    CatalogGetcategoryDetailsResult cd = dc.CatalogGetcategoryDetails(Convert.ToInt32(category_id)).SingleOrDefault();
                    catalogTitleLabel.Text = HttpUtility.HtmlEncode(cd.name);

                    CatalogGetdepartmentDetailsResult dd = dc.CatalogGetdepartmentDetails(Convert.ToInt32(department_id)).SingleOrDefault();
                    catalogDescriptionLabel.Text = HttpUtility.HtmlEncode(cd.description);

                    this.Title = HttpUtility.HtmlEncode(seoWebAppConfiguration.SiteName +
           ": " + dd.name + ": " + cd.name);

                }
                // Retrieve category and department details and display them
                //CategoryDetails cd = CatalogAccess.GetCategoryDetails(category_id);
                //catalogTitleLabel.Text = HttpUtility.HtmlEncode(cd.Name);
                //DepartmentDetails dd = CatalogAccess.GetDepartmentDetails(department_id);
                //catalogDescriptionLabel.Text = HttpUtility.HtmlEncode(cd.Description);
                // Set the title of the page

            }
            // If browsing a department...
            else if (department_id != null)
            {
                // Retrieve department details and display them

                // Retrieve list of products on department promotion             
                list.DataSource = catalogAccesor.GetProductsOnDeptPromo(department_id, page, out howManyPages);
                list.DataBind();
                // get first page url and pager format
                firstPageUrl = Linkor.ToDepartment(department_id, "1");
                pagerFormat = Linkor.ToDepartment(department_id, "{0}");
                using (var dc = new seowebappDataContext())
                {
                    CatalogGetdepartmentDetailsResult dd = dc.CatalogGetdepartmentDetails(Convert.ToInt32(department_id)).SingleOrDefault();
                    catalogDescriptionLabel.Text = HttpUtility.HtmlEncode(dd.description);
                    catalogTitleLabel.Text = HttpUtility.HtmlEncode(dd.name);
                    this.Title = HttpUtility.HtmlEncode(seoWebAppConfiguration.SiteName +
            ": " + dd.name);

                }
                //DepartmentDetails dd =
                //CatalogAccess.GetDepartmentDetails(department_id);
                //catalogTitleLabel.Text = HttpUtility.HtmlEncode(dd.Name);
                //catalogDescriptionLabel.Text =
                //HttpUtility.HtmlEncode(dd.Description);
                // Set the title of the page

            }
            else
            {
                // Retrieve list of products on department promotion             
                list.DataSource = catalogAccesor.GetProductsOnFrontPromo(page, out howManyPages);
                list.DataBind();

                // have the current page as integer
                int currentPage = Int32.Parse(page);

            }

            // Display pager controls 
            Pager2.Show(int.Parse(page), howManyPages, firstPageUrl, pagerFormat, true);

        }

        

        protected void R1_ItemCreated(Object Sender, RepeaterItemEventArgs e)
        {
            int i = 0;
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (e.Item.ItemIndex % 3 == 0)
                {
                    Literal lblDivStart = (Literal)e.Item.FindControl("lblDivStart");
                    Literal lblDivEnd = (Literal)e.Item.FindControl("lblDivEnd");

                    lblDivStart.Text = "<div class=row work-row'>";
                    i++;
                    if (i == 3)
                    {
                        lblDivEnd.Text = "</div>";
                        i = 0;
                    }
                }

            }
        }
    }
}
