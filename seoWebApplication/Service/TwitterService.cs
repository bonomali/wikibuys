using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using seoWebApplication.DAL;
using seoWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Tweetinvi;
using Tweetinvi.Core.Interfaces;
using Tweetinvi.Core.Interfaces.Models.Parameters;


namespace seoWebApplication.Service
{
    public class TwitterService
    {

        private readonly MongoHelper<mTweet> _Tweet;
        public TwitterService()
        {
            TwitterCredentials.SetCredentials(
                ConfigurationManager.AppSettings["token_AccessToken"],
                ConfigurationManager.AppSettings["token_AccessTokenSecret"],
                ConfigurationManager.AppSettings["token_ConsumerKey"],
                ConfigurationManager.AppSettings["token_ConsumerSecret"]);
            _Tweet = new MongoHelper<mTweet>();
        }

        public void Create(ITweet tweet)
        {
            _Tweet.Collection.Insert(tweet);
        }


        public IList<mTweet> Gettweets()
        {
            try
            {
                return _Tweet.Collection.FindAll().ToList<mTweet>();
            }
            catch (MongoConnectionException)
            {
                return new List<mTweet>();
            }
        }

        public IList<mTweet> GettweetsByDepartment(int Id)
        {
            try
            {
                return _Tweet.Collection.Find(Query.EQ("department_id", Id)).ToList<mTweet>();

            }
            catch (MongoConnectionException)
            {
                return new List<mTweet>();//
            }
        }

        public IList<mTweet> GettweetsByCategory(int Id)
        {
            try
            {
                return _Tweet.Collection.Find(Query.EQ("category_id", Id)).ToList<mTweet>();

            }
            catch (MongoConnectionException)
            {
                return new List<mTweet>();
            }
        }

        public mTweet Gettweets(string name)
        {
            var post = _Tweet.Collection.Find(Query.EQ("Name", name)).Single();
            return post;
        }

        public mTweet Gettweet(Guid Id)
        {
            seoWebApplication.Models.mTweet query = (from e in _Tweet.Collection.AsQueryable<mTweet>()
                                                     where e.Id == Id
                                                     select e).First();

            return query;
        }

        public void Search_FilteredSearch(string search)
        {
            var searchParameter = Tweetinvi.Search.GenerateSearchTweetParameter(search);
            searchParameter.TweetSearchFilter = TweetSearchFilter.All;
            searchParameter.Since = DateTime.Now.Subtract(new TimeSpan(1, 0, 0));


            var tweets = Tweetinvi.Search.SearchTweets(searchParameter);

            foreach (ITweet tweet in tweets)
            {
                Create(tweet);
            }
            
           
        }


    }
}