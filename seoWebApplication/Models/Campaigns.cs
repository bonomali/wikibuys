using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Models
{
    public class Campaigns
    {
        private DateTime date;
        public object UserKey;
        //added
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }
         [BsonElement("product_id")]
        public int product_id { get; set; }
         [BsonElement("store")]
         public Guid store { get; set; }
         [BsonElement("webstore_id")]
        public int webstore_id { get; set; }
          [BsonElement("category_id")]
         public int category_id { get; set; }        
         [BsonElement("name")]
        public string name { get; set; }
       
         [BsonElement("description")]
        public string description { get; set; }
      
         [BsonElement("price")]
        public decimal price { get; set; }
         [BsonElement("thumbnail")]
        public string thumbnail { get; set; }
        [BsonElement("image")]
        public string image { get; set; }
        [BsonElement("Url")]
        public string Url { get; set; }
         [BsonElement("Specifications")]
        public string Specifications { get; set; }
         [BsonElement("promofront")]
        public bool promofront { get; set; }
         [BsonElement("promodept")]
        public bool promodept { get; set; }
         [BsonElement("IsActive")]
         public bool IsActive { get; set; }
         [BsonElement("defaultAttribute")]
        public bool defaultAttribute { get; set; }
         [BsonElement("defaultAttCat")]
        public bool defaultAttCat { get; set; }        
        [BsonElement("InsertDate")]
        public System.DateTime InsertDate { get; set; }
        [BsonElement("InsertENTUserAccountId")]
        public int InsertENTUserAccountId { get; set; }         
        
 
    }
}