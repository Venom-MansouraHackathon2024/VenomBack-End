using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.Carts.Dtos;
using Venom.Application.Dtos;
using Venom.Application.GeneralManager;
using Venom.Domain.Entites;
using Venom.Domain.IRepositories;

namespace Venom.Application.Carts
{
    public class CartManager :  GeneralManager<Cart, ReadCartDto, AddCartDto, UpdateCartDto>, ICartManager
    {
        private readonly ICartRepo _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartManager(ICartRepo repository, IMapper mapper, UserManager<ApplicationUser> userManager) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<GeneralResponseDto> GetAll()
        {

            var resultList = await _repository.GetAll(u => u.User)
                        .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                        .ToListAsync();

          
            if (resultList == null)
            {
                return new GeneralResponseDto { IsSucceeded = false, Message = "no reviews in this product", StatusCode = 404 };
            }
            var dtoList = _mapper.Map<List<ReadCartDto>>(resultList);

            return new GeneralResponseDto { IsSucceeded = true, StatusCode = 200, Model = dtoList };

        }

        public async Task<GeneralResponseDto> GetByIdAsync(int id)
        {
            var idExsist = _repository.GetAll().Any(c => c.Id == id);
            if (idExsist)
            {
                var cart = await _repository.GetAll(c => c.Id == id, u => u.User)
                                                    .Include(c => c.CartItems)
                                                    .ThenInclude(ci => ci.Product)
                                                    .FirstOrDefaultAsync();
                return new GeneralResponseDto { IsSucceeded = true, Model = _mapper.Map<ReadCartDto>(cart), Message = $"{typeof(Cart).Name} retrieved successfully.", StatusCode = 200 };
            }

            return new GeneralResponseDto { IsSucceeded = false, Message = $"no cart with this id", StatusCode = 404 };
        }


        public async Task<GeneralResponseDto> GetByUserEmailAsync(string userEmail)
        {
            var userExists = await _userManager.FindByEmailAsync(userEmail);
            if (userExists == null)
            {
                return new GeneralResponseDto { IsSucceeded = false, Message = "No user with this id.", StatusCode = 404 }; // Not Found
            }

            var cart = await _repository.GetAll(x => x.User.Email == userEmail)
                .Include(c => c.CartItems)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                return new GeneralResponseDto { IsSucceeded = false, Message = "no cart", StatusCode = 404 }; // Not Found

            }
            var readDto = _mapper.Map<ReadCartDto>(cart);
            return new GeneralResponseDto { IsSucceeded = true, Model = readDto, StatusCode = 200 }; 

        }
        public async Task RemoveCartItems(IEnumerable<CartItem> cartItems)
        {
            await _repository.RemoveCartItemsAsync(cartItems);
            await _repository.SaveChangesAsync();
        }
    }
}
