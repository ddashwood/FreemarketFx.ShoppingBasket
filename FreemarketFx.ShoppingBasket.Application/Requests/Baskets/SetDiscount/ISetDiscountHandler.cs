namespace FreemarketFx.ShoppingBasket.Application.Requests.Baskets.SetDiscount;

public interface ISetDiscountHandler
{
    Task<SetDiscountResult> SetDiscountAsync(SetDiscountRequest request);
}
