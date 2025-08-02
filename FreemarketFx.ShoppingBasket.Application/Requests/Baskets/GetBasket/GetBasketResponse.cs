namespace FreemarketFx.ShoppingBasket.Application.Requests.Baskets.GetBasket;

public record CreateBasketResponseItem
(
    Guid BasketItemId,
    string Description,
    decimal BasePricePerItem,
    decimal PricePerItemWithDiscount,
    int Quantity,
    int? ItemDiscountPercent,
    decimal TotalPrice
);

public record CreateBasketResponseShipping
(
    Guid BasketShippingId,
    string CountryCode,
    decimal ShippingCost
);

public record GetBasketResponse
(
    Guid BasketId,
    string BasketName,
    decimal BasketPriceWithoutVat,
    decimal BasketPriceWithVat,
    ICollection<CreateBasketResponseItem> Items,
    decimal? BasketDiscountPercent,
    string? BasketDiscountCode,
    ICollection<CreateBasketResponseShipping> Shippings
);