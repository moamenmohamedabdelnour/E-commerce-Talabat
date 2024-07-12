using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat2.Core.Entites.Order_Aggregation
{
    public class OrderItem:BaseEntity
    {
        public OrderItem() { }
        public OrderItem(ProductOrderItem product, decimal price, int quentity)
        {
            Product=product;
            Price=price;
            Quentity=quentity;
        }


        //Just For Enhansing Code 
        public ProductOrderItem Product { get; set; }
        public decimal Price { get; set; }
        public int Quentity { get; set;}
    }
}
