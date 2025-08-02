using FreemarketFx.ShoppingBasket.Database;
using FreemarketFx.ShoppingBasket.Models;

namespace FreemarketFx.ShoppingBasket.Application.Requests.Baskets.CreateBasket;

internal class CreateBasketHandler
(
    ShoppingBasketContext context
) : ICreateBasketHandler
{
    public async Task<CreateBasketResponse> CreateBasketAsync(CreateBasketRequest request)
    {
        var basket = new Basket { BasketName = request.BasketName };
        context.Baskets.Add(basket);
        await context.SaveChangesAsync();

        return new(basket.BasketId, basket.BasketName);
    }
}
