using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Models
{
    public class Departments
    {
        private DateTime date;

        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }
        [BsonElement("department_id")]
        public int department_id { get; set; }
        [BsonElement("webstore_id")]
        public int webstore_id { get; set; }
         [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("InsertDate")]
        public System.DateTime InsertDate { get; set; }
        [BsonElement("InsertENTUserAccountId")]
        public int InsertENTUserAccountId { get; set; }         
        [BsonElement("UpdateDate")]
        public DateTime UpdateDate
        {
            get { return date.ToLocalTime(); }
            set { date = value; }
        }
         [BsonElement("UpdateENTUserAccountId")]
        public int UpdateENTUserAccountId { get; set; }
        [BsonElement("Version")]
        public byte[] Version { get; set; }
    }
}