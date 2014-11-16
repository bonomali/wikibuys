using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents;
using System.Configuration;
using System;

namespace seoWebApplication.DAL
{
    public class DocumentDBHelper<T> where T : class
    { 
        //Assign a name for your database
        private static readonly string databaseId = ConfigurationManager.AppSettings["DatabaseId"];
        private static readonly string endpointUrl = ConfigurationManager.AppSettings["EndpointURI"];
        private static readonly string authorizationKey = ConfigurationManager.AppSettings["AuthorizationKey"];
       
        public DocumentCollection Collection { get; private set; }

        public DocumentDBHelper()
        {
            DocumentClient client = new DocumentClient(new Uri(endpointUrl), authorizationKey);
            var collection = new DocumentCollection 
            { 
                Id =  typeof(T).Name.ToLower()
            };
            Collection = collection;
        }

        public static DocumentClient CreateConnection() {
            return new DocumentClient(new Uri(endpointUrl), authorizationKey); 
        }
    }
}