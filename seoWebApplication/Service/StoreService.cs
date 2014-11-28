using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using seoWebApplication.DAL;
using seoWebApplication.Models; 
using System;
using System.Collections.Generic;
using System.Linq; 

namespace seoWebApplication.Service
{
    public class StoreService
    {

        private readonly MongoHelper<mStores> _store;
        public StoreService()
        {
            _store = new MongoHelper<mStores>();
        }

        public void Create(mStores store)
        { 
            store.InsertDate = DateTime.Now;
            store.InsertENTUserAccountId = 1;
            _store.Collection.Insert(store);
        }

        public IEnumerable<mStores> Getstores(int limit, int skip)
        {
            var storesCursor = _store.Collection.FindAllAs<mStores>()
                .SetSortOrder(SortBy<mStores>.Ascending(p => p.name))
                .SetLimit(limit)
                .SetSkip(skip)
                .SetFields(Fields<mStores>.Include(p => p.Id, p => p.name));
            return storesCursor;
        }
 


        public IList<mStores> Getstores()
        {
            try
            { 
                return _store.Collection.FindAll().ToList<mStores>();
            }
            catch (MongoConnectionException)
            {
                return new List<mStores>();
            }
        }

        public IList<mStores> GetstoresByDepartment(int Id)
        {
            try
            {
                return _store.Collection.Find(Query.EQ("department_id", Id)).ToList<mStores>();

            }
            catch (MongoConnectionException)
            {
                return new List<mStores>();//
            }
        }


 

        public IList<mStores> GetstoresByCategory(int Id)
        {
            try
            { 
                return _store.Collection.Find(Query.EQ("category_id", Id)).ToList<mStores>();

            }
            catch (MongoConnectionException)
            {
                return new List<mStores>();
            }
        }

        public mStores Getstores(string name)
        {
            var post = _store.Collection.Find(Query.EQ("name", name)).Single();
            return post;
        }

        public mStores Getstore(Guid Id)
        { 
            seoWebApplication.Models.mStores query = (from e in _store.Collection.AsQueryable<mStores>()
                                                      where e.Id == Id
                                                       select e).First();

            return query;
        } 


        internal void StoreThumbnailUpdate(Guid id, string fileName)
        {
            try
            {
                var query = Query.And(
                                        Query<mStores>.EQ(e => e.Id, id)  
                                    );

                var update = Update<mStores>.Set(e => e.thumbnail, fileName);

                _store.Collection.Update(query, update); 
            }
            catch
            { 
            } 
        }

        internal void StoreImageUpdate(Guid id, string fileName)
        {
            try
            {
                var query = Query.And(
                                       Query<mStores>.EQ(e => e.Id, id)
                                   );

                var update = Update<mStores>.Set(e => e.image, fileName);

                _store.Collection.Update(query, update);
            }
            catch
            {
            }
        }
          
       
        

           

        internal void Update(mStores p)
        {  
            var query = Query<mStores>.EQ(e => e.Id, p.Id); 
            var update = Update<mStores>.Set(e => e.name, p.name) 
                                           .Set(e => e.IsActive, p.IsActive)
                                           .Set(e => e.image, p.image)
                                           .Set(e => e.thumbnail, p.thumbnail)
                                           .Set(e => e.description, p.description)
                                           .Set(e => e.AffiliateId, p.AffiliateId)
                                           .Set(e => e.Url, p.Url);

            _store.Collection.Update(query, update); 
        }

        internal void Delete(Guid id)
        {
            try
            {
                var query = Query<mStores>.EQ(e => e.Id, id);  

                _store.Collection.Remove(query);

            }
            catch
            {

            } 
        }

            
    }
}