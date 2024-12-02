using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.CartItems.Dtos;
using Venom.Application.Dtos;
using Venom.Application.GeneralManager;
using Venom.Domain.Entites;

namespace Venom.Application.CartItems
{
    public interface ICartItemManager : IGeneralManager<CartItem, ReadCartItemDto, AddCartItemDto, UpdateCartItemDto>
    {
        Task<GeneralResponseDto> GetByCartIdAsync(int cartId);
    }
}
