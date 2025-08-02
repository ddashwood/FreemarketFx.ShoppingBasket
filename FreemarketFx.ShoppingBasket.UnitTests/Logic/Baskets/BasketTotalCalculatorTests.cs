using FreemarketFx.ShoppingBasket.Application.Logic.Baskets;
using FreemarketFx.ShoppingBasket.Models;
using Microsoft.Extensions.Options;
using Moq;

namespace FreemarketFx.ShoppingBasket.UnitTests.Logic.Baskets;

public class BasketTotalCalculatorTests
{
    [Fact]
    public void GetTotalWithoutVatTest()
    {
        var items = new List<BasketItem>
        {
            new() { BasePricePerItem = 1.23m, Quantity = 2},
            new() { BasePricePerItem = 2.34m, Quantity = 3}
        };

        var calculator = GetBasketTotalCalculator();

        var result = calculator.GetTotalWithoutVat(items, 0);

        Assert.Equal(9.48m, result);
    }

    [Fact]
    public void GetTotalWithVatRoundDownTest()
    {
        var items = new List<BasketItem>
        {
            new() { BasePricePerItem = 5.22m, Quantity = 1}
        };

        var calculator = GetBasketTotalCalculator();

        var result = calculator.GetTotalWithVat(items, 0);

        Assert.Equal(6.26m, result);
    }

    [Fact]
    public void GetTotalWithVatRoundUpTest()
    {
        var items = new List<BasketItem>
        {
            new() { BasePricePerItem = 5.23m, Quantity = 1}
        };

        var calculator = GetBasketTotalCalculator();

        var result = calculator.GetTotalWithVat(items, 0);

        Assert.Equal(6.28m, result);
    }

    [Fact]
    public void GetTotalWithDiscountWithoutVatTest()
    {
        var items = new List<BasketItem>
        {
            new() { BasePricePerItem = 1.23m, Quantity = 2},
            new() { BasePricePerItem = 2.34m, Quantity = 3, DiscountPercent = 10}
        };

        var calculator = GetBasketTotalCalculator();

        var result = calculator.GetTotalWithoutVat(items, 0);

        // Each of the second item is priced at £2.11 (£2.34, with a 10% discount, which is
        // £2.106, rounded to the nearest penny), for a total of £6.33.
        
        // Add the value of the first item, £1.23 x 2, for a total of £8.79.
        Assert.Equal(8.79m, result);
    }

    [Fact]
    public void GetTotalWithDiscountWithVatTest()
    {
        var items = new List<BasketItem>
        {
            new() { BasePricePerItem = 1.23m, Quantity = 2},
            new() { BasePricePerItem = 2.34m, Quantity = 3, DiscountPercent = 10}
        };

        var calculator = GetBasketTotalCalculator();

        var result = calculator.GetTotalWithVat(items, 0);

        // Each of the second item is priced at £2.11 (£2.34, with a 10% discount, which is
        // £2.106, rounded to the nearest penny), for a total of £6.33.

        // Add the value of the first item, £1.23 x 2, for a total of £8.79.

        // VAT is charged on this amount
        Assert.Equal(10.55m, result);
    }

    [Fact]
    public void GetTotalWithBasketDiscountWithoutVatTest()
    {
        var items = new List<BasketItem>
        {
            new() { BasePricePerItem = 1.23m, Quantity = 2},
            new() { BasePricePerItem = 2.34m, Quantity = 3, DiscountPercent = 10}
        };

        var calculator = GetBasketTotalCalculator();

        var result = calculator.GetTotalWithoutVat(items, 20);

        // Each of the second item is priced at £2.11 (£2.34, with a 10% discount, which is
        // £2.106, rounded to the nearest penny), for a total of £6.33.

        // The basket discount of 20% applies to the first item, giving a total
        // of £1.96 (£0.98 each)

        // Adding these together gives a grand total of 
        Assert.Equal(8.29m, result);
    }

    private BasketTotalCalculator GetBasketTotalCalculator()
    {
        var options = new BasketOptions { VatPercent = 20 };
        var mockOptions = new Mock<IOptions<BasketOptions>>();
        mockOptions.Setup(mock => mock.Value).Returns(options);
        return new(mockOptions.Object);
    }
}
