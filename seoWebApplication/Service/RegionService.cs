using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using seoWebApplication.DAL;
using seoWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions; 

namespace seoWebApplication.Service
{
    public class RegionService
    {

        private readonly MongoHelper<Region> _region;
        public RegionService()
        {
            _region = new MongoHelper<Region>();
        }

        public void Create(Region Region)
        {  
            _region.Collection.Insert(Region);
        }

        public IEnumerable<Region> GetRegions(int limit, int skip)
        {
            var RegionsCursor = _region.Collection.FindAllAs<Region>()
                .SetSortOrder(SortBy<Region>.Ascending(p => p.Name))
                .SetLimit(limit)
                .SetSkip(skip)
                .SetFields(Fields<Region>.Include(p => p.Id, p => p.Name));
            return RegionsCursor;
        }
 


        public IList<Region> GetRegions()
        {
            try
            { 
                return _region.Collection.FindAll().ToList<Region>();
            }
            catch (MongoConnectionException)
            {
                return new List<Region>();
            }
        }
          
        public IList<Region> GetRegionsByState(Guid Id)
        {
            try
            {
                return _region.Collection.Find(Query.EQ("StateId", Id)).ToList<Region>();

            }
            catch (MongoConnectionException)
            {
                return new List<Region>();
            }
        }

        public Region GetRegions(string Name)
        {
            var post = _region.Collection.Find(Query.EQ("Name", BsonRegularExpression.Create(new Regex(Name, RegexOptions.IgnoreCase)))).Single();
            return post;
        }

        public Region GetRegion(Guid Id)
        { 
            seoWebApplication.Models.Region query = (from e in _region.Collection.AsQueryable<Region>()
                                                      where e.Id == Id
                                                       select e).First();

            return query;
        } 
          
        internal void Update(Region p)
        {  
            var query = Query<Region>.EQ(e => e.Id, p.Id);
            var update = Update<Region>.Set(e => e.Name, p.Name)
                                           .Set(e => e.Description, p.Description);

            _region.Collection.Update(query, update); 
        }

        internal void Delete(Guid id)
        {
            try
            {
                var query = Query<Region>.EQ(e => e.Id, id);  

                _region.Collection.Remove(query);

            }
            catch
            {

            } 
        }

            
    }
}