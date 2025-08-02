namespace FreemarketFx.ShoppingBasket.Application.Requests.Baskets.SetDiscount;

public record SetDiscountRequest
(
    Guid BasketId,
    int? DiscountPercent,
    string? DiscountCode
)
{
    public bool IsValid()
    {
        if (DiscountPercent == null && DiscountCode == null)
        {
            return true;
        }

        if (DiscountPercent != null && DiscountCode != null)
        {
            return true;
        }

        return false;
    }
}