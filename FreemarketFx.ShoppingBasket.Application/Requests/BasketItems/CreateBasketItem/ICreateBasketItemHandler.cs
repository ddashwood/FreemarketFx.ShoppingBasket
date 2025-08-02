namespace FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBasketItem;

public interface ICreateBasketItemHandler
{
    Task<CreateBasketItemResponse> CreateBasketItemAsync(CreateBasketItemRequest request);
}
