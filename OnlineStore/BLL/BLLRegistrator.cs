using BLL.Services;
using BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class BLLRegistrator
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services
                .AddTransient<IOrderService, OrderService>()
                .AddTransient<IProductService, ProductService>();

            return services;
        }
    }
}
