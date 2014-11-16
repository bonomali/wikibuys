using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Models
{
    public class mProductAttributeValue
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }
        public int ProductAttributeValueId { get; set; }

        public int product_id { get; set; }

        public int AttributeValueID { get; set; }

        public int webstore_id { get; set; }

        public decimal Value { get; set; }
        public string Name { get; set; }
        public string AttributeName { get; set; }

        public System.DateTime InsertDate { get; set; }

        public int InsertENTUserAccountId { get; set; }

        public System.DateTime UpdateDate { get; set; }

        public int UpdateENTUserAccountId { get; set; }
    }
}