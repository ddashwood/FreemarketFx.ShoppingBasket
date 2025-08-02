using FreemarketFx.ShoppingBasket.Models;
using Microsoft.Extensions.Options;

namespace FreemarketFx.ShoppingBasket.Application.Logic.Baskets;

internal class BasketTotalCalculator
(
    IOptions<BasketOptions> options
) : IBasketTotalCalculator
{
    public decimal GetTotalWithoutVat(ICollection<BasketItem> basketItems, int basketDiscountPercent)
    {
        return basketItems.Sum(bi => bi.Quantity * GetPricePerItemWithDiscount(bi, basketDiscountPercent));
    }

    public decimal GetTotalWithVat(ICollection<BasketItem> basketItems, int basketDiscountPercent)
    {
        // Rounding rules: VAT is applied on the whole transaction. Amounts <0.5p are rounded down,
        // amounts >=0.5p are rounded up. Source: https://www.gov.uk/hmrc-internal-manuals/vat-trader-records/vatrec12030

        var totalWithoutVat = GetTotalWithoutVat(basketItems, basketDiscountPercent);
        var vat = Math.Round(totalWithoutVat * options.Value.VatPercent / 100, 2, MidpointRounding.AwayFromZero);

        return totalWithoutVat + vat;
    }

    public decimal GetPricePerItemWithDiscount(BasketItem basketItem, int basketDiscountPercent)
    {
        decimal basketDiscountMultiplier = 1 - basketDiscountPercent / 100m;

        if (basketItem.DiscountPercent == null)
        {
            return Math.Round(basketItem.BasePricePerItem * basketDiscountMultiplier, 2);
        }

        return Math.Round(basketItem.BasePricePerItem * (1 - basketItem.DiscountPercent.Value / 100m), 2);
    }
}
