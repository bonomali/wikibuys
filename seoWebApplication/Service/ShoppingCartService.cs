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
    public class ShoppingCartService
    {
        private readonly MongoHelper<mShoppingCart> _shoppingcart;
        public ShoppingCartService()
        {
            _shoppingcart = new MongoHelper<mShoppingCart>();
        }

        public bool Create(mShoppingCart shoppingcart)
        {
            try
            { 
                _shoppingcart.Collection.Insert(shoppingcart);
                return true;
            }
            catch {
                return false;
            }
            
        }

        public bool Delete(mShoppingCart shoppingcart)
        {
            try
            {  
                var query = Query.And(
                                        Query<mShoppingCart>.EQ(e => e.product_id, shoppingcart.product_id),
                                        Query<mShoppingCart>.EQ(e => e.cart_id, shoppingcart.cart_id)
                                    );
                 
                
                _shoppingcart.Collection.Remove(query);  
                return true;
            }
            catch
            {
                return false;
            } 
        }

        public bool Update(mShoppingCart shoppingcart)
        {
            try
            {
                var query = Query.And(
                                        Query<mShoppingCart>.EQ(e => e.product_id, shoppingcart.product_id),
                                        Query<mShoppingCart>.EQ(e => e.cart_id, shoppingcart.cart_id)
                                    );
                
                var update = Update<mShoppingCart>.Set(e => e.quantity, shoppingcart.quantity).Set(e => e.price, shoppingcart.price).Set(c => c.subtotal, shoppingcart.subtotal);
                 
                _shoppingcart.Collection.Update(query, update); 
                return true;
            }
            catch
            {
                return false;
            } 
        }

         


        public IList<mShoppingCart> GetShoppingCart()
        {
            try
            {
                return _shoppingcart.Collection.FindAll().ToList<mShoppingCart>();
            }
            catch (MongoConnectionException)
            {
                return new List<mShoppingCart>();
            }
        }

        public IList<mShoppingCart> GetShoppingCartByDepartment(int Id)
        {
            try
            {
                return _shoppingcart.Collection.Find(Query.EQ("department_id", Id)).ToList<mShoppingCart>();

            }
            catch (MongoConnectionException)
            {
                return new List<mShoppingCart>();//
            }
        }

        public IList<mShoppingCart> GetShoppingCartById(string cart_id)
        {
            try
            {
                return _shoppingcart.Collection.Find(Query.EQ("cart_id", cart_id)).ToList<mShoppingCart>();

            }
            catch (MongoConnectionException)
            {
                return new List<mShoppingCart>();
            }
        }

        public decimal GetShoppingTotalById(string cart_id)
        {
            try
            {
                var itemsCart = _shoppingcart.Collection.Find(Query.EQ("cart_id", cart_id)).ToList<mShoppingCart>();

                return itemsCart.Select(c => c.subtotal).Sum();

            }
            catch (MongoConnectionException)
            {
                return 0;
            }
        }

         
    }
}