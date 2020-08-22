using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyStore.Core.Contracts;
using MyStore.Core.Models;
using MyStore.Core.ViewModels;
using MyStore.DataAccess.InMemory;

namespace MyStore.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        //create an instance of the Product Repository
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
       
       

        //crteate an instance of the Productmanagercontroller which isitiates that Product Repository
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext ) {
            context = productContext;
            productCategories = productCategoryContext;

        }

        // GET: ProductManager
        public ActionResult Index()
        {
            //get data from collection and turn it into a list 
            //and send back into the view
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create() {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();


            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
            
        }
        [HttpPost]
        public ActionResult Create(Product product) {
            if (!ModelState.IsValid)
            {
                return View(product);

            }
            else {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);

            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
            }
        }

            [HttpPost]
            public ActionResult Edit(Product product, string Id) {
            //find item in database iwth ID
            Product productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else {
                if (!ModelState.IsValid) {
                    return View(product);
                }
                //manually update product
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                //commit those changes
                context.Commit();

                return RedirectToAction("Index");

            }
        }
        public ActionResult Delete(string Id) {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id) {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
            
        
    }
}