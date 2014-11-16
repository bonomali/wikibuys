using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Models
{
    public class mAttributeValue
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }
        public int AttributeValueID { get; set; }

        public int AttributeID { get; set; }

        public string Value { get; set; } 
    
        public int Webstore_id { get; set; }

        public System.DateTime InsertDate { get; set; }

        public int InsertENTUserAccountId { get; set; }

        public System.DateTime UpdateDate { get; set; }

        public int UpdateENTUserAccountId { get; set; }
    }
}