using Application.Contracts;
using Core.DomainModels.BasketModel;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Basket;

[ApiController]
[Route("[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpPost("CreateBasket")]
    public async Task<ActionResult<string>> CreateBasket()
    {
        var basketId = await _basketService.CreateBasketAsync();
        return Ok(basketId);
    }

    [HttpGet("GetBasket")]
    public async Task<ActionResult<BasketSummaryDto>> GetBasket()
    {
        var result = await _basketService.GetBasketAsync();
        return Ok(result);
    }

    // [HttpPost("AddItemsToBasket")]
    // public async Task<IActionResult> AddItemsToBasket([FromBody] List<AddItemToBasketDto> model, [FromQuery] string? sessionid = null)
    // {
    //     await _basketService.AddItemsToBasketAsync(model, sessionid);
    //     return Ok();
    // }

    // [HttpDelete("RemoveItem/{itemid}/{sessionid?}")]
    // public async Task<IActionResult> RemoveItemFromBasket(string itemid, string? sessionid = null)
    // {
    //     await _basketService.RemoveItemFromBasketAsync(itemid, sessionid);
    //     return Ok();
    // }
}