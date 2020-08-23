using MyStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.DataAccess.SQL
{
   public class DataContext : DbContext
    {
        public DataContext()
            : base("DefaultConnection") { 
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; } 

        public DbSet<Basket> BAskets { get; set; }
        
        public DbSet<BasketItem> BasketItems { get; set; }
    }
}
