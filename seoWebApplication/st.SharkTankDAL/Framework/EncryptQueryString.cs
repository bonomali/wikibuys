using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.st.SharkTankDAL.Framework
{
    public static class EncryptQueryString
    {
        public static string Get(string queryString)
        {
            return "?" + HttpUtility.UrlEncode(StringHelpers.Encrypt(queryString));
        }
    }
}