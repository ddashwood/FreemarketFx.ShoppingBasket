using FluentValidation;
using FreemarketFx.ShoppingBasket.Application.Logic.Baskets;
using FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBasketItem;
using FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBulkBasketItems;
using FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.DeleteBasketItem;
using FreemarketFx.ShoppingBasket.Application.Requests.Baskets.CreateBasket;
using FreemarketFx.ShoppingBasket.Application.Requests.Baskets.GetBasket;
using FreemarketFx.ShoppingBasket.Application.Requests.Baskets.SetDiscount;
using FreemarketFx.ShoppingBasket.Application.Requests.BasketShippings.CreateOrUpdateBasketShipping;
using FreemarketFx.ShoppingBasket.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreemarketFx.ShoppingBasket.Application;

public static class Startup
{
    public static IServiceCollection AddShoppingBasketApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICreateBasketHandler, CreateBasketHandler>();
        services.AddScoped<IGetBasketHandler, GetBasketHandler>();
        services.AddScoped<ICreateBasketItemHandler, CreateBasketItemHandler>();
        services.AddScoped<ICreateBulkBasketItemsHandler, CreateBulkBasketItemsHandler>();
        services.AddScoped<IDeleteBasketItemHandler, DeleteBasketItemHandler>();
        services.AddScoped<ISetDiscountHandler, SetDiscountHandler>();
        services.AddScoped<ICreateOrUpdateBasketShippingHandler, CreateOrUpdateBasketShippingHandler>();

        services.AddScoped<IBasketTotalCalculator, BasketTotalCalculator>();

        services.AddShoppingBasketDatabase(configuration);
        services.AddValidatorsFromAssemblyContaining(typeof(Startup));

        services.AddOptions<BasketOptions>()
            .BindConfiguration(BasketOptions.Basket);

        return services;
    }
}
