using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat2.Core.Entites.Order_Aggregation
{
    public class DeliveryMethod:BaseEntity
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, decimal cost, string description, string deliveryTime)
        {
            ShortName=shortName;
            Cost=cost;
            Description=description;
            DeliveryTime=deliveryTime;
        }

        //DM=>Order 1=>M
        //Will Be Saved To DB 
        public string ShortName { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
    }
}
