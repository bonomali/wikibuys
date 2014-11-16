using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;

namespace seoWebApplication.Models
{
    public class Note
    {
        public Note()
        {
            Date = DateTime.UtcNow;
        }

        private DateTime date;

        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        [BsonElement("Note")]
        public string Text { get; set; }

        [BsonElement("Date")]
        public DateTime Date
        {
            get { return date.ToLocalTime(); }
            set { date = value; }
        }
    }
}