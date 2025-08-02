using FreemarketFx.ShoppingBasket.Models;

namespace FreemarketFx.ShoppingBasket.Application.Logic.Baskets;

public interface IBasketTotalCalculator
{
    decimal GetTotalWithoutVat(ICollection<BasketItem> basketItems, int basketDiscountPercent);
    decimal GetTotalWithVat(ICollection<BasketItem> basketItems, int basketDiscountPercent);
    decimal GetPricePerItemWithDiscount(BasketItem basketItem, int basketDiscountPercent);
}
