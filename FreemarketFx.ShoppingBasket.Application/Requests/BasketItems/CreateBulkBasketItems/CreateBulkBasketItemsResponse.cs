namespace FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBulkBasketItems;

public enum CreateBulkBasketItemResponseType
{
    Success,
    BasketNotFound
}

public record CreateBulkBasketItemResponseDetails
(
    Guid BasketItemId,
    Guid BasketId,
    string Description,
    decimal BasePricePerItem,
    int Quantity,
    int? DiscountPercent
);

public class CreateBulkBasketItemsResponse
{
    private CreateBulkBasketItemsResponse()
    {
    }

    public CreateBulkBasketItemResponseType ResponseType { get; init; }
    public IEnumerable<CreateBulkBasketItemResponseDetails>? Details { get; init; }

    public static CreateBulkBasketItemsResponse Success(IEnumerable<CreateBulkBasketItemResponseDetails> details) =>
        new() { ResponseType = CreateBulkBasketItemResponseType.Success, Details = details };

    public static CreateBulkBasketItemsResponse BasketNotFound() =>
        new() { ResponseType = CreateBulkBasketItemResponseType.BasketNotFound };
}