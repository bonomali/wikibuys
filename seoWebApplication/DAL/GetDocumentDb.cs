using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace seoWebApplication.DAL
{
    public static class GetDocumentDb
    {
        private static DocumentClient client;
        //Assign a name for your database
        private static readonly string databaseId = ConfigurationManager.AppSettings["DatabaseId"];
        private static readonly string endpointUrl = ConfigurationManager.AppSettings["EndpointURI"];
        private static readonly string authorizationKey = ConfigurationManager.AppSettings["AuthorizationKey"];
        public static Database GetDb()
        {
            try
            {
                using (client = new DocumentClient(new Uri(endpointUrl), authorizationKey))
                {
                   return client.CreateDatabaseQuery().Where(db => db.Id == databaseId).AsEnumerable().FirstOrDefault();
                }
            }
            catch (DocumentClientException de)
            {
                return null; 
            }
            catch (Exception e)
            {
                return null;
            } 
        }

        

        
      
    }
}