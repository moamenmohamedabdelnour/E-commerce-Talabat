using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites;

namespace Talabat2.Core.Specifications
{
    public class ProductWithFilterationForCountSpecification:BaseSpecification<Product>
    {
        public ProductWithFilterationForCountSpecification(ProductSpecParams productSpec)//for GetAll Without Where
           : base(P =>
           (string.IsNullOrEmpty(productSpec.Search)||P.Name.ToLower().Contains(productSpec.Search))&&
           (!productSpec.BrandId.HasValue||P.ProductBrandId==productSpec.BrandId)&&
          
           (!productSpec.TypeId.HasValue || P.ProductTypeId == productSpec.TypeId))
        


        {
           
        }
    }
}
