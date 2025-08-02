using FluentValidation.TestHelper;
using FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBasketItem;

namespace FreemarketFx.ShoppingBasket.UnitTests.Requests.BasketItems.CreateBasketItem;

public class CreateBasketItemRequestValidatorTests
{
    [Theory]
    [InlineData(0, 500, nameof(CreateBasketItemRequest.DiscountPercent))]
    [InlineData(1, 500, null)]
    [InlineData(100, 500, null)]
    [InlineData(101, 500, nameof(CreateBasketItemRequest.DiscountPercent))]
    [InlineData(100, 501, nameof(CreateBasketItemRequest.Description))]
    public void ValidateTest(int discountPercent, int descriptionLength, string? errorPropertyName)
    {
        var validator = new CreateBasketItemRequestValidator();

        var request = new CreateBasketItemRequest
        (
            new Guid(),
            new string('x', descriptionLength),
            5,
            1,
            discountPercent
        );

        var result = validator.TestValidate(request);

        if (errorPropertyName == null)
        {
            result.ShouldNotHaveAnyValidationErrors();
        }
        else
        {
            result.ShouldHaveValidationErrorFor(errorPropertyName);
        }
    }
}
