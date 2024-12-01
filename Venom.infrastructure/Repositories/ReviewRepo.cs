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
    public class ReviewRepo : GeneralRepository<Review>, IReviewRepo
    {
        public ReviewRepo(VenomDbContext context) : base(context) { }

        public async Task<double> CalculateAverageRatingAsync(int productId)
        {
            var reviews = await _dbSet.Where(r => r.ProductId == productId).ToListAsync();
            if (!reviews.Any()) return 0;
            return reviews.Average(r => r.Rating);
        }
        public async Task<IQueryable<Review>> GetReviewsByProductIdAsync(int productId)
        {
            var reviews = await _dbSet.Where(r => r.ProductId == productId).ToListAsync();
            return reviews.AsQueryable();
        }
    }
}
