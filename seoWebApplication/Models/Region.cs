﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seoWebApplication.Models
{
    [BsonIgnoreExtraElements]
    public class Region
    {
        //added
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        
        [BsonElement("Description")]
        public string Description { get; set; }
      
    }
}
