using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.Carts;
using Venom.Domain.Entites;
using Venom.Domain.IRepositories;
using Venom.infrastructure.Persistance;
using Venom.infrastructure.Repositories;

namespace Venom.infrastructure.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<VenomDbContext>(options
                => options.UseSqlServer(connectionString)
               .EnableSensitiveDataLogging());

            services.AddScoped<IprofileRepo, ProfileRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IProductRepo,ProductRepo>();
            services.AddScoped<ICartItemRepo , CartItemRepo>();
            services.AddScoped<IReviewRepo , ReviewRepo>();
            services.AddScoped<ICartRepo, CartRepo>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<VenomDbContext>()
            .AddDefaultTokenProviders();


        }
    }
}
