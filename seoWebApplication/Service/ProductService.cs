using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using seoWebApplication.DAL;
using seoWebApplication.Models;
using seoWebApplication.st.SharkTankDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Service
{
    public class ProductService
    {

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
                int Id = dBHelper.GetWebstoreId();
                return _product.Collection.Find(Query.EQ("webstore_id", Id)).ToList<mProducts>();
            }
            catch (MongoConnectionException)
            {
                return new List<mProducts>();
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

        internal IList<Categories> ProductCategorySelect(int Id)
        {
            if (Id > 0)
            {
                mProducts query = (from e in _product.Collection.AsQueryable<mProducts>()
                                   where e.product_id == Id
                                   select e).First();

                return query.Categories;
            }
            else {
                return null;
            }
            
        }

        public IList<mProducts> GetProductsOnDeptPromo(string department_id, string pageNumber)
        {
            try
            { 
                int Id = dBHelper.GetWebstoreId();
                int deptId = Convert.ToInt32(department_id);
                var ancestorsQuery = Query<Categories>.EQ(d => d.department_id, deptId);
                var finalQuery = Query<mProducts>.ElemMatch(p => p.Categories, builder => ancestorsQuery);
                 
                return _product.Collection.Find(finalQuery).ToList<mProducts>();
            }
            catch (MongoConnectionException)
            {
                return new List<mProducts>();
            }
        }

        internal void AddProductToCategory(int productid, int categoryid)
        {
            try
            {
                var query =  Query<mProducts>.EQ(e => e.product_id, productid);

                IList<Categories> cat = GetProduct(productid).Categories;
                var dc = new CategoriesService();
                Categories category = dc.GetCategoryById(categoryid); 
                if (cat != null)
                {
                    cat.Add(category);
                    var update = Update<mProducts>.Set(e => e.Categories, cat);

                    _product.Collection.Update(query, update);
                }
                else {
                    List<Categories> _category = new List<Categories>();
                    _category.Add(category);
                    var update = Update<mProducts>.Set(e => e.Categories, _category);

                    _product.Collection.Update(query, update);
                } 
            }
            catch
            {
            }
        }


        internal void DeleteCategory(int id, int pid)
        {
            try
            {  
                List<Categories> cat = GetProduct(pid).Categories;
                var dc = new CategoriesService(); 

                var query = Query<mProducts>.EQ(e => e.product_id, pid); 
                cat.RemoveAll((x) => x.category_id == id);
                var update = Update<mProducts>.Set(e => e.Categories, cat);

                _product.Collection.Update(query, update); 
                
            }
            catch
            {
                
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
                                           .Set(e => e.Categories, p.Categories)
                                           .Set(e => e.Attributes, p.Attributes)
                                           .Set(e => e.Url, p.Url)
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

        internal void RemovePromoDefault()
        { 
            var query = Query<mProducts>.EQ(e => e.promofront, true);
            var update = Update<mProducts>.Set(e => e.promofront, false); 

            _product.Collection.Update(query, update, UpdateFlags.Multi); 
        }
    }
}