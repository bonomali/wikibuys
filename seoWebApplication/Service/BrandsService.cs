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
    public class BrandsService
    {
         private readonly MongoHelper<Brands> _brands;
         public BrandsService()
        {
            _brands = new MongoHelper<Brands>();
        }

         public void Create(Brands Brands)
        {
            Brands.brand_id = GetLastId();
            Brands.webstore_id = dBHelper.GetWebstoreId();
            Brands.Name = Helpers.GenerateSlug(Brands.Name.ToLower());
            _brands.Collection.Insert(Brands);
        }

         internal void Update(Brands p)
         {
             string name = Helpers.GenerateSlug(p.Name.ToLower());
             var query = Query<Brands>.EQ(e => e.Id, p.Id);
             var update = Update<Brands>.Set(e => e.Name, name)
                                        .Set(e => e.Description, p.Description);

             _brands.Collection.Update(query, update);
         }
         public bool Delete(Guid id)
         {
             try
             {
                 var query = Query<Brands>.EQ(e => e.Id, id);
                 _brands.Collection.Remove(query);
                 return true;
             }
             catch
             {
                 return false;
             }
         }


         public IList<Brands> GetBrands()
        {
            try
            { 
                int Id = dBHelper.GetWebstoreId();
                return _brands.Collection.Find(Query.EQ("webstore_id", Id)).ToList<Brands>();
            }
            catch (MongoConnectionException)
            {
                return new List<Brands>();
            }
        }

         public IList<Brands> GetBrandsById(int Id)
         {
             try
             {
                 return _brands.Collection.Find(Query.EQ("brand_id", Id)).ToList<Brands>();
                  
             }
             catch (MongoConnectionException)
             {
                 return new List<Brands>();
             }
         }
         public Brands GetBrandByName(string name)
         {
             try
             { 
                     var query = Query<Brands>.EQ(e => e.Name, name);
                     var list = _brands.Collection.Find(query).First<Brands>();
                     return list;
                
             }
             catch { return null; }
         }
         public Brands GetBrandById(int Id)
         {
             try
             {
                 if (Id > 0)
                 {
                     var query = Query.And(
                                     Query<Brands>.EQ(e => e.brand_id, Id),
                                     Query<Brands>.EQ(e => e.webstore_id, dBHelper.GetWebstoreId())
                                 );
                     var list = _brands.Collection.Find(query).First<Brands>();
                     return list;
                 }
                 else
                 {
                     return null;
                 }

             }
             catch { return null; }
         }

         public Brands GetBrands(string name)
        {
            var post = _brands.Collection.Find(Query.EQ("Name", name)).Single();  
            return post;
        }

        
         public int GetLastId()
         {
             try
             {
                 var query = (from d in GetBrands() orderby d.brand_id ascending select d).First();

                 return query.brand_id + 1;
             }
             catch
             {
                 return 1;
             }
         }

         internal Brands GetBrandByGuid(Guid id)
         {
             try
             {
                 if (id != null)
                 {
                     var query = Query<Brands>.EQ(e => e.Id, id);
                     var list = _brands.Collection.Find(query).First<Brands>();
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