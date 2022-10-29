using Core.DomainModels.BasketModel;

namespace Application.Contracts;

public interface IBasketService
{
    Task<string> CreateBasketAsync(string? ip = null);
    Task<BasketSummaryDto> GetBasketAsync();
    // Task AddItemsToBasketAsync(List<AddItemToBasketDto> addItemsToBasketDto, string? sessionid =  null);
    // Task UpdateQuantityAsync(UpdateBasketItemQuantityDto updateBasketItemQuantityDto);
    // Task RemoveItemFromBasketAsync(string itemid, string? sessionid = null);
}