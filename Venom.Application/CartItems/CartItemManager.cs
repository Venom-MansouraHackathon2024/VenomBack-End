using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.CartItems.Dtos;
using Venom.Application.Dtos;
using Venom.Application.GeneralManager;
using Venom.Domain.Entites;
using Venom.Domain.IRepositories;

namespace Venom.Application.CartItems
{
    public class CartItemManager : GeneralManager<CartItem, ReadCartItemDto, AddCartItemDto, UpdateCartItemDto> , ICartItemManager
    {
        private readonly ICartItemRepo _cartItemrepository;
        private readonly IMapper _mapper;
        private readonly ICartRepo _cartRepo;
        private readonly IProductRepo _productRepo;

        public CartItemManager(ICartItemRepo cartItemrepository, IMapper mapper, ICartRepo cartRepo, IProductRepo productRepo) : base(cartItemrepository, mapper)
        {
            _cartItemrepository = cartItemrepository;
            _mapper = mapper;
            _cartRepo = cartRepo;
            _productRepo = productRepo;
        }

        public async override Task<GeneralResponseDto> GetByIdAsync(int id)
        {
            var result = await _cartItemrepository.GetAll(e => e.Id == id, p => p.Product)
                      .AsNoTracking()
                      .ProjectTo<ReadCartItemDto>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();

            if (result == null)
                return new GeneralResponseDto { IsSucceeded = false, Message = "no cartItem", StatusCode = 404 }; // Not Found

            return new GeneralResponseDto { IsSucceeded = true, Model = result, StatusCode = 200 };
        }

        public async Task<GeneralResponseDto> GetByCartIdAsync(int cartId)
        {
            var cartExists = _cartRepo.GetAll().Any(c => c.Id == cartId);
            if (cartExists == null)
                return new GeneralResponseDto { IsSucceeded = false, Message = "no cart with this id", StatusCode = 404 }; 

            return await base.GetAllByConditionAndIncludes(c => c.CartID == cartId, p => p.Product);
        }

     
    }
}
