using FluentValidation;

namespace FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBulkBasketItems;

internal class CreateBulkBasketItemRequestValidator : AbstractValidator<CreateBulkBasketItemRequest>
{
    public CreateBulkBasketItemRequestValidator()
    {
        RuleFor(cbi => cbi.DiscountPercent).GreaterThan(0);
        RuleFor(cbi => cbi.DiscountPercent).LessThanOrEqualTo(100);
        RuleFor(cbi => cbi.Description).MaximumLength(500);
    }
}

public class CreateBulkBasketItemsRequestValidator : AbstractValidator<CreateBulkBasketItemsRequest>
{
    public CreateBulkBasketItemsRequestValidator()
    {
        RuleForEach(cbbi => cbbi.Items).SetValidator(new CreateBulkBasketItemRequestValidator());
    }
}
