using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Domain.Entites;
using Venom.Domain.IRepositories;
using Venom.infrastructure.Persistance;

namespace Venom.infrastructure.Repositories
{
    public class CartRepo : GeneralRepository<Cart>, ICartRepo
    {
        private readonly VenomDbContext _context;
        public CartRepo(VenomDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task RemoveCartItemsAsync(IEnumerable<CartItem> cartItems)
        {
            _context.CartItems.RemoveRange(cartItems);
            // await _context.SaveChangesAsync(); 
        }
    }
}
