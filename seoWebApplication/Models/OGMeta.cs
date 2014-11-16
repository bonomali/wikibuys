using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seoWebApplication.Models
{
    public class OGVideo
    {
        public string Content { set; get; }
        public string Type { set; get; }
        public string Width { set; get; }
        public string Height { set; get; }
        public string Tag { set; get; }
        public string SecureUrl { set; get; }
    }

    public class OGMeta
    {
        public OGMeta()
        {
            Video = new OGVideo();
        }

        public string AppId { set; get; }
        public string SiteName { set; get; }
        public string Locale { set; get; }
        public string Type { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public string Url { set; get; }
        public string Image { set; get; }
        public string Audio { set; get; }
        public string Price { set; get; } 
        public string offerlink { set; get; }
        public OGVideo Video { set; get; }
    }
}
