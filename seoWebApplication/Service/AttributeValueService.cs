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
    public class AttributeValueService
    {

        private readonly MongoHelper<mAttributeValue> _attribute;
        public AttributeValueService()
        {
            _attribute = new MongoHelper<mAttributeValue>();
        }

        public void Create(mAttributeValue product)
        {
            product.AttributeValueID = GetLastId();
            product.Webstore_id = dBHelper.GetWebstoreId();
            _attribute.Collection.Insert(product);
        }


        public IList<mAttributeValue> GetAttributes(int id)
        {
            try
            {
                return _attribute.Collection.Find(Query.EQ("AttributeID", id)).ToList<mAttributeValue>();
            }
            catch (MongoConnectionException)
            {
                return new List<mAttributeValue>();
            }
        }

        public IList<mAttributeValue> GetAttributesById(int Id)
        {
            try
            {
                return _attribute.Collection.Find(Query.EQ("AttributeID", Id)).ToList<mAttributeValue>();

            }
            catch (MongoConnectionException)
            {
                return new List<mAttributeValue>();//
            }
        }

        public IList<mAttributeValue> GetAttributesByCategory(int Id)
        {
            try
            {
                return _attribute.Collection.Find(Query.EQ("category_id", Id)).ToList<mAttributeValue>();

            }
            catch (MongoConnectionException)
            {
                return new List<mAttributeValue>();
            }
        }
 

        public mAttributeValue GetAttribute(int Id)
        {
            int _Id = Convert.ToInt32(Id);
            seoWebApplication.Models.mAttributeValue query = (from e in _attribute.Collection.AsQueryable<mAttributeValue>()
                                                       where e.AttributeValueID == _Id
                                                       select e).First();

            return query;
        }
         
        public IList<mAttributeValue> GetAllAttributes()
        {
            try
            {
                var query = (from e in _attribute.Collection.AsQueryable<mAttributeValue>()
                             select e);
                return query.ToList<mAttributeValue>();
            }
            catch (MongoConnectionException)
            {
                return new List<mAttributeValue>();
            }
        }

        public int GetLastId()
        {
            try
            {
                var query = (from d in GetAllAttributes() orderby d.AttributeValueID ascending select d).First();

                return query.AttributeID + 1;
            }
            catch
            {
                return 1;
            }


        }


        internal void Update(int AttributeID, string Value)
        {
            try
            {
                var query =  Query<mAttributeValue>.EQ(e => e.AttributeID, AttributeID);

                var update = Update<mAttributeValue>.Set(e => e.Value, Value);

                _attribute.Collection.Update(query, update); 
            }
            catch
            { 
            } 
        }



         

 
    }
}