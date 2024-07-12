using AutoMapper;
using Talabat2.APIs.Dtos;
using Talabat2.Core.Entites;

namespace Talabat2.APIs.Helpers
{
    //This Class To Resolve Picture Url From Product To ProductReturnDto And Choose Correct Path For This Pictures
    //Which allocate In This Host In wwwroot/images/pictures
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration=configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                //if Product PictureUrl Has Value return Base Of Host Which Will Catch It From AppSetting Which Take It From LaunchSetting
                //Put It In AppSetting To Be Dynamic
                return $"{configuration["ApiBaseUrl"]}{source.PictureUrl}";
            return string.Empty ;
        }
    }
}
