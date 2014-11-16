using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;

namespace seoWebApplication.Models
{
    public class Results
    {
        [BsonElement("Result")]
        public string Result { get; set; }
         
    }
}