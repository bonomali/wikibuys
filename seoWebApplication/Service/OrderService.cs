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
    public class OrderService
    {
        
        private readonly MongoHelper<Orders> _order;
        public OrderService()
        {
            _order = new MongoHelper<Orders>();
        }

         public int Create(Orders order)
        {
            order.OrderID = GetLastId();
            _order.Collection.Insert(order);
            return order.OrderID;
        }


         public IList<Orders> GetOrders()
        {
            try
            {
                return _order.Collection.FindAll().ToList<Orders>();
            }
            catch (MongoConnectionException)
            {
                return new List<Orders>();
            }
        }

         public IList<Orders> GetOrdersByWebstoreId(int Id)
         {
             try
             {
                 return _order.Collection.Find(Query.EQ("webstore_id", Id)).ToList<Orders>();
                  
             }
             catch (MongoConnectionException)
             {
                 return new List<Orders>();//
             }
         }

         public IList<Orders> GetOrdersByCustomerId(int Id)
         {
             try
             {
                 return _order.Collection.Find(Query.EQ("CustomerID", Id)).ToList<Orders>();

             }
             catch (MongoConnectionException)
             {
                 return new List<Orders>();
             }
         }

         public Orders GetOrders(string name)
        {
            var post = _order.Collection.Find(Query.EQ("Name", name)).Single(); 
            return post;
        }

         public Orders GetOrder(Guid Id)
         { 
             seoWebApplication.Models.Orders query = (from e in _order.Collection.AsQueryable<Orders>()
                                                        where e.Id == Id
                                                        select e).First();

             return query;
         }

         internal void UpdateOrderTotal(string cartID, decimal orderTotal, int orderPoints)
         {
             var query = Query<Orders>.EQ(e => e.cart_id, cartID);
             var update = Update<Orders>.Set(e => e.total, orderTotal).Set(e => e.point, orderPoints);

             _order.Collection.Update(query, update); 
         }

         public int GetLastId()
         {
             try
             {
                 var query = (from d in GetOrders() orderby d.OrderID descending select d).First();


                 return query.OrderID + 1;
             }
             catch
             {
                 return 1;
             }
         }

         internal int CreateOrder(string cartId, int custId, int p1, int p2, decimal amount, IList<mShoppingCart> lineitem, Users name)
         {
             Orders o = new Orders();
             o.cart_id = cartId;
             o.CustomerID = custId;
             o.total = amount;
             o.lineitem = lineitem;
             o.Customer = name;
             return Create(o);
         }

         internal void UpdateOrderComplete(string cartID, string token, string payerID)
         {
             var query = Query<Orders>.EQ(e => e.cart_id, cartID);
             var update = Update<Orders>.Set(e => e.Status, 1).Set(e => e.Token, token).Set(e => e.PayerID, payerID);

             _order.Collection.Update(query, update);
         }
    }
}