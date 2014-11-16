using System; 
using System.Linq; 
using System.Web.UI.WebControls; 
using System.Collections.Specialized;
using seoWebApplication.Service;

namespace seoWebApplication.st.SharkTankDAL
{
    public class commonClasses 
    {
        enum ControlType
        {
            Checkbox,
            Select,
            Radio,
            Textbox
        };

        public static int GetId()
        {
            //Decrypt the query string
            
            NameValueCollection queryString = DecryptQueryString(System.Web.HttpContext.Current.Request.QueryString.ToString());

            if (queryString == null)
            {
                return 0;
            }
            else
            {
                //Check if the id was passed in.
                string id = queryString["id"];

                if ((id == null) || (id == "0"))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(id);
                }
            }
        }

        public bool GetNewPage()
        {
            //Decrypt the query string
            NameValueCollection queryString = DecryptQueryString(System.Web.HttpContext.Current.Request.QueryString.ToString());

            if (queryString == null)
            {
                return false;
            }
            else
            {
                //Check if the id was passed in.
                string value = queryString["newRec"];

                if ((value == null) || (value == "0"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static int GetP_order_id()
        {
            //Decrypt the query string
            NameValueCollection queryString = DecryptQueryString(System.Web.HttpContext.Current.Request.QueryString.ToString());

            if (queryString == null)
            {
                return 0;
            }
            else
            {
                //Check if the id was passed in.
                string value = queryString["p_order_id"];

                if ((value == null) || (value == "0"))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(value);
                }
            }
        }

        public static NameValueCollection DecryptQueryString(string queryString)
        {
            return StringHelpers.DecryptQueryString(queryString);
        }

        public static string EncryptQueryString(NameValueCollection queryString)
        {
            return StringHelpers.EncryptQueryString(queryString);
        }

        public static void LoadDdlDept(DropDownList ddl, int Id)
        {
            var dc = new DepartmentService();
            ddl.DataSource = dc.GetDepartments();
                ddl.DataTextField = "Name";
                ddl.DataValueField = "Department_id";
                ddl.DataBind();
                if(Id>0)
                ddl.SelectedValue = Id.ToString();
            
        }

        public static void LoadDdlCategory(DropDownList ddl, int Id)
        {

                var dc = new CategoriesService();
                ddl.DataSource = dc.GetCategories();
                ddl.DataTextField = "Name";
                ddl.DataValueField = "Category_id";
                ddl.DataBind();
                if (Id > 0)
                {
                    ddl.SelectedValue = Id.ToString();
                }
            
        }

        public static void LoadDdlAttributes(DropDownList ddl, string Id)
        {

            Array itemValues = System.Enum.GetValues(typeof(ControlType));
            Array itemNames = System.Enum.GetNames(typeof(ControlType));

            foreach (var value in itemValues)
            {
                ListItem item = new ListItem(value.ToString(), value.ToString());
                ddl.Items.Add(item);
            }

            ddl.DataBind(); 
            if (Id.Length > 0)
            {
                ddl.SelectedValue = Id.ToString();
            }

        }

        public static void LoadAttributes(DropDownList ddl)
        {
            var dc = new AttributeService();
            ddl.DataSource = dc.GetAttributes();
            ddl.DataTextField = "Name";
            ddl.DataValueField = "AttributeID";
            ddl.DataBind(); 

        }

        internal static void LoadDdlAttributeValue(DropDownList ddl, int id)
        { 
            var dc = new AttributeValueService();
            ddl.DataSource = dc.GetAttributesById(id);
            ddl.DataTextField = "Value";
            ddl.DataValueField = "AttributeValueID";
            ddl.DataBind(); 
        }

        internal static void LoadDdlAttributeValue(DropDownList ddl, int p1, int p2)
        {
            var dc = new AttributeValueService();
            ddl.DataSource = dc.GetAttributesById(p1);
            ddl.DataTextField = "Value";
            ddl.DataValueField = "AttributeValueID";
            ddl.DataBind();
            ddl.SelectedValue = p2.ToString();
            
        }
    }
}