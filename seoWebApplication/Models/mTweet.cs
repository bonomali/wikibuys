using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Interfaces;

namespace seoWebApplication.Models
{
    public class mTweet : ITweet
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        public void AddMedia(byte[] data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<long> Contributors
        {
            get { throw new NotImplementedException(); }
        }

        public int[] ContributorsIds
        {
            get { throw new NotImplementedException(); }
        }

        public Tweetinvi.Core.Interfaces.Models.ICoordinates Coordinates
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime CreatedAt
        {
            get { throw new NotImplementedException(); }
        }

        public IUser Creator
        {
            get { throw new NotImplementedException(); }
        }

        public Tweetinvi.Core.Interfaces.Models.ITweetIdentifier CurrentUserRetweetIdentifier
        {
            get { throw new NotImplementedException(); }
        }

        public bool Destroy()
        {
            throw new NotImplementedException();
        }

        public Tweetinvi.Core.Interfaces.Models.Entities.ITweetEntities Entities
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Favourite()
        {
            throw new NotImplementedException();
        }

        public int FavouriteCount
        {
            get { throw new NotImplementedException(); }
        }

        public bool Favourited
        {
            get { throw new NotImplementedException(); }
        }

        public string FilterLevel
        {
            get { throw new NotImplementedException(); }
        }

        public Tweetinvi.Core.Interfaces.Models.IOEmbedTweet GenerateOEmbedTweet()
        {
            throw new NotImplementedException();
        }

        public List<ITweet> GetRetweets()
        {
            throw new NotImplementedException();
        }

        public List<Tweetinvi.Core.Interfaces.Models.Entities.IHashtagEntity> Hashtags
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string InReplyToScreenName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public long? InReplyToStatusId
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string InReplyToStatusIdStr
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public long? InReplyToUserId
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string InReplyToUserIdStr
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsRetweet
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsTweetDestroyed
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsTweetPublished
        {
            get { throw new NotImplementedException(); }
        }

        public Tweetinvi.Core.Enum.Language Language
        {
            get { throw new NotImplementedException(); }
        }

        public int Length
        {
            get { throw new NotImplementedException(); }
        }

        public List<Tweetinvi.Core.Interfaces.Models.Entities.IMediaEntity> Media
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Tweetinvi.Core.Interfaces.Models.IPlace Place
        {
            get { throw new NotImplementedException(); }
        }

        public bool PossiblySensitive
        {
            get { throw new NotImplementedException(); }
        }

        public bool Publish()
        {
            throw new NotImplementedException();
        }

        public bool PublishInReplyTo(ITweet replyToTweet)
        {
            throw new NotImplementedException();
        }

        public bool PublishInReplyTo(long replyToTweetId)
        {
            throw new NotImplementedException();
        }

        public bool PublishReply(ITweet replyTweet)
        {
            throw new NotImplementedException();
        }

        public ITweet PublishReply(string text)
        {
            throw new NotImplementedException();
        }

        public ITweet PublishRetweet()
        {
            throw new NotImplementedException();
        }

        public bool PublishWithGeo(double longitude, double latitude)
        {
            throw new NotImplementedException();
        }

        public bool PublishWithGeo(Tweetinvi.Core.Interfaces.Models.ICoordinates coordinates)
        {
            throw new NotImplementedException();
        }

        public bool PublishWithGeoInReplyTo(double longitude, double latitude, ITweet replyToTweet)
        {
            throw new NotImplementedException();
        }

        public bool PublishWithGeoInReplyTo(double longitude, double latitude, long replyToTweetId)
        {
            throw new NotImplementedException();
        }

        public bool PublishWithGeoInReplyTo(Tweetinvi.Core.Interfaces.Models.ICoordinates coordinates, ITweet replyToTweet)
        {
            throw new NotImplementedException();
        }

        public int RetweetCount
        {
            get { throw new NotImplementedException(); }
        }

        public bool Retweeted
        {
            get { throw new NotImplementedException(); }
        }

        public ITweet RetweetedTweet
        {
            get { throw new NotImplementedException(); }
        }

        public List<ITweet> Retweets
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Dictionary<string, object> Scopes
        {
            get { throw new NotImplementedException(); }
        }

        public string Source
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Text
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool Truncated
        {
            get { throw new NotImplementedException(); }
        }

        public Tweetinvi.Core.Interfaces.DTO.ITweetDTO TweetDTO
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime TweetLocalCreationDate
        {
            get { throw new NotImplementedException(); }
        }

        public int TweetRemainingCharacters()
        {
            throw new NotImplementedException();
        }

        public void UnFavourite()
        {
            throw new NotImplementedException();
        }

        public List<Tweetinvi.Core.Interfaces.Models.Entities.IUrlEntity> Urls
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public List<Tweetinvi.Core.Interfaces.Models.Entities.IUserMentionEntity> UserMentions
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool WithheldCopyright
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<string> WithheldInCountries
        {
            get { throw new NotImplementedException(); }
        }

        public string WithheldScope
        {
            get { throw new NotImplementedException(); }
        }

        long Tweetinvi.Core.Interfaces.Models.ITweetIdentifier.Id
        {
            get { throw new NotImplementedException(); }
        }

        public string IdStr
        {
            get { throw new NotImplementedException(); }
        }

        public Task<bool> DestroyAsync()
        {
            throw new NotImplementedException();
        }

        public Task FavouriteAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tweetinvi.Core.Interfaces.Models.IOEmbedTweet> GenerateOEmbedTweetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ITweet>> GetRetweetsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PublishAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PublishInReplyToAsync(ITweet replyToTweet)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PublishInReplyToAsync(long replyToTweetId)
        {
            throw new NotImplementedException();
        }

        public Task<ITweet> PublishReplyAsync(string text)
        {
            throw new NotImplementedException();
        }

        public Task<ITweet> PublishRetweetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PublishWithGeoAsync(double longitude, double latitude)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PublishWithGeoAsync(Tweetinvi.Core.Interfaces.Models.ICoordinates coordinates)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PublishWithGeoInReplyToAsync(double latitude, double longitude, ITweet replyToTweet)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PublishWithGeoInReplyToAsync(double latitude, double longitude, long replyToTweetId)
        {
            throw new NotImplementedException();
        }

        public Task UnFavouriteAsync()
        {
            throw new NotImplementedException();
        }

        public bool Equals(ITweet other)
        {
            throw new NotImplementedException();
        }
    }
}
