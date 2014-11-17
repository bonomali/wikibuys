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
    public class ScrapeService
    {

        private readonly MongoHelper<mScrape> _mScrapes;
        public ScrapeService()
        {
            _mScrapes = new MongoHelper<mScrape>();
        }


        public void Create(mScrape mScrape)
        {
            _mScrapes.Collection.Insert(mScrape);
        }
        public void Edit(mScrape mScrape)
        {
            _mScrapes.Collection.Update(
                Query.EQ("_id", mScrape.Id),
                Update.Set("Name", mScrape.Name)
                    .Set("Url", mScrape.Url));
        }

        public IList<mScrape> GetmScrapes()
        {
            try
            {
                return _mScrapes.Collection.FindAll().ToList<mScrape>();
            }
            catch (MongoConnectionException)
            {
                return new List<mScrape>();
            }
        }

        public mScrape GetmScrape(string url)
        {
            var mScrape = _mScrapes.Collection.Find(Query.EQ("Url", url)).Single(); 
            return mScrape;
        }

        internal mScrape GetmScrapeById(Guid id)
        {
            var mScrape = _mScrapes.Collection.Find(Query.EQ("_id", id)).Single(); 
            return mScrape;
        }


        internal void AddScrapeProperties(ScrapeProperties pvals)
        {
            try
            {
                var query = Query<mScrape>.EQ(e => e.Id, pvals.Id);

                IList<ScrapeProperties> attr = GetmScrapeById(pvals.Id).Properties;
                
                Guid id = Guid.NewGuid();
                pvals.Id = id;
                if (attr != null)
                {
                    attr.Add(pvals);
                    var update = Update<mScrape>.Set(e => e.Properties, attr);

                    _mScrapes.Collection.Update(query, update);
                }
                else
                {
                    List<ScrapeProperties> _attr = new List<ScrapeProperties>();
                    _attr.Add(pvals);
                    var update = Update<mScrape>.Set(e => e.Properties, _attr);

                    _mScrapes.Collection.Update(query, update);
                }
            }
            catch
            {
            }
        }

        internal void DeleteScrapeProperties(Guid Id, Guid Pid)
        {
            try
            {
                List<ScrapeProperties> attr = GetmScrapeById(Pid).Properties;
                var query = Query<mScrape>.EQ(e => e.Id, Pid);
                attr.RemoveAll((x) => x.Id == Id);
                var update = Update<mScrape>.Set(e => e.Properties, attr);

                _mScrapes.Collection.Update(query, update);

            }
            catch
            {

            } 
        }
    }
}