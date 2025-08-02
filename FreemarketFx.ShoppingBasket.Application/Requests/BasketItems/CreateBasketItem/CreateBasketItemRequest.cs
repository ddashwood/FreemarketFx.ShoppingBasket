namespace FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBasketItem;

public record CreateBasketItemRequest
(
    Guid BasketId,
    string Description,
    decimal BasePricePerItem,
    int Quantity = 1,
    int? DiscountPercent = null
);