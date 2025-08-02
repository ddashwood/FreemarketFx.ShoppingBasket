namespace FreemarketFx.ShoppingBasket.Application.Requests.Baskets.CreateBasket;

public interface ICreateBasketHandler
{
    Task<CreateBasketResponse> CreateBasketAsync(CreateBasketRequest request);
}
