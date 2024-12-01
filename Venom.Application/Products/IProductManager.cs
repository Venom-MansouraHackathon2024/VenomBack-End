using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.Dtos;
using Venom.Application.GeneralManager;
using Venom.Application.Reviews.Dtos;
using Venom.Domain.Entites;

namespace Venom.Application.Products
{
    public interface IProductManager:  IGeneralManager<Product, ReadProductDto, AddProductDto, UpdateProductDto>
    {
        Task<GeneralResponseDto> GetProductByIdAsync(int id);
        Task<GeneralResponseDto> GetByCategoryNameAsync(string categoryName);
        Task<GeneralResponseDto> GetByProductNameAsync(string ProductName);
        Task<GeneralResponseDto> GetByPriceAsync(decimal price);
        Task<GeneralResponseDto> GetByPriceInRangeAsync(decimal highPrice, decimal lowPrice);
        Task<GeneralResponseDto> GetByPriceLessThanAsync(decimal highPrice);
        Task<GeneralResponseDto> GetByPriceGreaterThanAsync(decimal lowPrice);

    }
}

