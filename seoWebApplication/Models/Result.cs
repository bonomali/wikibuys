using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace seoWebApplication.Models
{
    public class Result
    {  
        public string ImageUrl { get; set; } 
        public string Message { get; set; } 
        public int Points { get; set; } 
    }
}