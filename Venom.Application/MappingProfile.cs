﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.Profiles.Dtos;
using Venom.Application.Reviews.Dtos;
using Venom.Domain.Entites;

namespace Venom.Application
{
    public class MappingProfile : AutoMapper.Profile
    {

        public MappingProfile() {

            CreateMap<ProfileReadDto, ApplicationUser>().ReverseMap();
            CreateMap<ProfileUpdateDto, ApplicationUser>().ReverseMap();

            CreateMap<Product, ReadProductDto>()
                          .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                          .ReverseMap();
            CreateMap<AddProductDto, Product>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ReverseMap();

            CreateMap<UpdateProductDto, Product>()
                .ReverseMap();



            CreateMap<Review, ReadReviewDto>()
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
              .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<Review, UpdateReviewDto>().ReverseMap();
            CreateMap<Review, AddReviewDto>().ReverseMap();

        }
    }
}