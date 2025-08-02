using FluentValidation.TestHelper;
using FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBulkBasketItems;

namespace FreemarketFx.ShoppingBasket.UnitTests.Requests.BasketItems.CreateBulkBasketItems;

public class CreateBulkBasketItemsRequestValidatorTests
{
    [Theory]
    [InlineData(0, 500, $"{nameof(CreateBulkBasketItemsRequest.Items)}[0].{nameof(CreateBulkBasketItemRequest.DiscountPercent)}")]
    [InlineData(1, 500, null)]
    [InlineData(100, 500, null)]
    [InlineData(101, 500, $"{nameof(CreateBulkBasketItemsRequest.Items)}[0].{nameof(CreateBulkBasketItemRequest.DiscountPercent)}")]
    [InlineData(100, 501, $"{nameof(CreateBulkBasketItemsRequest.Items)}[0].{nameof(CreateBulkBasketItemRequest.Description)}")]
    public void ValidateTest(int discountPercent, int descriptionLength, string? errorPropertyName)
    {
        var validator = new CreateBulkBasketItemsRequestValidator();

        var request = new CreateBulkBasketItemsRequest
        (
            new Guid(),
            new List<CreateBulkBasketItemRequest>
            {
                new
                (
                    new string('x', descriptionLength),
                    5,
                    1,
                    discountPercent
                )
            }
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
