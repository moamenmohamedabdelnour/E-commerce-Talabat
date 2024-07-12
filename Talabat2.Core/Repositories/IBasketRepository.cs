using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites;

namespace Talabat2.Core.Repositories
{
    public interface IBasketRepository
    {//Signatures For Methods Will Using In Basket

        Task<CustomerBasket> GetBasketAsync(string basketid);
        //This Function Will Use To Update Basket if Founded Or Create It If Not
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        //return Bool If Deleted Successfully Or Not
        Task<bool> DeleteBasketAsync(string basketid);
        
        
        //Go To Repository Layer To Create Implementation Of BasketRepo
    }
}
