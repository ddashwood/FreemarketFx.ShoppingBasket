using FreemarketFx.ShoppingBasket.Models;
using Microsoft.EntityFrameworkCore;

namespace FreemarketFx.ShoppingBasket.Database;

public class ShoppingBasketContext : DbContext
{
    public ShoppingBasketContext(DbContextOptions<ShoppingBasketContext> options)
        : base(options)
    {
    }

    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<BasketShipping> BasketShippings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
