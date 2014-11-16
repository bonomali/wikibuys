using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Models
{

    public class mAttribute 
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }
        public int AttributeID { get; set; }

        public string Name { get; set; }

        public string ControlType { get; set; }

        public int Webstore_id { get; set; }

        public bool ApplyToAllProducts { get; set; }

        public bool ApplyToCategory { get; set; }

        public System.DateTime InsertDate { get; set; }

        public int InsertENTUserAccountId { get; set; }

        public System.DateTime UpdateDate { get; set; }

        public int UpdateENTUserAccountId { get; set; }

        public byte[] Version { get; set; } 
        public IList<mAttributeValue> AttributeValues { get; set; }
    }
}