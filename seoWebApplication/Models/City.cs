using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seoWebApplication.Models
{
    [BsonIgnoreExtraElements]
    public class City
    {
        //added
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        public string NameTrimmed
        {
            get
            {
                if (Name.Length > 60)
                    return Name.Substring(0, 60) + "...";
                else
                    return Name;
            }
        }
        [BsonElement("Description")]
        public string Description { get; set; }
        public string DescriptionTrimmed
        {
            get
            {
                if (Description.Length > 60)
                    return Description.Substring(0, 60) + "...";
                else
                    return Description;
            }
        }
        public Guid StateId { get; set; } 
    }
}
