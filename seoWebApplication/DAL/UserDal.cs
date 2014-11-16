using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Configuration; 
using seoWebApplication.Models;
using seoWebApplication.Framework;

namespace seoWebApplication.DAL
{
    public class UserDal : IDisposable
    {
        private MongoServer mongoServer = null;
        private bool disposed = false;

        static string connectionString = "mongodb://admin:Spz123456@kahana.mongohq.com:10001/shoppersparadise";
        MongoUrl url = new MongoUrl(connectionString);

        private string dbName = "shoppersparadise";
        private string collectionName = "User";

        // Default constructor.        
        public UserDal()
        {
        }

        public List<User> GetAll()
        {
            try
            {
                MongoCollection<User> collection = getCollectionForEdit();
                return collection.FindAll().ToList<User>();
            }
            catch (MongoConnectionException)
            {
                return new List<User>();
            }
        }

        public User GetByUserName(string UserName)
        {
            try
            {
                MongoCollection<User> collection = getCollectionForEdit();
                var query =
                (from e in collection.AsQueryable<User>()
                where e.UserName == UserName
                select e).First();

                return query;
            }
            catch (MongoConnectionException)
            {
                return new User();
            }
        }

        // Creates a Note and inserts it into the collection in MongoDB.
        public void Create(User User)
        {
            MongoCollection<User> collection = getCollectionForEdit();
            try
            {
                User.Password = phasher.Hash(User.Password);
                collection.Insert(User);
            }
            catch (MongoCommandException ex)
            {
                string msg = ex.Message;
            }
        }

        private MongoCollection<User> getCollectionForEdit()
        {
            MongoClient client = new MongoClient(url);
            mongoServer = client.GetServer();
            MongoDatabase database = mongoServer.GetDatabase(dbName);
            MongoCollection<User> _Collection = database.GetCollection<User>(collectionName);
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