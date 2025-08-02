namespace FreemarketFx.ShoppingBasket.Models;

public class BasketItem
{
    public Guid BasketItemId { get; set; }
    public Guid BasketId { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; } = "";
    public decimal BasePricePerItem { get; set; }
    public int? DiscountPercent { get; set; }

    public Basket Basket { get; set; } = null!;
}
