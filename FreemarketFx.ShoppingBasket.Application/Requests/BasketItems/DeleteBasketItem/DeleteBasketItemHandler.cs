using FreemarketFx.ShoppingBasket.Database;
using Microsoft.EntityFrameworkCore;

namespace FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.DeleteBasketItem;

internal class DeleteBasketItemHandler
(
    ShoppingBasketContext context
) : IDeleteBasketItemHandler
{
    public async Task<bool> DeleteBasketItemAsync(Guid basketItemId)
    {
        var deletedCount = await context.BasketItems.Where(bi => bi.BasketItemId == basketItemId).ExecuteDeleteAsync();

        return deletedCount > 0;
    }
}
