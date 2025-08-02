using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreemarketFx.ShoppingBasket.Database;

public static class Startup
{
    public static IServiceCollection AddShoppingBasketDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ShoppingBasketContext>(options => options.UseSqlServer(configuration.GetConnectionString("ShoppingBasket")));

        return services;
    }
}
