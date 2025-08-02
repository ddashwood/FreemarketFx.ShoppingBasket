namespace FreemarketFx.ShoppingBasket.Models;

public class Basket
{
    public Guid BasketId { get; set; }
    public string BasketName { get; set; } = "";
    public int? DiscountPercent { get; set; }
    public string? DiscountCode { get; set; }

    public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    public ICollection<BasketShipping> BasketShippings { get; set; } = new List<BasketShipping>();
}
