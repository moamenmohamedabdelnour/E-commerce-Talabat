using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites.Order_Aggregation;

namespace Talabat2.Core.Specifications.Order_Spec
{
    public class OrderSpecififcation:BaseSpecification<Entites.Order_Aggregation.Order>
    {
        public OrderSpecififcation(string email):base(O=>O.BuyerEmail==email)     
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O=>O.Items);

            AddOrderByDescendeing(O => O.OrderDate);
        }
        public OrderSpecififcation(int id,string email) : base(O => O.BuyerEmail==email && O.Id==id) 
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
    }
}
