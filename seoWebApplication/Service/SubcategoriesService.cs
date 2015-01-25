using MongoDB.Driver;
using MongoDB.Driver.Builders;
using seoWebApplication.DAL;
using seoWebApplication.Models;
using seoWebApplication.st.SharkTankDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Service
{
    public class SubcategoriesService
    {
         private readonly MongoHelper<Subcategories> _subcategories;
        public SubcategoriesService()
        {
        _subcategories = new MongoHelper<Subcategories>();
        }

         public void Create(Subcategories categories)
        {
            categories.subcategory_id = GetLastId();
            categories.webstore_id = dBHelper.GetWebstoreId();
            _subcategories.Collection.Insert(categories);
        }

         public bool Delete(Guid id)
         {
             try
             {
                 var query = Query<Subcategories>.EQ(e => e.Id, id);
                 _subcategories.Collection.Remove(query);
                 return true;
             }
             catch
             {
                 return false;
             }
         }


         public IList<Subcategories> GetSubcategories()
        {
            try
            { 
                int Id = dBHelper.GetWebstoreId();
                return _subcategories.Collection.Find(Query.EQ("webstore_id", Id)).ToList<Subcategories>();
            }
            catch (MongoConnectionException)
            {
                return new List<Subcategories>();
            }
        }

         public IList<Subcategories> GetSubcategoriesById(int Id)
         {
             try
             {
                 return _subcategories.Collection.Find(Query.EQ("department_id", Id)).ToList<Subcategories>();
                  
             }
             catch (MongoConnectionException)
             {
                 return new List<Subcategories>();
             }
         }

         public Subcategories GetSubcategoryById(int Id)
         {
             try
             {
                 if (Id > 0)
                 {
                     var query = Query.And(
                                     Query<Subcategories>.EQ(e => e.subcategory_id, Id),
                                     Query<Subcategories>.EQ(e => e.webstore_id, dBHelper.GetWebstoreId())
                                 );
                     var list = _subcategories.Collection.Find(query).First<Subcategories>();
                     return list;
                 }
                 else
                 {
                     return null;
                 }

             }
             catch { return null; }
         }

         public Subcategories GetCategories(string name)
        {
            var post = _subcategories.Collection.Find(Query.EQ("Name", name)).Single();  
            return post;
        }

         internal void Update(Subcategories p)
         {
             var query = Query<Subcategories>.EQ(e => e.subcategory_id, p.subcategory_id);
             var update = Update<Subcategories>.Set(e => e.Name, p.Name)
                                            .Set(e => e.category_id, p.category_id)
                                            .Set(e => e.Description, p.Description);

             _subcategories.Collection.Update(query, update);
         }
         public int GetLastId()
         {
             try
             {
                 var query = (from d in GetSubcategories() orderby d.subcategory_id ascending select d).First();

                 return query.subcategory_id + 1;
             }
             catch
             {
                 return 1;
             }
         }

         internal Subcategories GetCategoryByGuid(Guid id)
         {
             try
             {
                 if (id != null)
                 {
                     var query = Query<Subcategories>.EQ(e => e.Id, id);
                     var list = _subcategories.Collection.Find(query).First<Subcategories>();
                     return list;
                 }
                 else
                 {
                     return null;
                 }

             }
             catch { return null; }
         }
    }
}