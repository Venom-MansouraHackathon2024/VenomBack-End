using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.Dtos;
using Venom.Application.GeneralManager;
using Venom.Application.Reviews.Dtos;
using Venom.Domain.Entites;
using Venom.Domain.IRepositories;

namespace Venom.Application.Products
{
    public class ProductManager : GeneralManager<Product, ReadProductDto, AddProductDto, UpdateProductDto>, IProductManager
    {

        private readonly IProductRepo _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ProductManager(IProductRepo repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
           : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<GeneralResponseDto> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) 
            return new GeneralResponseDto
            {
                Message = "no product with this id" ,
                StatusCode = 404,
                IsSucceeded = false
            }; ;

            var prodDto = new ReadProductDto
            {
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                SupplierId = product.SupplierId,
                SupplierName = product.Supplier.Name,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = GenerateImageUrl(product.ImageUrl)
            };

            return new GeneralResponseDto
            {
               Model = prodDto,
               StatusCode = 200,
               IsSucceeded = true
            };
        }

      
        private string GenerateImageUrl(string relativePath)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return $"http://{request.Host}/upload{relativePath}";
        }

        public async Task<GeneralResponseDto> GetByCategoryNameAsync(string categoryName)
        {
           
            var products = _repository.GetAll().Include(c=>c.Category);

            if (!products.Any())
            {
                return new GeneralResponseDto
                {
                    Model = null,
                    StatusCode = 404,
                    IsSucceeded = false,
                    Message = "No products found."
                };
            }


            var productDtos = products.Select(product => new ReadProductDto
            {
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                SupplierId = product.SupplierId,
                SupplierName = product.Supplier.Name,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = GenerateImageUrl(product.ImageUrl)
            }).ToList();

            return new GeneralResponseDto
            {
                Model = productDtos,
                StatusCode = 200,
                IsSucceeded = true
            };
        }

        public async Task<GeneralResponseDto> GetByPriceAsync(decimal price)
        {
             return await base.GetAllByConditionAndIncludes(p => p.Price == price, p => p.Category);
        }


        public async Task<GeneralResponseDto> GetByPriceInRangeAsync(decimal highPrice, decimal lowPrice)
        {
            return await base.GetAllByConditionAndIncludes(p => p.Price >= lowPrice && p.Price <= highPrice, p => p.Category);
        }


        public async Task<GeneralResponseDto> GetByProductNameAsync(string productName)
        {
            return await base.GetAllByConditionAndIncludes(
                p => p.Name == productName, p => p.Category);
        }

        public override async Task<GeneralResponseDto> AddAsync(AddProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

        
            //save one time after all changes (add product and add inventory)
            await _repository.SaveChangesAsync();

            var ReadProduct = _mapper.Map<ReadProductDto>(product);
            return new GeneralResponseDto { Model = ReadProduct, IsSucceeded = true, Message = "Product created successfully." };
        }


        public async Task<GeneralResponseDto> GetByPriceLessThanAsync(decimal highPrice)
        {
            return await base.GetAll(p => p.Price < highPrice, p => p.Category);
        }

        public async Task<GeneralResponseDto> GetByPriceGreaterThanAsync(decimal lowPrice)
        {
            return await base.GetAllByConditionAndIncludes(p => p.Price > lowPrice, p => p.Category);
        }
    }
}
