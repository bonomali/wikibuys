using seoWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Framework
{
    public static class ResultHelper
    {
        public static string CountItems(IList<Results> results)
        {
            string retVal = null;

            if (results.Count(c => c.Result.Equals("desani-64")) == 3)
                    {
                        retVal = "desani";
                    }
            if (results.Count(c => c.Result.Equals("coke-64")) == 3) 
                    {
                        retVal = "coke";
                    }
            if (results.Count(c => c.Result.Equals("snickers-64")) == 3) 
                    {
                        retVal = "snickers";
                    }
            if (results.Count(c => c.Result.Equals("doritos-64")) == 3)    
                    {
                        retVal = "doritos";
                    }

                    return retVal; 
            } 
        } 
}