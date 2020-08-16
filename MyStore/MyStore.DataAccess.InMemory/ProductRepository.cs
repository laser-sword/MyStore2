using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyStore.Core.Models;

namespace MyStore.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit() {
            //puts the products into the cache before commiting
            cache["products"] = products;
        }

        public void Insert(Product p) {
            //this adds the prouct into the list, this method takes in an object of Product
            products.Add(p);
        }

        public void Update(Product product) {
            //first you need to find the product to update
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;

            }
            else {
                throw new Exception("Product NOT found!");
            }
        }

        public Product Find(string Id) {
            //same as Update just productToUpdat changed to just product
            Product product = products.Find(p => p.Id == p.Id);

            if (product != null)
            {
                return product;

            }
            else
            {
                throw new Exception("Product NOT found!");
            }
        }
        //a list that can be queried!
        public IQueryable<Product> Collection() {
            return products.AsQueryable();
        }
        //same as update , but changed to delete
        public void Delete(string Id) {
            Product productToDelete = products.Find(p => p.Id == p.Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);

            }
            else
            {
                throw new Exception("Product NOT found!");
            }
        }

    }
}
