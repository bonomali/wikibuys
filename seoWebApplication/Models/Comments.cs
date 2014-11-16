using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Models
{
    public class Comment
       {
        public Comment()
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