namespace Core.DomainModels.BasketModel;

/*
AGGREGATE ROOT
*/

public class Basket
{
    public string Id { get; set; }
    public string? UserId { get; set; }
    public string IpAddress { get; set; }
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();

    public Basket()
    {
        Id = Guid.NewGuid().ToString();
    }

    public async Task Persist(IBasketRepository basketRepository)
    {
        var basketExists = await basketRepository.CheckIfExistsById(Id);

        if (basketExists)
        {
            var persistenceDto = new BasketPersistenceDto
            {
                Id = Id,
                IpAddress = IpAddress,
                UserId = UserId,
                DateUpdated = DateTime.Now,
            };

            await basketRepository.Update(persistenceDto);
        }
        else
        {
            var persistenceDto = new BasketPersistenceDto
            {
                Id = Id,
                IpAddress = IpAddress,
                UserId = UserId,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
            };

            await basketRepository.Add(persistenceDto);
        }
    }

    public decimal GetBasketPrice()
    {
        decimal totalAmount = default(decimal);

        foreach (var item in Items)
        {
            totalAmount += item.PriceInBasket;
        }

        return totalAmount;
    }

    public int GetItemsCount()
    {
        int totalCount = 0;

        foreach (var item in Items)
        {
            totalCount += item.Count;
        }

        return totalCount;
    }
}