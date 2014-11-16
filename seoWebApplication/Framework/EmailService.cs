using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Configuration;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace seoWebApplication.Framework
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {  
            emailSend.send(message.Destination, message.Subject, message.Body);

            return Task.FromResult(0);
        }
 
    }
}