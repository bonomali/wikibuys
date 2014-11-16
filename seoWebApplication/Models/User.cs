using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Models
{
    public class User
    {
        public User()
        {
            Date = DateTime.UtcNow;
        }
        private DateTime date;

        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }
        [BsonElement("ClientId")]
        public string ClientId { get; set; }
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("Lastname")]
        public string LastName { get; set; }
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