namespace FreemarketFx.ShoppingBasket.Models;

public class BasketShipping
{
    public Guid BasketShippingId { get; set; }
    public Guid BasketId { get; set; }
    public string CountryCode { get; set; } = "";
    public decimal ShippingCost { get; set; }

    public Basket Basket { get; set; } = null!;
}
