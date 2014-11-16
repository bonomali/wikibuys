using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using seoWebApplication.DocumentDbModels;
using seoWebApplication.DocumentDbServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace seoWebApplication.DocumentDbServices
{
    public class ProductsService  
    {
        public DocumentClient Client;
         
        public DocumentCollection Collection;
         public ProductsService()
        {
            
            Client = DocumentDBFactory.Client;
        }

         public static List<seoWebApplication.Data.product> GetProducts()
        {
            DocumentCollection collection = DocumentDBFactory.ReadOrCreateCollection(DocumentDBFactory.ReadOrCreateDatabase().SelfLink, "Product");

            return DocumentDBFactory.Client.CreateDocumentQuery<seoWebApplication.Data.product>(collection.DocumentsLink)
                      
                       .AsEnumerable()
                       .ToList<seoWebApplication.Data.product>();
        }

        public static async Task<Document> CreateItemAsync(seoWebApplication.Data.product product)
        {
            DocumentCollection collection = DocumentDBFactory.ReadOrCreateCollection(DocumentDBFactory.ReadOrCreateDatabase().SelfLink, "Product");

            return await DocumentDBFactory.Client.CreateDocumentAsync(collection.SelfLink, product);
        }

        public static seoWebApplication.Data.product GetProduct(string id)
        {
            DocumentCollection collection = DocumentDBFactory.ReadOrCreateCollection(DocumentDBFactory.ReadOrCreateDatabase().SelfLink, "Product");

            return DocumentDBFactory.Client.CreateDocumentQuery<seoWebApplication.Data.product>(collection.DocumentsLink)
                        .Where(d => d.product_id.Equals(id))
                        .AsEnumerable()
                        .FirstOrDefault();
        }

        public static async Task<Document> UpdateProductAsync(seoWebApplication.Data.product product)
        {
            DocumentCollection collection = DocumentDBFactory.ReadOrCreateCollection(DocumentDBFactory.ReadOrCreateDatabase().SelfLink, "Product");

            Document doc = DocumentDBFactory.Client.CreateDocumentQuery(collection.DocumentsLink)
                                .Where(d => d.Id.Equals(product.product_id))
                                .AsEnumerable()
                                .FirstOrDefault();

            return await DocumentDBFactory.Client.ReplaceDocumentAsync(doc.SelfLink, product);
        }
    }
}