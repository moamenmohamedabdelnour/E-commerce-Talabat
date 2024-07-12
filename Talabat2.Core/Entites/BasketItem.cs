using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat2.Core.Entites
{
    //Each CustomerBasket Contains List BasketItem So We Create This Class To Add Products To Basket
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quentity { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}
