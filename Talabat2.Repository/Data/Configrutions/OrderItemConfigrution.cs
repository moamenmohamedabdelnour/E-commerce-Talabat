using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites.Order_Aggregation;

namespace Talabat2.Repository.Data.Configrutions
{
    public class OrderItemConfigrution : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(OI => OI.Product, Product => Product.WithOwner());
            builder.Property(OI=>OI.Price).HasColumnType("decimal(18,2)");
        }
    }
}
