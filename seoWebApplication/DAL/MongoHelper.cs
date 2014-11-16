using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace seoWebApplication.DAL
{
    public class MongoHelper<T> where T : class
    {
        private MongoServer mongoServer = null;
        private bool disposed = false;

        static string connectionString = ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;
        MongoUrl con = new MongoUrl(connectionString);

        private string dbName = ConfigurationManager.AppSettings["DatabaseId"];

        public MongoCollection<T> Collection { get; private set; }

        public MongoHelper()
        { 
            MongoClient client = new MongoClient(con);
            mongoServer = client.GetServer();
            MongoDatabase database = mongoServer.GetDatabase(dbName);
            Collection = database.GetCollection<T>(typeof(T).Name.ToLower());
        }

         
    }
}