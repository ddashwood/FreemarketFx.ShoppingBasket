using FluentValidation;
using FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBasketItem;
using FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.CreateBulkBasketItems;
using FreemarketFx.ShoppingBasket.Application.Requests.BasketItems.DeleteBasketItem;
using Microsoft.AspNetCore.Mvc;

namespace FreemarketFx.ShoppingBasket.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketItemController
(
    ICreateBasketItemHandler createBasketItemHandler,
    ICreateBulkBasketItemsHandler createBulkBasketItemsHandler,
    IDeleteBasketItemHandler deleteBasketItemHandler,
    IValidator<CreateBasketItemRequest> createBasketItemRequestValidator,
    IValidator<CreateBulkBasketItemsRequest> createBulkBasketItemsRequestValidator
) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateBasketItemResponse>> CreateBasketItem(CreateBasketItemRequest request)
    {
        var validationResult = createBasketItemRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var result = await createBasketItemHandler.CreateBasketItemAsync(request);

        return result.ResponseType switch
        {
            CreateBasketItemResponseType.Success => Created((string?) null, result.Details),
            CreateBasketItemResponseType.BasketNotFound => BadRequest("Basket ID not recognised"),
            _ => throw new InvalidOperationException("Invalid return value")
        };
    }

    [HttpPost("Bulk")]
    public async Task<ActionResult<CreateBulkBasketItemsResponse>> CreateBulkBasketItems(CreateBulkBasketItemsRequest request)
    {
        var validationResult = createBulkBasketItemsRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var result = await createBulkBasketItemsHandler.CreateBulkBasketItemsAsync(request);

        return result.ResponseType switch
        {
            CreateBulkBasketItemResponseType.Success => Created((string?)null, result.Details),
            CreateBulkBasketItemResponseType.BasketNotFound => BadRequest("Basket ID not recognised"),
            _ => throw new InvalidOperationException("Invalid return value")
        };
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBasketItem(Guid id)
    {
        var result = await deleteBasketItemHandler.DeleteBasketItemAsync(id);

        return result ? NoContent() : NotFound();
    }
}
