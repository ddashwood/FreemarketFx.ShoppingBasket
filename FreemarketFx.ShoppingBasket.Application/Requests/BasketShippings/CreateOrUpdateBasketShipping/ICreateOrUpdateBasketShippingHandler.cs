namespace FreemarketFx.ShoppingBasket.Application.Requests.BasketShippings.CreateOrUpdateBasketShipping;

public interface ICreateOrUpdateBasketShippingHandler
{
    Task<CreateOrUpdateBasketShippingResponse> CreateOrUpdateBasketShippingAsync(Guid basketId, string countryCode, CreateOrUpdateBasketShippingRequest request);
}
