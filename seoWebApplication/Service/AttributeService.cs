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
    public class AttributeService
    {

        private readonly MongoHelper<mAttribute> _attribute;
        public AttributeService()
        {
            _attribute = new MongoHelper<mAttribute>();
        }

        public void Create(mAttribute product)
        {
            product.AttributeID = GetLastId();
            product.Webstore_id = dBHelper.GetWebstoreId();
            _attribute.Collection.Insert(product);
        }


        public IList<mAttribute> GetAttributes()
        {
            try
            {
                int Id = dBHelper.GetWebstoreId();
                return _attribute.Collection.Find(Query.EQ("Webstore_id", Id)).ToList<mAttribute>();
            }
            catch (MongoConnectionException)
            {
                return new List<mAttribute>();
            }
        }

        public IList<mAttribute> GetAttributesById(int Id)
        {
            try
            {
                return _attribute.Collection.Find(Query.EQ("AttributeID", Id)).ToList<mAttribute>();

            }
            catch (MongoConnectionException)
            {
                return new List<mAttribute>();//
            }
        }

        public IList<mAttribute> GetAttributesByCategory(int Id)
        {
            try
            {
                return _attribute.Collection.Find(Query.EQ("category_id", Id)).ToList<mAttribute>();

            }
            catch (MongoConnectionException)
            {
                return new List<mAttribute>();
            }
        }
 

        public mAttribute GetAttribute(int Id)
        {
            int _Id = Convert.ToInt32(Id);
            seoWebApplication.Models.mAttribute query = (from e in _attribute.Collection.AsQueryable<mAttribute>()
                                                       where e.AttributeID == _Id
                                                       select e).First();

            return query;
        }

        public int GetLastId()
        {
            try
            {
                var query = (from d in GetAttributes() orderby d.AttributeID ascending select d).First();

                return query.AttributeID + 1;
            }
            catch
            {
                return 1;
            }


        }


        internal void Update(int AttributeID, string Name)
        {
            try
            {
                var query =  Query<mAttribute>.EQ(e => e.AttributeID, AttributeID);

                var update = Update<mAttribute>.Set(e => e.Name, Name);

                _attribute.Collection.Update(query, update); 
            }
            catch
            { 
            } 
        }



        internal mAttributeValue GetAttributeValue(int id, int attrId)
        {
            int _Id = Convert.ToInt32(id);
            seoWebApplication.Models.mAttributeValue query = (from e in _attribute.Collection.AsQueryable<mAttributeValue>()
                                                         where e.AttributeValueID == _Id
                                                         select e).First();
            return query;
        }

        internal void AddAttributeValue(int id, mAttributeValue mAttr) {
            try
            {
                var query = Query<mAttribute>.EQ(e => e.AttributeID, id);

                IList<mAttributeValue> attr = GetAttribute(id).AttributeValues; 

                if (attr != null)
                {
                    attr.Add(mAttr);
                    var update = Update<mAttribute>.Set(e => e.AttributeValues, attr);

                    _attribute.Collection.Update(query, update);
                }
                else
                {
                    List<mAttributeValue> _mAttributeValue = new List<mAttributeValue>(); 
                    var update = Update<mAttribute>.Set(e => e.AttributeValues, _mAttributeValue);

                    _attribute.Collection.Update(query, update);
                }



            }
            catch
            {
            }
        }
    }
}