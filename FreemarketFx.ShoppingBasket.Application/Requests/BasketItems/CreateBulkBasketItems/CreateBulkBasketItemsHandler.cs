using FreemarketFx.ShoppingBasket.Database;
using FreemarketFx.ShoppingBasket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBulkBasketItems;

internal class CreateBulkBasketItemsHandler
(
    ShoppingBasketContext context,
    ILogger<CreateBulkBasketItemsHandler> logger
) : ICreateBulkBasketItemsHandler
{
    public async Task<CreateBulkBasketItemsResponse> CreateBulkBasketItemsAsync(CreateBulkBasketItemsRequest request)
    {
        if (!await context.Baskets.AnyAsync(b => b.BasketId == request.BasketId))
        {
            return CreateBulkBasketItemsResponse.BasketNotFound();
        }

        var results = new List<CreateBulkBasketItemResponseDetails>();

        using (var tran = await context.Database.BeginTransactionAsync())
        {
            foreach (var bulkItem in request.Items)
            {
                var item = new BasketItem
                {
                    BasketId = request.BasketId,
                    Description = bulkItem.Description,
                    BasePricePerItem = bulkItem.BasePricePerItem,
                    Quantity = bulkItem.Quantity,
                    DiscountPercent = bulkItem.DiscountPercent,
                };

                context.BasketItems.Add(item);
                await context.SaveChangesAsync();

                results.Add(new(
                    item.BasketItemId, item.BasketId, item.Description, item.BasePricePerItem, item.Quantity, item.DiscountPercent
                ));
            }

            await tran.CommitAsync();

            foreach (var result in results)
            {
                logger.LogInformation("Added {itemDescription} to basket {basketId}", result.Description, result.BasketId);
            }

            return CreateBulkBasketItemsResponse.Success(results);
        }
    }
}
