using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Domain.Entites;

namespace Venom.Domain.IRepositories
{
    public interface IReviewRepo : IGeneralRepository<Review>
    {
        Task<double> CalculateAverageRatingAsync(int productId);
        Task<IQueryable<Review>> GetReviewsByProductIdAsync(int productId);
    }
}
