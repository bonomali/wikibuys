using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents;
using System.Configuration;
using System;

namespace seoWebApplication.DAL
{
    public class CreateDocumentDbConn 
    { 
        //Assign a name for your database
        private static readonly string databaseId = ConfigurationManager.AppSettings["DatabaseId"];
        private static readonly string endpointUrl = ConfigurationManager.AppSettings["EndpointURI"];
        private static readonly string authorizationKey = ConfigurationManager.AppSettings["AuthorizationKey"];
        
        public static DocumentClient CreateConnection() {
            return new DocumentClient(new Uri(endpointUrl), authorizationKey); 
        }
    }
}