using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core;
using Talabat2.Core.Entites;
using Talabat2.Core.Entites.Order_Aggregation;
using Talabat2.Core.Repositories;
using Talabat2.Core.Services;
using Talabat2.Core.Specifications.Order_Spec;

namespace Talabat2.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;

        //private readonly IGenericRepository<Product> productRepository;
        //private readonly IGenericRepository<DeliveryMethod> deliveryMethodRepository;
        //private readonly IGenericRepository<Order> orderRepository;

        public OrderService(IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            this.basketRepository=basketRepository;
            this.unitOfWork=unitOfWork;
            //this.productRepository=productRepository;
            //this.deliveryMethodRepository=deliveryMethodRepository;
            //this.orderRepository=orderRepository;
        }
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            //1-Get Basket From Basket Repo
            var basket = await basketRepository.GetBasketAsync(BasketId);
           
            //2-Get Slected Item At Basket From Product Repo
            var OrderItems = new List<OrderItem>();
            if (basket?.Items?.Count>0)
            {
            foreach(var item in basket.Items)
                {
                    var product = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var OrderItemOredered = new ProductOrderItem(product.Id, product.Name, product.PictureUrl);
                    var orderItem=new OrderItem(OrderItemOredered,product.Price,item.Quentity);
                    OrderItems.Add(orderItem);
                }
                 
            }
            //3-Calculate SubTotal
            var subTotal = OrderItems.Sum(Item => Item.Price * Item.Quentity);
            //4-Get Delivery Method From DeliveryMethod Repo
            var deliveryMethod =await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);
            //5-Create Order
            var order = new Order(BuyerEmail,ShippingAddress,deliveryMethod,OrderItems,subTotal);
            //6-Add Order Locally
            await unitOfWork.Repository<Order>().Add(order);
            //7-Save Order To DB
            var result = await unitOfWork.Complete();
            if (result<=0) return null;
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods= await unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethods;
        }

        public async Task<Order> GetOrderByIdForUserAsync(int OrderId, string BuyerEmail)
        {
           var spec = new OrderSpecififcation(OrderId, BuyerEmail);
            var order= await unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail)
        {
            var spec = new OrderSpecififcation(BuyerEmail);
                var orders = await unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }
    }
}
