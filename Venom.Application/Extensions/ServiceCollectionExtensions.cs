using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.Auth;
using Venom.Application.CartItems;
using Venom.Application.Carts;
using Venom.Application.EmailService;
using Venom.Application.Otp;
using Venom.Application.Products;
using Venom.Application.Profile;
using Venom.Application.Reviews;
using Venom.Domain.Entites;
using Venom.Domain.IRepositories;

namespace Venom.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
            services.AddScoped<IEmailManager, EmailManager>();
            services.AddScoped<IReviewManager, ReviewManager>();
            services.AddScoped<IOtpManager, OtpManager>();  

            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IProfileManager, ProfileManager>();
            services.AddScoped<ICartManager, CartManager>();
            services.AddScoped<ICartItemManager, CartItemManager>();

            services.AddScoped<IProductManager,ProductManager>();
            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
            services.AddHttpContextAccessor();

        }
    }
}
