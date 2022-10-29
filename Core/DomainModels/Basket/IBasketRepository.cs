namespace Core.DomainModels.BasketModel;

public interface IBasketRepository
{
    Task<Basket> GetBasket(string ipAddress, string? userId = null);
    Task Add(BasketPersistenceDto basketPersistenceDto);
    Task Update(BasketPersistenceDto basketPersistenceDto);
    Task<bool> CheckIfExistsById(string basketId);
}