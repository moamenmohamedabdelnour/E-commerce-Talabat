using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites;

namespace Talabat2.Core.Specifications
{
    public class ProductSpecification:BaseSpecification<Product>
    {
        //sorting Will be With GetAll If Found Becouse it's Optional 
        //And Go TO ISpecification To Create Expression For Orderby And OrderbyDesc By name Or Price
        //to Filter Withn Brandid And TypeId Should Pass There Value To where Linq Operation
        //And it Here In Creitira Expression So will Make ShainingConstructor To Allow Creitira
        public ProductSpecification(ProductSpecParams productSpec)//for GetAll Without Where
            :base(P=>
            (string.IsNullOrEmpty(productSpec.Search)||P.Name.ToLower().Contains(productSpec.Search))&&
            (!productSpec.BrandId.HasValue||P.ProductBrandId==productSpec.BrandId)&&
            //if brandid Has Value First Condition Will Be True When say !True So Will Be False => P.ProductBrandId==brandid
            (!productSpec.TypeId.HasValue || P.ProductTypeId == productSpec.TypeId))
            //if typeid Has Value First Condition Will Be True When say !True So Will Be False => P.ProductTypeId==typeid
            //else 
            //(P=>true&&true) it Will Skip Filtaration


        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);

            if (!string.IsNullOrEmpty(productSpec.Sort))
            {
                //OrderTo Kind Of Sorting Will Create Lambda Expression And Send It To 2 Functions In BaseSpecification
                switch (productSpec.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescendeing(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }

            ApplyPagination(productSpec.PageSize*(productSpec.PageIndex-1),productSpec.PageSize);
        }
        public ProductSpecification(int id):base(P=>P.Id==id) //For GetById 
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }

    }
}
