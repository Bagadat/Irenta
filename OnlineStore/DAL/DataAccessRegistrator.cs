using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class DataAccessRegistrator
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration, string sectionName)
        {
            var connectionString = configuration.GetConnectionString(sectionName);

            return services
                .AddDbContext<OnlineStoreDbContext>(options => options.UseSqlServer(connectionString))
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
