using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Models
{
    public class mShoppingCart 
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }
        [BsonElement("cart_id")]
        public string cart_id { get; set; }
        public int product_id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int webstore_id { get; set; }
        public string attributes { get; set; }
        public int quantity { get; set; }
        public decimal subtotal { get; set; }
        public DateTime dateadded { get; set; }
    }
}