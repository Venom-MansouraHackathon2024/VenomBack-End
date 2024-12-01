using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.Dtos;
using Venom.Application.GeneralManager;
using Venom.Application.Products;
using Venom.Application.Reviews.Dtos;
using Venom.Domain.Entites;
using Venom.Domain.IRepositories;

namespace Venom.Application.Reviews
{
    public class ReviewManager : GeneralManager<Review, ReadReviewDto, AddReviewDto, UpdateReviewDto>, IReviewManager
    {
        private readonly IReviewRepo _reviewRepository;
        private readonly IProductRepo _productRepository;

        public ReviewManager(IMapper mapper, IProductRepo productRepository, IReviewRepo reviewRepository) : base(reviewRepository, mapper)
        {

            _productRepository = productRepository;
            _reviewRepository = reviewRepository;
        }

        public override async Task<GeneralResponseDto> GetAll()
        {
            return await base.GetAll(ve => ve.User, Version => Version.Product);
        }
        public override async Task<GeneralResponseDto> GetByIdAsync(int id)
        {
            return await base.GetAllByConditionAndIncludes(ve => ve.ReviewId == id, ve => ve.User, Version => Version.Product);
        }
        public async Task<GeneralResponseDto> GetAverageRatingAsync(int productId)
        {
            var x =  await _reviewRepository.CalculateAverageRatingAsync(productId);
            return new GeneralResponseDto { IsSucceeded = true, Model = x , StatusCode = 200};
        }

        public async Task<GeneralResponseDto> GetByProductIdAsync(int productid)
        {
            return await base.GetAllByConditionAndIncludes(ve => ve.ProductId == productid, ve => ve.User, Version => Version.Product);
        }

        public async Task<GeneralResponseDto> GetByUserIdAsync(string userId)
        {
            return await base.GetAllByConditionAndIncludes(ve => ve.UserId == userId, ve => ve.User, Version => Version.Product);
        }

        public async Task<GeneralResponseDto> GetProductReviewsAsync(int productId)
        {
            var reviews = await _reviewRepository.GetReviewsByProductIdAsync(productId);
            if(reviews != null)
            {
                return new GeneralResponseDto { IsSucceeded = false, Message = "no reviews in this product", StatusCode = 404 };
            }
            return new GeneralResponseDto { IsSucceeded = true, StatusCode = 200 , Model = reviews};


        }
    }
}
