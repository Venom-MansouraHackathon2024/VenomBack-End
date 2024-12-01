using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Domain.Entites;

namespace Venom.Domain.IRepositories
{
    public interface IProductRepo : IGeneralRepository<Product>
    {
        Task<Product> GetByIdAsync(int id);

    }
}
