using AdminPanel.Models;
using AutoMapper;
using Talabat2.Core.Entites;

namespace AdminPanel.Helpers
{
    public class MapsProfile:Profile
    {
        public MapsProfile()
        {
            CreateMap<Product,ProductViewModel>().ReverseMap();
        }
    }
}
