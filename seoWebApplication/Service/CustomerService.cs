using MongoDB.AspNet.Identity;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using seoWebApplication.DAL;
using seoWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seoWebApplication.Service
{
    public class CustomerService
    {

        private readonly MongoHelper<Users> _customer;
        public CustomerService()
        {
            _customer = new MongoHelper<Users>();
        }

        //public void Create(ApplicationUser user)
        //{
        //    _customer.Collection.Insert(user);
        //}


        //public IList<ApplicationUser> GetUsers()
        //{
        //    try
        //    {
        //        return _customer.Collection.FindAll().ToList<ApplicationUser>();
        //    }
        //    catch (MongoConnectionException)
        //    {
        //        return new List<ApplicationUser>();
        //    }
        //}

        //public IList<ApplicationUser> GetProductsByDepartment(int Id)
        // {
        //     try
        //     {
        //         return _customer.Collection.Find(Query.EQ("department_id", Id)).ToList<ApplicationUser>();
                  
        //     }
        //     catch (MongoConnectionException)
        //     {
        //         return new List<ApplicationUser>();//
        //     }
        // }

        //public IList<ApplicationUser> GetCustomersByWebstore(int Id)
        // {
        //     try
        //     {
        //         return _customer.Collection.Find(Query.EQ("webstore_id", Id)).ToList<ApplicationUser>();

        //     }
        //     catch (MongoConnectionException)
        //     {
        //         return new List<ApplicationUser>();
        //     }
        // }

       

        public void updateTotalPoints(int cId, int points)
        {
            try
            {
                Users cust = (from e in _customer.Collection.AsQueryable<Users>()
                              where e.CellPhone == cId.ToString()
                               select e).First();

                int newPoints = points;
                int oldPoints = Convert.ToInt32(cust.RewardPoints);

                //check if new points are greater than old points
                if (newPoints > oldPoints)
                {
                    newPoints -= oldPoints;
                }
                else
                {
                    newPoints = 0;
                }
                cust.RewardPoints = newPoints;

                Save(cust); 
                 

            }
            catch (MongoConnectionException)
            {
              
            }
        }

        public void Save(Users user)
        {
            try
            {
                _customer.Collection.Save(user); 
            }
            catch (MongoConnectionException)
            { 
            }
        }

        public Users GetCustomerByUserName(string email)
        {
            try
            { 
                Users query = (from e in _customer.Collection.AsQueryable<Users>()
                               where e.UserName == email
                               select e).First();

                return query;

            }
            catch (MongoConnectionException)
            {
                return new Users();
            }
        }

        public Users GetCustomerByNumber(string Id)
         {
             int _Id = Convert.ToInt32(Id);
             Users query = (from e in _customer.Collection.AsQueryable<Users>()
                                                        where e.CellPhone == Id
                                                        select e).First();

             return query;
         }
    }
}