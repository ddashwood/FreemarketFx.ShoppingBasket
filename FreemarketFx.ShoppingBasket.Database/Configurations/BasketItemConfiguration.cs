using FreemarketFx.ShoppingBasket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreemarketFx.ShoppingBasket.Database.Configurations;

internal class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.ToTable("BasketItem");
        builder.HasKey(bi => bi.BasketItemId);
        builder.Property(bi => bi.Description).HasMaxLength(500);
        builder.Property(bi => bi.BasePricePerItem).HasPrecision(7, 2);
        builder.ToTable(t => t.HasCheckConstraint("CHK_BasketItem_Quantity", "Quantity > 0"));
        builder.ToTable(t => t.HasCheckConstraint("CHK_BasketItem_BasePricePerItem", "BasePricePerItem > 0"));
        builder.ToTable(t => t.HasCheckConstraint("CHK_BasketItem_DiscountPercent", "DiscountPercent > 0 AND DiscountPercent <= 100"));
    }
}
