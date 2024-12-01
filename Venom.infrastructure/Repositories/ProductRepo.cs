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
    public class ProductRepo : GeneralRepository<Product>, IProductRepo
    {
        private readonly VenomDbContext _context;

        public ProductRepo(VenomDbContext context) : base(context)
        {
            _context = context;

        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Set<Product>().FindAsync(id);
        }


    }
}
