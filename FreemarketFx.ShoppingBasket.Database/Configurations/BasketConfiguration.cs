using FreemarketFx.ShoppingBasket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreemarketFx.ShoppingBasket.Database.Configurations;

internal class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.ToTable("Basket");
        builder.HasKey(b => b.BasketId);
        builder.Property(b => b.BasketName).HasMaxLength(100);
        builder.Property(b => b.DiscountCode).HasMaxLength(20);
        builder.ToTable(t => t.HasCheckConstraint("CHK_Basket_DiscountPercent", "DiscountPercent > 0 AND DiscountPercent <= 100"));

        builder.HasMany(b => b.BasketItems).WithOne(bi => bi.Basket).HasForeignKey(bi => bi.BasketId);
        builder.HasMany(b => b.BasketShippings).WithOne(bs => bs.Basket).HasForeignKey(bs => bs.BasketId);
    }
}
