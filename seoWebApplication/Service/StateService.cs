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
    public class StateService
    {

        private readonly MongoHelper<State> _state;
        public StateService()
        {
            _state = new MongoHelper<State>();
        }

        public void Create(State State)
        {  
            _state.Collection.Insert(State);
        }

        public IEnumerable<State> GetStates(int limit, int skip)
        {
            var StatesCursor = _state.Collection.FindAllAs<State>()
                .SetSortOrder(SortBy<State>.Ascending(p => p.Name))
                .SetLimit(limit)
                .SetSkip(skip)
                .SetFields(Fields<State>.Include(p => p.Id, p => p.Name));
            return StatesCursor;
        }
 


        public IList<State> GetStates()
        {
            try
            { 
                return _state.Collection.FindAll().ToList<State>();
            }
            catch (MongoConnectionException)
            {
                return new List<State>();
            }
        }
          
        public IList<State> GetStatesByState(Guid Id)
        {
            try
            {
                return _state.Collection.Find(Query.EQ("StateId", Id)).ToList<State>();

            }
            catch (MongoConnectionException)
            {
                return new List<State>();
            }
        }

        public State GetStates(string Name)
        {
            var post = _state.Collection.Find(Query.EQ("Name", BsonRegularExpression.Create(new Regex(Name, RegexOptions.IgnoreCase)))).Single();
            return post;
        }

        public State GetState(Guid Id)
        { 
            seoWebApplication.Models.State query = (from e in _state.Collection.AsQueryable<State>()
                                                      where e.Id == Id
                                                       select e).First();

            return query;
        } 
          
        internal void Update(State p)
        {  
            var query = Query<State>.EQ(e => e.Id, p.Id);
            var update = Update<State>.Set(e => e.Name, p.Name)
                                           .Set(e => e.Description, p.Description);

            _state.Collection.Update(query, update); 
        }

        internal void Delete(Guid id)
        {
            try
            {
                var query = Query<State>.EQ(e => e.Id, id);  

                _state.Collection.Remove(query);

            }
            catch
            {

            } 
        }

            
    }
}