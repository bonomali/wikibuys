using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Models
{
    public class Company
    {
        public Company()
        {
             
        }
        private DateTime date;

        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; }
        [BsonElement("City")]
        public string City { get; set; }
        [BsonElement("State")]
        public string State { get; set; }
        [BsonElement("Zip")]
        public string Zip { get; set; }
        [BsonElement("Country")]
        public string Country { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Phone")]
        public string Phone { get; set; }
        [BsonElement("Url")]
        public string Url { get; set; }

        [BsonElement("IncorporatedDate")]
        public DateTime IncorporatedDate { get; set; }

        [BsonElement("CreatedDate")]
        public DateTime CreatedDate
        {
            get { return date.ToLocalTime(); }
            set { date = value; }
        }
    }
}