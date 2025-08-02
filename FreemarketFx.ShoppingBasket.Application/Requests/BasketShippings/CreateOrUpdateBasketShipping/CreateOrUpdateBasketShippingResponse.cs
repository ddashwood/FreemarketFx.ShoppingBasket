namespace FreemarketFx.ShoppingBasket.Application.Requests.BasketShippings.CreateOrUpdateBasketShipping;

public record CreateOrUpdateBasketShippingResponse
(
    Guid BasketShippingId,
    string CountryCode,
    decimal ShippingCost
);
