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

namespace seoWebApplication.DocumentDbServices
{
    public class DocumentDBFactory
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

        public static Database ReadOrCreateDatabase()
        {
            var db = DocumentDBFactory.Client.CreateDatabaseQuery()
                            .Where(d => d.Id == DatabaseId)
                            .AsEnumerable()
                            .FirstOrDefault();

            if (db == null)
            {
                db = DocumentDBFactory.Client.CreateDatabaseAsync(new Database { Id = DatabaseId }).Result;
            }

            return db;
        }

        //private static DocumentCollection collection;
        //public static DocumentCollection Collection
        //{
        //    get
        //    {
        //        if (collection == null)
        //        {
        //            collection = ReadOrCreateCollection(ReadOrCreateDatabase().SelfLink,);
        //        }

        //        return collection;
        //    }
        //}

        public static DocumentCollection ReadOrCreateCollection(string databaseLink, string collectionName)
        {
            var col = DocumentDBFactory.Client.CreateDocumentCollectionQuery(databaseLink)
                              .Where(c => c.Id == collectionName)
                              .AsEnumerable()
                              .FirstOrDefault();

            if (col == null)
            {
                col = DocumentDBFactory.Client.CreateDocumentCollectionAsync(databaseLink, new DocumentCollection { Id = CollectionId }).Result;
            }

            return col;
        }

        public static DocumentClient client;
        public static DocumentClient Client
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
    }
}