using MongoDB.Driver;
using MongoDB.Driver.Builders;
using seoWebApplication.DAL;
using seoWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Service
{
    public class PostService
    {
        private readonly MongoHelper<Post> _posts;
        public PostService()
        {
            _posts = new MongoHelper<Post>();
        }

        public void Create(Post post)
        {
            _posts.Collection.Insert(post);
        }
        public void Edit(Post post)
        {
            _posts.Collection.Update(
                Query.EQ("_id", post.PostId),
                Update.Set("Title", post.Title)
                    .Set("Url", post.Url)
                    .Set("Summary", post.Summary)
                    .Set("Details", post.Details));
        }

        public IList<Post> GetPosts()
        {
            try
            {
                return _posts.Collection.FindAll().ToList<Post>();
            }
            catch (MongoConnectionException)
            {
                return new List<Post>();
            }
        }

        public Post GetPost(string url)
        {
            var post = _posts.Collection.Find(Query.EQ("Url", url)).SetFields(Fields.Slice("Comments", -5)).Single();
            //post.Comments = post.Comments.OrderByDescending(c => c.Date).ToList();
            return post;
        }
    }
}