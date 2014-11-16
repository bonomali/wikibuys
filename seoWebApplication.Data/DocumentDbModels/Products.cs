using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seoWebApplication.Data.DocumentDbModels
{
    public class Products
    {
        private DateTime date;
        //added  
        public Guid Id { get; set; } 
        public int product_id { get; set; } 
        public int webstore_id { get; set; } 
        public string name { get; set; } 
        public string description { get; set; } 
        public decimal price { get; set; } 
        public string thumbnail { get; set; } 
        public string image { get; set; } 
        public bool promofront { get; set; } 
        public bool promodept { get; set; } 
        public bool IsActive { get; set; } 
        public bool defaultAttribute { get; set; } 
        public bool defaultAttCat { get; set; } 
        public System.DateTime InsertDate { get; set; } 
        public int InsertENTUserAccountId { get; set; } 
        public DateTime UpdateDate
        {
            get { return date.ToLocalTime(); }
            set { date = value; }
        } 
        public int UpdateENTUserAccountId { get; set; } 
        public byte[] Version { get; set; }
    }
}
