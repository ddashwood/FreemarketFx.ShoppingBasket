namespace FreemarketFx.ShoppingBasket.Application.Requests.Baskets.CreateBasket;

public record CreateBasketResponse
(
    Guid BasketId,
    string BasketName
);