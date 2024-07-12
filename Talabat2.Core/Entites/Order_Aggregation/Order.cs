using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat2.Core.Entites.Order_Aggregation
{
    public class Order:BaseEntity
    {
        public Order() { }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail=buyerEmail;
            ShippingAddress=shippingAddress;
            DeliveryMethod=deliveryMethod;
            Items=items;
            SubTotal=subTotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }//Navegntional Prop
        public ICollection<OrderItem> Items { get; set;} = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal
            => SubTotal+DeliveryMethod.Cost;
        public string PaymentIntentId { set; get; }= string.Empty;
    }
}
