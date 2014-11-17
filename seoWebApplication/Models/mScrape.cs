using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seoWebApplication.Models
{
   
    public class mScrape
    {

        private DateTime date;
        //added
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; } 
        public List<ScrapeProperties> Properties { get; set; } 
        [BsonElement("Url")]
        public string Url { get; set; }
        [BsonElement("UpdateDate")]
        public DateTime UpdateDate
        {
            get { return date.ToLocalTime(); }
            set { date = value; }
        }
    }
}
