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
    public interface ICartManager : IGeneralManager<Cart, ReadCartDto, AddCartDto, UpdateCartDto>
    {
        Task<GeneralResponseDto> GetByUserEmailAsync(string userEmail);
        Task RemoveCartItems(IEnumerable<CartItem> cartItems);
        //  Task<GeneralRespons> UpdateCartItemsInCart(int id, List<UpdateCartItemsInCartDto> cartItemsInCart);
    }
}
