using AutoMapper;
using BLL.Models;
using WebAPILayer.Models;

namespace WebAPILayer.Configuration
{
    public class WebAPIAutoMapperProfile : Profile
    {
        public WebAPIAutoMapperProfile()
        {
            CreateMap<OrderViewModel, Order>().ReverseMap();
            CreateMap<ProductViewModel, Product>().ReverseMap();
        }
    }
}
