using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Models
{
    public class DeviceLog
    {
        public DeviceLog()
        {
            Date = DateTime.UtcNow;
        }
        private DateTime date;

        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }
        [BsonElement("DeviceId")]
        public Guid DeviceId { get; set; }

        [BsonElement("ClientId")]
        public string ClientId { get; set; }

        [BsonElement("Results")] 
        public IList<Results> Results { get; set; }

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