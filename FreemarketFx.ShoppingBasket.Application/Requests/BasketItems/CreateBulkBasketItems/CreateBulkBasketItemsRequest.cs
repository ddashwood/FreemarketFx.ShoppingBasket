namespace FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBulkBasketItems;

public record CreateBulkBasketItemRequest
(
    string Description,
    decimal BasePricePerItem,
    int Quantity = 1,
    int? DiscountPercent = null
);

public record CreateBulkBasketItemsRequest
(
    Guid BasketId,
    ICollection<CreateBulkBasketItemRequest> Items
);
