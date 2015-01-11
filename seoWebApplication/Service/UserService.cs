using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using seoWebApplication.DAL;
using seoWebApplication.Models;
using seoWebApplication.st.SharkTankDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace seoWebApplication.Service
{
    public class UserService
    {

        private readonly MongoHelper<Users> _user;
        public UserService()
        {
            _user = new MongoHelper<Users>();
        }



        public IEnumerable<Users> GetUsers(int limit, int skip)
        {
            var productsCursor = _user.Collection.FindAllAs<Users>()
                .SetSortOrder(SortBy<Users>.Ascending(p => p.UserId))
                .SetLimit(limit)
                .SetSkip(skip)
                .SetFields(Fields<Users>.Include(p => p.Id, p => p.UserId));
            return productsCursor;
        }



        public IList<Users> GetUsers()
        {
            try
            {
                int Id = dBHelper.GetWebstoreId();
                return _user.Collection.FindAll().ToList<Users>();
            }
            catch (MongoConnectionException)
            {
                return new List<Users>();
            }
        }




        public Users GetUser(string name)
        {
            Users retUser;
            try
            { 
                retUser = _user.Collection.Find(Query.EQ("UserId", name)).Single();
            }
            catch 
            {
                retUser = null;
            }
            return retUser;
        }



        internal Users GetUserByEmail(string email)
        {
            var post = _user.Collection.Find(Query.EQ("UserName", email)).Single();
            return post;
        }
    }
}