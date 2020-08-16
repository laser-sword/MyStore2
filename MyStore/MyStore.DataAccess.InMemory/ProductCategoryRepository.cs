using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyStore.Core.Models;

namespace MyStore.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            //puts the products into the cache before commiting
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            //this adds the prouct into the list, this method takes in an object of Product
            productCategories.Add(p);
        }

        public void Update(ProductCategory productCategory)
        {
            //first you need to find the product to update
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;

            }
            else
            {
                throw new Exception("Product Category NOT found!");
            }
        }

        public ProductCategory Find(string Id)
        {
            //same as Update just productToUpdat changed to just product
            ProductCategory productCategory = productCategories.Find(p => p.Id == p.Id);

            if (productCategory != null)
            {
                return productCategory;

            }
            else
            {
                throw new Exception("Product Category NOT found!");
            }
        }
        //a list that can be queried!
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }
        //same as update , but changed to delete
        
        
        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == p.Id);

            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);

            }
            else
            {
                throw new Exception("Product NOT found!");
            }
        }
    }
}
