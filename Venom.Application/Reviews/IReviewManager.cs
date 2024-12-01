using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.Dtos;
using Venom.Application.GeneralManager;
using Venom.Application.Reviews.Dtos;
using Venom.Domain.Entites;
using Venom.Domain.IRepositories;

namespace Venom.Application.Reviews
{
    public interface IReviewManager : IGeneralManager<Review, ReadReviewDto, AddReviewDto, UpdateReviewDto>
    { 
        Task<GeneralResponseDto> GetAverageRatingAsync(int productId);
        Task<GeneralResponseDto> GetProductReviewsAsync(int productId);
        Task<GeneralResponseDto> GetByUserIdAsync(string userId);
        Task<GeneralResponseDto> GetByProductIdAsync(int productid);
    }
}
