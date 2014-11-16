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
    public static class CreateDocumentDb
    {
        private static DocumentClient client;
        //Assign a name for your database
        private static readonly string databaseId = ConfigurationManager.AppSettings["DatabaseId"];
        private static readonly string endpointUrl = ConfigurationManager.AppSettings["EndpointURI"];
        private static readonly string authorizationKey = ConfigurationManager.AppSettings["AuthorizationKey"];
        public static async Task CreateDb()
        {
            try
            {
                using (client = new DocumentClient(new Uri(endpointUrl), authorizationKey))
                {
                    RunAsync().Wait();
                }
            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            } 
        }

        private static async Task RunAsync()
        {
            //Try get a database
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == databaseId).AsEnumerable().FirstOrDefault();
 
            //Check if a database was returned, If not then it was not found. Then create the database
            if (database == null)
            {
                database = await client.CreateDatabaseAsync(new Database { Id = databaseId });
                Console.WriteLine("Created Database: id - {0} and selfLink - {1}", database.Id, database.SelfLink);
            } 
        }

        
      
    }
}