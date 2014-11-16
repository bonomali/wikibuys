using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Models
{
    public class IdentityContext
    {
        public MongoCollection Users { get; private set; }

        public IdentityContext(MongoCollection users)
        {
            Users = users;
            EnsureUniqueIndexOnUserName(users);
        }

        private void EnsureUniqueIndexOnUserName(MongoCollection users)
        {
            var userName = new IndexKeysBuilder().Ascending("UserName");
            var unique = new IndexOptionsBuilder().SetUnique(true);
            users.EnsureIndex(userName, unique);
        }
    }
}