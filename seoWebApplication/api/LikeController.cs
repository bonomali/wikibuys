using seoWebApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace seoWebApplication.api
{
    public class LikeController : ApiController
    {
        private ProductService _productService = new ProductService();
        // GET: api/Like
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Like/5
        public string Get(string id, string userid)
        {
            int _id = Convert.ToInt32(id);
            string _userid = userid;
            var mp = _productService.GetProduct(Convert.ToInt32(id));
            if (mp.Likes != null)
            {
                if (!mp.Likes.Exists(x => x == userid))
                {
                    _productService.LikeProduct(_id, userid);
                }
            }
            else {
                _productService.LikeProduct(_id, userid);
            }
            

            int likes = _productService.GetProduct(_id).Likes.Count;

            var retVal = "<i class='fa fa-heart' style='color:red;'>" + likes + "</i>";
            return retVal;
        }

        // POST: api/Like
        public void Post(string id, string userid)
        {
            string _id = id;
            string _userid = userid;
            var _name = User.Identity.Name;
        }

        // PUT: api/Like/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Like/5
        public void Delete(int id)
        {
        }
    }
}
