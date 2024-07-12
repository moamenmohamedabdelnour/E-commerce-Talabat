using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat2.Core.Entites;
using Talabat2.Core.Repositories;

namespace Talabat2.Repository
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IDatabase _database;

        //As We Know How Will Create Any Of This Methods Without DbContext 
        //So First Create Configrution Of Redis in Constructor
        public BasketRepository(IConnectionMultiplexer redis)
        {
            //First Create Services In Progarm File To Allow To CLR Pass By DependencyInjection Object Of IConnectionMultiplexer
            //To Connect To Redis Server

            _database=redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketid)
        {
            return await _database.KeyDeleteAsync(basketid);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketid)
        {
            var basket = await _database.StringGetAsync(basketid);
            //StringGetAsync Return RedisValue It's JsonFile But Have Complex Attribute I Don't Need
            //So Will Deserialize It To CustomerBasket and In Controller Return It To BasketDto
            return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var createdOrUpdated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(1));
            if (!createdOrUpdated) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
