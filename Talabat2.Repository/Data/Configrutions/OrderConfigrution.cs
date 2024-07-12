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
    public class OrderConfigrution : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, OS => OS.WithOwner());
            builder.Property(O => O.Status)
                .HasConversion(
                OS => OS.ToString(),
                OS => (OrderStatus)Enum.Parse(typeof(OrderStatus), OS));
            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");
            builder.HasOne(O => O.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
        }
    }
}
