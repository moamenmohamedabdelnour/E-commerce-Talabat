using Microsoft.Extensions.Configuration;
using Stripe;
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
using Product = Talabat2.Core.Entites.Product;
namespace Talabat2.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration configuration;
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IConfiguration configuration,IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            this.configuration=configuration;
            this.basketRepository=basketRepository;
            this.unitOfWork=unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketid)
        {
            StripeConfiguration.ApiKey = configuration["StripeSetting:Secretkey"];
            var basket=await basketRepository.GetBasketAsync(basketid);
            if (basket == null) return null;
            var ShippingPrice = 0.0m;
            if(basket.DeliveryMethodId.HasValue) 
            {
                var deliveryMethod= await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                ShippingPrice=deliveryMethod.Cost;
                basket.ShippingCost=deliveryMethod.Cost;
            }
            if(basket?.Items?.Count() > 0)
            {
                foreach(var item in basket.Items)
                {
                    var product = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    if(item.Price!=product.Price)
                    {
                        item.Price = product.Price;
                    }

                }
            }

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))//create Payment Intent
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(I => I.Price*I.Quentity*100)+(long)ShippingPrice*100,
                    Currency="usd",
                    PaymentMethodTypes=new List<string>() { "card" }
                };
                paymentIntent=await service.CreateAsync(options);
                basket.PaymentIntentId=paymentIntent.Id;
                basket.ClientSecret=paymentIntent.ClientSecret;
            }
            else//Update PaymentIntent
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(I => I.Price*I.Quentity*100)+(long)ShippingPrice*100,

                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }
            await basketRepository.UpdateBasketAsync(basket);
            return basket;
        }
    }
}
