using AutoMapper;
using BLL.Models;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Configuration
{
    public class BLLAutoMapperProfile : Profile
    {
        public BLLAutoMapperProfile()
        {
            CreateMap<Order, OrderDTO>().ForMember("FullName", o => o.MapFrom(c => c.Name + " " + c.Surname)).ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
