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
    public class CategoriesService
    {
         private readonly MongoHelper<Categories> _categories;
         public CategoriesService()
        {
            _categories = new MongoHelper<Categories>();
        }

         public void Create(Categories categories)
        {
            categories.category_id = GetLastId();
            _categories.Collection.Insert(categories);
        }

         public bool Delete(int id)
         {
             try
             {
                 var query = Query<Categories>.EQ(e => e.category_id, id);
                 _categories.Collection.Remove(query);
                 return true;
             }
             catch
             {
                 return false;
             }
         }


         public IList<Categories> GetCategories()
        {
            try
            { 
                int Id = dBHelper.GetWebstoreId();
                return _categories.Collection.Find(Query.EQ("webstore_id", Id)).ToList<Categories>();
            }
            catch (MongoConnectionException)
            {
                return new List<Categories>();
            }
        }

         public IList<Categories> GetCategoriesById(int Id)
         {
             try
             {
                 return _categories.Collection.Find(Query.EQ("department_id", Id)).ToList<Categories>();
                  
             }
             catch (MongoConnectionException)
             {
                 return new List<Categories>();
             }
         }

         public Categories GetCategoryById(int Id)
         {
             try
             {
                 if (Id > 0)
                 {
                     var query = Query.And(
                                     Query<Categories>.EQ(e => e.category_id, Id),
                                     Query<Categories>.EQ(e => e.webstore_id, dBHelper.GetWebstoreId())
                                 );
                     var list = _categories.Collection.Find(query).First<Categories>();
                     return list;
                 }
                 else
                 {
                     return null;
                 }

             }
             catch { return null; }
         }

         public Categories GetCategories(string name)
        {
            var post = _categories.Collection.Find(Query.EQ("Name", name)).Single();  
            return post;
        }

         public int GetLastId()
         {
             try
             {
                 var query = (from d in GetCategories() orderby d.category_id ascending select d).First();

                 return query.category_id + 1;
             }
             catch
             {
                 return 1;
             }
         }
    }
}