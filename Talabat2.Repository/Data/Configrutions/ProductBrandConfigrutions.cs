using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites;

namespace Talabat2.Repository.Data.Configrutions
{
    public class ProductBrandConfigrutions : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.Property(PB => PB.Name).IsRequired();
            builder.HasIndex(PB=>PB.Name).IsUnique();
        }
    }
}
