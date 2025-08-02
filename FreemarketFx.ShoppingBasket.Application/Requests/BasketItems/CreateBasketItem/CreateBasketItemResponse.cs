namespace FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBasketItem;

public enum CreateBasketItemResponseType
{
    Success,
    BasketNotFound
}

public record CreateBasketItemResponseDetails
(
    Guid BasketItemId,
    Guid BasketId,
    string Description,
    decimal BasePricePerItem,
    int Quantity,
    int? DiscountPercent
);

public class CreateBasketItemResponse
{
    private CreateBasketItemResponse()
    {
    }

    public CreateBasketItemResponseType ResponseType { get; init; }
    public CreateBasketItemResponseDetails? Details { get; init; }

    public static CreateBasketItemResponse Success(CreateBasketItemResponseDetails details) =>
        new() { ResponseType = CreateBasketItemResponseType.Success, Details = details };

    public static CreateBasketItemResponse BasketNotFound() =>
        new() { ResponseType = CreateBasketItemResponseType.BasketNotFound };
}