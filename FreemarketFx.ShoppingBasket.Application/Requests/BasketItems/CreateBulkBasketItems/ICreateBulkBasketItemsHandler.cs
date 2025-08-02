namespace FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBulkBasketItems;

public interface ICreateBulkBasketItemsHandler
{
    Task<CreateBulkBasketItemsResponse> CreateBulkBasketItemsAsync(CreateBulkBasketItemsRequest request);
}
