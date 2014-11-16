using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using MongoDB.Driver;
using System.Configuration;
using seoWebApplication.Models;

namespace seoWebApplication.DAL
{
    public class NoteDal : IDisposable
    {
        private MongoServer mongoServer = null;
        private bool disposed = false;

        static string connectionString = "mongodb://admin:Spz123456@kahana.mongohq.com:10001/shoppersparadise";
        MongoUrl url = new MongoUrl(connectionString);

        private string dbName = "shoppersparadise";
        private string collectionName = "Notes";

        // Default constructor.        
        public NoteDal()
        {
        }

        public List<Note> GetAllNotes()
        {
            try
            {
                MongoCollection<Note> collection = GetNotesCollection();
                return collection.FindAll().ToList<Note>();
            }
            catch (MongoConnectionException)
            {
                return new List<Note>();
            }
        }

        // Creates a Note and inserts it into the collection in MongoDB.
        public void CreateNote(Note note)
        {
            MongoCollection<Note> collection = getNotesCollectionForEdit();
            try
            {
                collection.Insert(note);
            }
            catch (MongoCommandException ex)
            {
                string msg = ex.Message;
            }
        }

        private MongoCollection<Note> GetNotesCollection()
        {
            MongoClient client = new MongoClient(url);
            mongoServer = client.GetServer();
            MongoDatabase database = mongoServer.GetDatabase(dbName);
            MongoCollection<Note> noteCollection = database.GetCollection<Note>(collectionName);
            return noteCollection;
        }

        private MongoCollection<Note> getNotesCollectionForEdit()
        {
            MongoClient client = new MongoClient(url);
            mongoServer = client.GetServer();
            MongoDatabase database = mongoServer.GetDatabase(dbName);
            MongoCollection<Note> notesCollection = database.GetCollection<Note>(collectionName);
            return notesCollection;
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