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
    public class CartItemRepo : GeneralRepository<CartItem> , ICartItemRepo
    {
        private readonly VenomDbContext _context;
        public CartItemRepo(VenomDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
