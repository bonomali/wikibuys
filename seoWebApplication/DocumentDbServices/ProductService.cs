using Microsoft.Azure.Documents;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using seoWebApplication.DAL;
using seoWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Configuration; 
using System.Threading.Tasks;
using seoWebApplication.DocumentDbModels;

namespace seoWebApplication.DocumentDbServices
{
    public class ProductService
    {

        private readonly DocumentDBHelper<Products> _product;
         public ProductService()
        {
            _product = new DocumentDBHelper<Products>();
        }

         public async Task Create(seoWebApplication.DocumentDbModels.Products product)
        {
            try
            {
                var client = CreateDocumentDbConn.CreateConnection();
                string dbName = ConfigurationManager.AppSettings["DatabaseId"];
                Database database = GetDocumentDb.GetDb();

                DocumentCollection col1 = await GetOrCreateCollectionAsync(database.SelfLink, "Product");

                Document doc1 = await client.CreateDocumentAsync(col1.DocumentsLink, product);
                 
            }
            catch { }
            }

         private static async Task<DocumentCollection> GetOrCreateCollectionAsync(string dbLink, string id)
         {
             var client = CreateDocumentDbConn.CreateConnection();
               
             var collection = client.CreateDocumentCollectionQuery(dbLink).Where(c => c.Id == id).ToArray().FirstOrDefault();
             if (collection == null)
             {
                 collection = await client.CreateDocumentCollectionAsync(dbLink, new DocumentCollection { Id = id });
             }

             return collection;
         }
         public async Task GetProducts()
         {
             try
             {
                 var client = CreateDocumentDbConn.CreateConnection();
                 string dbName = ConfigurationManager.AppSettings["DatabaseId"];
                 Database database = GetDocumentDb.GetDb();
                 DocumentCollection collection = await GetOrCreateCollectionAsync(database.SelfLink, "Product");

                 foreach (seoWebApplication.DocumentDbModels.Products product in client.CreateDocumentQuery(collection.SelfLink, "select * from Product"))
                 {
                     Console.WriteLine("\tRead {0} from SQL", product.name);
                 }
             }
             catch (Exception e)
             { 
             }
         }

        // public IList<Products> GetProductsByDepartment(int Id)
        // {
        //     try
        //     {
        //         return _product.Collection.Find(Query.EQ("department_id", Id)).ToList<Products>();
                  
        //     }
        //     catch (MongoConnectionException)
        //     {
        //         return new List<Products>();//
        //     }
        // }

        // public IList<Products> GetProductsByCategory(int Id)
        // {
        //     try
        //     {
        //         return _product.Collection.Find(Query.EQ("category_id", Id)).ToList<Products>();

        //     }
        //     catch (MongoConnectionException)
        //     {
        //         return new List<Products>();
        //     }
        // }

        // public Products GetProducts(string name)
        //{
        //    var post = _product.Collection.Find(Query.EQ("Name", name)).Single(); 
        //    return post;
        //}

        // public Products GetProduct(string Id)
        // {
        //     int _Id = Convert.ToInt32(Id);
        //     seoWebApplication.Models.Products query = (from e in _product.Collection.AsQueryable<Products>()
        //                                                where e.product_id == _Id
        //                                                select e).First();

        //     return query;
        // }
    }
}