using FreemarketFx.ShoppingBasket.Database;
using Microsoft.EntityFrameworkCore;

namespace FreemarketFx.ShoppingBasket.Application.Requests.Baskets.SetDiscount;

internal class SetDiscountHandler
(
    ShoppingBasketContext context
) : ISetDiscountHandler
{
    public async Task<SetDiscountResult> SetDiscountAsync(SetDiscountRequest request)
    {
        if (!request.IsValid())
        {
            return SetDiscountResult.InvalidRequest;
        }

        var updateCount = await context.Baskets.Where(b => b.BasketId == request.BasketId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(b => b.DiscountPercent, request.DiscountPercent)
                       .SetProperty(b => b.DiscountCode, request.DiscountCode));

        return updateCount > 0 ? SetDiscountResult.Success : SetDiscountResult.BasketNotFound;
    }
}
