using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites;
using Talabat2.Core.Entites.Order_Aggregation;

namespace Talabat2.Repository.Data
{
    public class StoreContext2:DbContext
    {
        //To Retrive Object Of Options Of StoreContext2 Have Configrution To Base By DI
        //So Go To Program To Service 
        //Don't Forget To Add Refrence Of (Repository Layer) TO (Apis Layer)
        public StoreContext2(DbContextOptions<StoreContext2>options):base(options)
        { 
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //To Apply All Configrutions in This Project Which Implement IEntityTypeConfigrution<> Interface
           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        //Add DbSet To Entity Will Be Tabels
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrdersItems { get; set;}
        public DbSet<DeliveryMethod> DeliveryMethods { get; set;}
    }
}
