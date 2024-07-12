﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat2.Core.Entites.Order_Aggregation
{
    public class ProductOrderItem
    {
        public ProductOrderItem() { }
        public ProductOrderItem(int productId, string productName, string pictureUrl)
        {
            ProductId=productId;
            ProductName=productName;
            PictureUrl=pictureUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}
