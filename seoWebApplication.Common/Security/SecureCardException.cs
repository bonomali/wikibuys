using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
//using System.Web.Security; 
using System.Xml.Linq;

namespace seoWebApplication.Common.Security
{
    public class SecureCardException : Exception
    {
        public SecureCardException(string message)
            : base(message)
        {
        }
    }
}
