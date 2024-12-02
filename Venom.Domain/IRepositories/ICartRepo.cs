using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Domain.Entites;

namespace Venom.Domain.IRepositories
{
    public interface ICartRepo : IGeneralRepository<Cart>
    {
        Task RemoveCartItemsAsync(IEnumerable<CartItem> cartItems);

    }
}
