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
    public class CityService
    {

        private readonly MongoHelper<City> _city;
        public CityService()
        {
            _city = new MongoHelper<City>();
        }

        public void Create(City city)
        {  
            _city.Collection.Insert(city);
        }

        public IEnumerable<City> Getcitys(int limit, int skip)
        {
            var citysCursor = _city.Collection.FindAllAs<City>()
                .SetSortOrder(SortBy<City>.Ascending(p => p.Name))
                .SetLimit(limit)
                .SetSkip(skip)
                .SetFields(Fields<City>.Include(p => p.Id, p => p.Name));
            return citysCursor;
        }
 


        public IList<City> Getcitys()
        {
            try
            { 
                return _city.Collection.FindAll().ToList<City>();
            }
            catch (MongoConnectionException)
            {
                return new List<City>();
            }
        }
          
        public IList<City> GetcitysByState(Guid Id)
        {
            try
            {
                return _city.Collection.Find(Query.EQ("StateId", Id)).ToList<City>();

            }
            catch (MongoConnectionException)
            {
                return new List<City>();
            }
        }

        public City Getcitys(string Name)
        {
            var post = _city.Collection.Find(Query.EQ("Name", BsonRegularExpression.Create(new Regex(Name, RegexOptions.IgnoreCase)))).Single();
            return post;
        }

        public City Getcity(Guid Id)
        { 
            seoWebApplication.Models.City query = (from e in _city.Collection.AsQueryable<City>()
                                                      where e.Id == Id
                                                       select e).First();

            return query;
        } 
          
        internal void Update(City p)
        {  
            var query = Query<City>.EQ(e => e.Id, p.Id);
            var update = Update<City>.Set(e => e.Name, p.Name)
                                           .Set(e => e.Description, p.Description)
             .Set(e => e.StateId, p.StateId);

            _city.Collection.Update(query, update); 
        }

        internal void Delete(Guid id)
        {
            try
            {
                var query = Query<City>.EQ(e => e.Id, id);  

                _city.Collection.Remove(query);

            }
            catch
            {

            } 
        }

            
    }
}