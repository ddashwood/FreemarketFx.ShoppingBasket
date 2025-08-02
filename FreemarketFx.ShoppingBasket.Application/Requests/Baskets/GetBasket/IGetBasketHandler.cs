namespace FreemarketFx.ShoppingBasket.Application.Requests.Baskets.GetBasket;

public interface IGetBasketHandler
{
    Task<GetBasketResponse?> GetBasketAsync(Guid basketId);
}
