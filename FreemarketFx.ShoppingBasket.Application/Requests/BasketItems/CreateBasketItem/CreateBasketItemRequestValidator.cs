using FluentValidation;

namespace FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBasketItem;

public class CreateBasketItemRequestValidator : AbstractValidator<CreateBasketItemRequest>
{
    public CreateBasketItemRequestValidator()
    {
        RuleFor(cbi => cbi.DiscountPercent).GreaterThan(0);
        RuleFor(cbi => cbi.DiscountPercent).LessThanOrEqualTo(100);
        RuleFor(cbi => cbi.Description).MaximumLength(500);
    }
}
