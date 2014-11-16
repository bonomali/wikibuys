using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using MongoDB.Driver;
using System.Configuration;
using seoWebApplication.Models;

namespace seoWebApplication.DAL
{
    public class SubscriberDal : IDisposable
    {
        private MongoServer mongoServer = null;
        private bool disposed = false;

        static string connectionString = "mongodb://admin:Spz123456@kahana.mongohq.com:10001/shoppersparadise";
        MongoUrl url = new MongoUrl(connectionString);

        private string dbName = "shoppersparadise";
        private string collectionName = "Subscriber";

        // Default constructor.        
        public SubscriberDal()
        {
        }

        public List<Subscriber> GetAll()
        {
            try
            {
                MongoCollection<Subscriber> collection = getCollectionForEdit();
                return collection.FindAll().ToList<Subscriber>();
            }
            catch (MongoConnectionException)
            {
                return new List<Subscriber>();
            }
        }

        // Creates a Note and inserts it into the collection in MongoDB.
        public void Create(Subscriber Subscriber)
        {
            MongoCollection<Subscriber> collection = getCollectionForEdit();
            try
            {
                collection.Insert(Subscriber);
            }
            catch (MongoCommandException ex)
            {
                string msg = ex.Message;
            }
        }

        private MongoCollection<Subscriber> getCollectionForEdit()
        {
            MongoClient client = new MongoClient(url);
            mongoServer = client.GetServer();
            MongoDatabase database = mongoServer.GetDatabase(dbName);
            MongoCollection<Subscriber> _Collection = database.GetCollection<Subscriber>(collectionName);
            return _Collection;
        }

        # region IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (mongoServer != null)
                    {
                        this.mongoServer.Disconnect();
                    }
                }
            }

            this.disposed = true;
        }

        # endregion

        internal void CreateNote(NoteDal note)
        {
            throw new NotImplementedException();
        }
    }
}