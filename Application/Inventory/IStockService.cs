namespace Application.Contracts;

public interface IStockService
{
    Task<bool> CheckProductAvailability(string productId, int expectedCount);
}