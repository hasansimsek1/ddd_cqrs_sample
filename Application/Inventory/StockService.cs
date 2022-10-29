using Application.Contracts;

namespace Application.Services;

public class StockService : IStockService
{
    public async Task<bool> CheckProductAvailability(string productId, int expectedCount)
    {
        return await Task.Run(() => true);
    }
}