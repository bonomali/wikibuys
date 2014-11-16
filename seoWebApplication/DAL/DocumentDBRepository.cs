using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using seoWebApplication.DocumentDbModels;
using seoWebApplication.DocumentDbServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace seoWebApplication.DAL
{
    public class DocumentDBRepository
    {
        private static string databaseId;
        private static String DatabaseId
        {
            get
            {
                if (string.IsNullOrEmpty(databaseId))
                {

                    databaseId = ConfigurationManager.AppSettings["DatabaseId"];
                }

                return databaseId;
            }
        }

        private static string collectionId;
        private static String CollectionId
        {
            get
            {
                if (string.IsNullOrEmpty(collectionId))
                {

                    collectionId = ConfigurationManager.AppSettings["collection"];
                }

                return collectionId;
            }
        }

        private static Database database;
        private static Database Database
        {
            get
            {
                if (database == null)
                {

                    database = ReadOrCreateDatabase();
                }

                return database;
            }
        }

        private static DocumentCollection collection;
        private static DocumentCollection Collection
        {
            get
            {
                if (collection == null)
                {
                    collection = ReadOrCreateCollection(Database.SelfLink);
                }

                return collection;
            }
        }

        private static DocumentClient client;
        private static DocumentClient Client
        {
            get
            {
                if (client == null)
                {
                    string endpoint = ConfigurationManager.AppSettings["EndpointURI"];
                    string authKey = ConfigurationManager.AppSettings["AuthorizationKey"];
                    Uri endpointUri = new Uri(endpoint);
                    client = new DocumentClient(endpointUri, authKey);
                }

                return client;
            }
        }

        private static DocumentCollection ReadOrCreateCollection(string databaseLink)
        {
            var col = Client.CreateDocumentCollectionQuery(databaseLink)
                              .Where(c => c.Id == CollectionId)
                              .AsEnumerable()
                              .FirstOrDefault();

            if (col == null)
            {
                col = Client.CreateDocumentCollectionAsync(databaseLink, new DocumentCollection { Id = CollectionId }).Result;
            }

            return col;
        }

        private static Database ReadOrCreateDatabase()
        {
            var db = Client.CreateDatabaseQuery()
                            .Where(d => d.Id == DatabaseId)
                            .AsEnumerable()
                            .FirstOrDefault();

            if (db == null)
            {
                db = Client.CreateDatabaseAsync(new Database { Id = DatabaseId }).Result;
            }

            return db;
        }

        public static List<Item> GetIncompleteItems()
        {
            return Client.CreateDocumentQuery<Item>(Collection.DocumentsLink)
                       .Where(d => !d.Completed)
                       .AsEnumerable()
                       .ToList<Item>();
        }

        public static async Task<Document> CreateItemAsync(Item item)
        {
            return await Client.CreateDocumentAsync(Collection.SelfLink, item);
        }

        public static Item GetItem(string id)
        {
            return Client.CreateDocumentQuery<Item>(Collection.DocumentsLink)
                        .Where(d => d.ID == id)
                        .AsEnumerable()
                        .FirstOrDefault();
        }

        public static async Task<Document> UpdateItemAsync(Item item)
        {
            Document doc = Client.CreateDocumentQuery(Collection.DocumentsLink)
                                .Where(d => d.Id == item.ID)
                                .AsEnumerable()
                                .FirstOrDefault();

            return await Client.ReplaceDocumentAsync(doc.SelfLink, item);
        }
    }
}