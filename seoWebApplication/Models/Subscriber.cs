using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Models
{
    public class Subscriber
    {
        public Subscriber()
        {
            Date = DateTime.UtcNow;
        }
        private DateTime date;

        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Lastname")]
        public string Lastname { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; } 

        [BsonElement("Date")]
        public DateTime Date
        {
            get { return date.ToLocalTime(); }
            set { date = value; }
        }
         
        [BsonElement("CreatedDate")]
        public DateTime CreatedDate
        {
            get { return date.ToLocalTime(); }
            set { date = value; }
        }
    }
}