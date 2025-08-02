using FreemarketFx.ShoppingBasket.Database;
using FreemarketFx.ShoppingBasket.Models;
using Microsoft.EntityFrameworkCore;

namespace FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBasketItem;

internal class CreateBasketItemHandler
(
    ShoppingBasketContext context
) : ICreateBasketItemHandler
{
    public async Task<CreateBasketItemResponse> CreateBasketItemAsync(CreateBasketItemRequest request)
    {
        if (!await context.Baskets.AnyAsync(b => b.BasketId == request.BasketId))
        {
            return CreateBasketItemResponse.BasketNotFound();
        }

        var item = new BasketItem
        {
            BasketId = request.BasketId,
            Description = request.Description,
            BasePricePerItem = request.BasePricePerItem,
            Quantity = request.Quantity,
            DiscountPercent = request.DiscountPercent,
        };

        context.BasketItems.Add(item);
        await context.SaveChangesAsync();

        return CreateBasketItemResponse.Success(
            new(item.BasketItemId, item.BasketId, item.Description, item.BasePricePerItem, item.Quantity, item.DiscountPercent)
        );
    }
}
