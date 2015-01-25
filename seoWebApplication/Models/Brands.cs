using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Models
{
    public class Brands
    { 
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }
        [BsonElement("webstore_id")]
        public int webstore_id { get; set; }
        [BsonElement("brand_id")]
        public int brand_id { get; set; } 
         [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }  
    }
}