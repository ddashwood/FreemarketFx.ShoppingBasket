using FreemarketFx.ShoppingBasket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreemarketFx.ShoppingBasket.Database.Configurations;

internal class BasketShippingConfiguration : IEntityTypeConfiguration<BasketShipping>
{
    public void Configure(EntityTypeBuilder<BasketShipping> builder)
    {
        builder.ToTable("BasketShipping");
        builder.HasKey(bs => bs.BasketShippingId);
        builder.Property(bs => bs.ShippingCost).HasPrecision(7, 2);
        builder.Property(bs => bs.CountryCode).HasMaxLength(2);
    }
}
