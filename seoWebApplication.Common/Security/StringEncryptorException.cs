using System;
using System.Collections.Generic;
using System.Text;

namespace seoWebApplication.Common.Security
{ 
        public class StringEncryptorException : Exception
        {
            public StringEncryptorException(string message)
                : base(message)
            {
            }
        }
     
}