using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Models
{
    public class Subcategories
    {
        private DateTime date;

        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }
        [BsonElement("webstore_id")]
        public int webstore_id { get; set; }
        [BsonElement("subcategory_id")]
        public int subcategory_id { get; set; }
        [BsonElement("category_id")]
        public Guid category_id { get; set; }
         [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; } 
    }
}