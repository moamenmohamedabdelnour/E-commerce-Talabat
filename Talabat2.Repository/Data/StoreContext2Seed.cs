using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat2.Core.Entites;
using Talabat2.Core.Entites.Order_Aggregation;

namespace Talabat2.Repository.Data
{
    public static class StoreContext2Seed
    {
        //To Seeding Data Into DataBase
        public static async Task SeedAsync(StoreContext2 dbContext)
        {

            //To Not Each Run Enter Same Data
            if (!dbContext.DeliveryMethods.Any())
            {
                //To hold File Of Data
                var MethosDate = File.ReadAllText("../Talabat2.Repository/Data/DataSeed/delivery.json");
                //To Convert Data From Json File To List Can Use It in Program
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(MethosDate);
                if (DeliveryMethods?.Count > 0)
                {
                    foreach (var Deliverymethod in DeliveryMethods)
                    {
                        await dbContext.DeliveryMethods.AddAsync(Deliverymethod);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
            if (!dbContext.ProductBrands.Any())
            {
                //To hold File Of Data
                var brandsDate = File.ReadAllText("../Talabat2.Repository/Data/DataSeed/brands.json");
                //To Convert Data From Json File To List Can Use It in Program
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsDate);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        await dbContext.ProductBrands.AddAsync(brand);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
            if (!dbContext.ProductTypes.Any())
            {
                var typesDate = File.ReadAllText("../Talabat2.Repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesDate);
                if (types?.Count > 0)
                {
                    foreach (var type in types)
                    {
                        await dbContext.ProductTypes.AddAsync(type);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
            if (!dbContext.Products.Any())
            {
                var productsDate = File.ReadAllText("../Talabat2.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsDate);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await dbContext.Products.AddAsync(product);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }

        }
    }
}
