using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seoWebApplication.Framework.HtmlHelpers
{
    public static class SeoHtmlHelper
    {
        public static string GetPhone()
        {
            return String.Format(seoWebAppConfiguration.StorePhone);
        }


        public static string GetEmail()
        {
            return String.Format(seoWebAppConfiguration.PaypalEmail);
        }
    }
   
}
