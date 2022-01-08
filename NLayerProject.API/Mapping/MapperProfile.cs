using AutoMapper;
using NLayerProject.API.DTOs;
using NLayerProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.API.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryWithProductDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductWithCategoryDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
        }
    }
}
