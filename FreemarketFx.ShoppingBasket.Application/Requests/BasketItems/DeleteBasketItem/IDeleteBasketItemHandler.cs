namespace FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.DeleteBasketItem;

public interface IDeleteBasketItemHandler
{
    Task<bool> DeleteBasketItemAsync(Guid basketItemId);
}
