using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using seoWebApplication.DAL;
using seoWebApplication.Models;
using seoWebApplication.st.SharkTankDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace seoWebApplication.Service
{
    public class ProductService
    {
        private DepartmentService _departmentsService = new DepartmentService();
        private readonly MongoHelper<mProducts> _product;
        public ProductService()
        {
            _product = new MongoHelper<mProducts>();
        }

        public void Create(mProducts product)
        {
            product.product_id = GetLastId();
            product.webstore_id = dBHelper.GetWebstoreId();
            product.InsertDate = DateTime.Now;
            product.InsertENTUserAccountId = 1;
            _product.Collection.Insert(product);
        }

        public IEnumerable<mProducts> GetProducts(int limit, int skip)
        {
            var productsCursor = _product.Collection.FindAllAs<mProducts>()
                .SetSortOrder(SortBy<mProducts>.Ascending(p => p.name))
                .SetLimit(limit)
                .SetSkip(skip)
                .SetFields(Fields<mProducts>.Include(p => p.Id, p => p.name));
            return productsCursor;
        }
 


        public IList<mProducts> GetProducts()
        {
            try
            { 
                return _product.Collection.FindAll().ToList<mProducts>();
            }
            catch (MongoConnectionException)
            {
                return new List<mProducts>();
            }
        }

        public IList<mProducts> GetProductsByUserId(string Id)
        {
            try
            {
                return _product.Collection.Find(Query.EQ("UserKey", Id)).ToList<mProducts>();

            }
            catch (MongoConnectionException)
            {
                return new List<mProducts>();//
            }
        }

        public IList<mProducts> GetProductsByDepartment(int Id)
        {
            try
            {
                return _product.Collection.Find(Query.EQ("department_id", Id)).ToList<mProducts>();

            }
            catch (MongoConnectionException)
            {
                return new List<mProducts>();//
            }
        }



        public IList<mProducts> GetProductsOnFrontPromo()
        {
            try
            {
                var query = Query.And(
                                      Query<mProducts>.EQ(e => e.promofront, true),
                                      Query<mProducts>.EQ(e => e.webstore_id, dBHelper.GetWebstoreId())
                                  );
                return _product.Collection.Find(query).ToList<mProducts>();

            }
            catch (MongoConnectionException)
            {
                return new List<mProducts>();
            }
        }

        public IList<mProducts> GetProductsByCategory(int Id)
        {
            try
            { 
                return _product.Collection.Find(Query.EQ("category_id", Id)).ToList<mProducts>();

            }
            catch (MongoConnectionException)
            {
                return new List<mProducts>();
            }
        }

        public mProducts GetProducts(string name)
        {
            var post = _product.Collection.Find(Query.EQ("Name", name)).Single();
            return post;
        }

        public mProducts GetProduct(int Id)
        {
            int _Id = Convert.ToInt32(Id);
            seoWebApplication.Models.mProducts query = (from e in _product.Collection.AsQueryable<mProducts>()
                                                       where e.product_id == _Id
                                                       select e).First();

            return query;
        }

        public int GetLastId()
        {
            try
            {
                var query = (from d in GetProducts() orderby d.product_id descending select d).First();

                return query.product_id + 1;
            }
            catch
            {
                return 1;
            }


        }


        internal void ProductThumbnailUpdate(int productid, int webstoreid, string fileName)
        {
            try
            {
                var query = Query.And(
                                        Query<mProducts>.EQ(e => e.product_id, productid),
                                        Query<mProducts>.EQ(e => e.webstore_id, webstoreid) 
                                    );

                var update = Update<mProducts>.Set(e => e.thumbnail, fileName);

                _product.Collection.Update(query, update); 
            }
            catch
            { 
            } 
        }

        internal void ProductImageUpdate(int productid, int webstoreid, string fileName)
        {
            try
            {
                var query = Query.And(
                                        Query<mProducts>.EQ(e => e.product_id, productid),
                                        Query<mProducts>.EQ(e => e.webstore_id, webstoreid)
                                    );

                var update = Update<mProducts>.Set(e => e.image, fileName);

                _product.Collection.Update(query, update);
            }
            catch
            {
            }
        }

        internal IList<mProductAttributeValue> GetProductAttributes(int Id)
        {
            if (Id > 0)
            {
                mProducts query = (from e in _product.Collection.AsQueryable<mProducts>()
                                   where e.product_id == Id
                                   select e).First();

                return query.Attributes;
            }
            else {
                return null;
            }
           
        }
         
        internal mProductAttributeValue GetProductAttribute(int Id, int AttrId)
        {
            if (Id > 0)
            {
                mProducts pquery = (from e in _product.Collection.AsQueryable<mProducts>()
                                   where e.product_id == Id
                                   select e).First();
                var query = (from d in pquery.Attributes where d.AttributeValueID == AttrId select d).First();
                return query;
            }
            else
            {
                return null;
            }

        }

     

        public IList<mProducts> GetProductsOnDeptPromo(string department_id, string pageNumber)
        {
            Guid deptId = _departmentsService.GetDepartmentsById(Convert.ToInt32(department_id)).Id;
            try
            {
                var query = Query.And(
                                      Query<mProducts>.EQ(e => e.DepartmentId, deptId),
                                      Query<mProducts>.EQ(e => e.promofront, true),
                                      Query<mProducts>.EQ(e => e.webstore_id, dBHelper.GetWebstoreId())
                                  );
                return _product.Collection.Find(query).ToList<mProducts>();

            }
            catch (MongoConnectionException)
            {
                return new List<mProducts>();
            }
        } 
        


       

        internal void Update(mProducts p)
        {  
            var query = Query<mProducts>.EQ(e => e.product_id, p.product_id); 
            var update = Update<mProducts>.Set(e => e.name, p.name)
                                           .Set(e => e.price, p.price)
                                           .Set(e => e.IsActive, p.IsActive)
                                           .Set(e => e.image, p.image)
                                           .Set(e => e.thumbnail, p.thumbnail)
                                           .Set(e => e.description, p.description)
                                           .Set(e => e.promodept, p.promodept)
                                           .Set(e => e.promofront, p.promofront)
                                           .Set(e => e.defaultAttCat, p.defaultAttCat)
                                           .Set(e => e.defaultAttribute, p.defaultAttribute)
                                           .Set(e => e.CategoryId, p.CategoryId)
                                           .Set(e => e.SubcategoryId, p.SubcategoryId)
                                           .Set(e => e.DepartmentId, p.DepartmentId)
                                           .Set(e => e.BrandId, p.BrandId)
                                           .Set(e => e.Attributes, p.Attributes)
                                           .Set(e => e.Url, p.Url)
                                           .Set(e => e.store, p.store)
                                           .Set(e => e.Specifications, p.Specifications);

            _product.Collection.Update(query, update); 
        }

        internal void Delete(int id)
        {
            try
            {
                var query = Query<mProducts>.EQ(e => e.product_id, id);  

                _product.Collection.Remove(query);

            }
            catch
            {

            } 
        }

        internal void AddAttrbuteValue(mProductAttributeValue pvalue)
        {
            try
            {
                var query = Query<mProducts>.EQ(e => e.product_id, pvalue.product_id);

                IList<mProductAttributeValue> attr = GetProduct(pvalue.product_id).Attributes;
                 
                if (attr != null)
                {
                    attr.Add(pvalue);
                    var update = Update<mProducts>.Set(e => e.Attributes, attr);

                    _product.Collection.Update(query, update);
                }
                else
                {
                    List<mProductAttributeValue> _attr = new List<mProductAttributeValue>();
                    _attr.Add(pvalue);
                    var update = Update<mProducts>.Set(e => e.Attributes, _attr);

                    _product.Collection.Update(query, update);
                }
            }
            catch
            {
            }
        }

        internal void DeleteAttrbuteValue(mProductAttributeValue pvalue)
        {
            try
            {
                List<mProductAttributeValue> attr = GetProduct(pvalue.product_id).Attributes;
                var query = Query<mProducts>.EQ(e => e.product_id, pvalue.product_id);
                attr.RemoveAll((x) => x.AttributeValueID == pvalue.AttributeValueID);
                var update = Update<mProducts>.Set(e => e.Attributes, attr);

                _product.Collection.Update(query, update);

            }
            catch
            {

            } 
        }
        internal void UpdateAttrbuteValue(mProductAttributeValue pvalue)
        {
            try
            {
                List<mProductAttributeValue> attr = GetProduct(pvalue.product_id).Attributes;
                var query = Query<mProducts>.EQ(e => e.product_id, pvalue.product_id);
                attr.RemoveAll((x) => x.AttributeValueID == pvalue.AttributeValueID);
                attr.Add(pvalue);
                var update = Update<mProducts>.Set(e => e.Attributes, attr); 
                _product.Collection.Update(query, update);

            }
            catch
            {

            }
        }

        internal void SetPromoDefault(int Id)
        { 
            var query = Query<mProducts>.EQ(e => e.product_id, Id);
            var update = Update<mProducts>.Set(e => e.promofront, true);

            _product.Collection.Update(query, update);
        }

        internal void LikeProduct(int id, string userLike)
        { 

            try
            {
                var query = Query<mProducts>.EQ(e => e.product_id, id);

                IList<string> likes = GetProduct(id).Likes;

                if (likes != null)
                {
                    likes.Add(userLike);
                    var update = Update<mProducts>.Set(e => e.Likes, likes);

                    _product.Collection.Update(query, update);
                }
                else
                {
                    List<string> _likes = new List<string>();
                    _likes.Add(userLike);
                    var update = Update<mProducts>.Set(e => e.Likes, _likes);

                    _product.Collection.Update(query, update);
                }
            }
            catch
            {
            }
        }

        internal void UnLikeProduct(int id, string userLike)
        {

            try
            {
                List<string> attr = GetProduct(id).Likes;
                var query = Query<mProducts>.EQ(e => e.product_id, id);
                attr.RemoveAll((x) => x == userLike);
                var update = Update<mProducts>.Set(e => e.Likes, attr);

                _product.Collection.Update(query, update);
            }
            catch
            {
            }
        }

        internal void RemovePromoDefault()
        { 
            var query = Query<mProducts>.EQ(e => e.promofront, true);
            var update = Update<mProducts>.Set(e => e.promofront, false); 

            _product.Collection.Update(query, update, UpdateFlags.Multi); 
        }

        internal List<mProducts> GetProductsByStore(Guid id)
        {
            try
            { 
                return _product.Collection.Find(Query.EQ("store", id)).ToList<mProducts>();
            }
            catch (MongoConnectionException)
            {
                return new List<mProducts>();
            }
        }

        internal List<mProducts> GetProductsBySearch(string id)
        {
            try
            {
                List<mProducts> names = _product.Collection.AsQueryable().Where(name =>
    name.name.ToLower().Contains(id)).ToList();
                return names;
            }
            catch (MongoConnectionException)
            {
                return new List<mProducts>();
            }
        }
    }
}