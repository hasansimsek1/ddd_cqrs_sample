using Core.DomainModels.BasketModel;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Postgres;

public class BasketRepository : DapperBase, IBasketRepository
{
    private readonly IConfiguration _config;

    public BasketRepository(IConfiguration config) : base(config)
    {
        _config = config;
    }

    public async Task Add(BasketPersistenceDto basketPersistenceDto)
    {
        var command = @"
            INSERT INTO baskets(""id"", ip_address, user_id, date_created, date_updated)
                        VALUES(@id, @ip_address, @user_id, @date_created, @date_updated);
        ";

        var param = new
        {
            id = basketPersistenceDto.Id,
            ip_address = basketPersistenceDto.IpAddress,
            user_id = basketPersistenceDto.UserId,
            date_created = basketPersistenceDto.DateCreated,
            date_updated = basketPersistenceDto.DateUpdated
        };

        await ExecuteCommandAsync(command, param);
    }

    public async Task Update(BasketPersistenceDto basketPersistenceDto)
    {
        var command = @"
            UPDATE baskets SET ip_address = @ip_address, user_id = @user_id, date_updated = @date_updated WHERE ""id"" = @id
        ";

        var param = new
        {
            id = basketPersistenceDto.Id,
            ip_address = basketPersistenceDto.IpAddress,
            user_id = basketPersistenceDto.UserId,
            date_updated = basketPersistenceDto.DateUpdated
        };

        await ExecuteCommandAsync(command, param);
    }

    public async Task<Basket> GetBasket(string ipAddress, string? userId = null)
    {
        if (!string.IsNullOrWhiteSpace(userId))
        {
            var query = @"
                SELECT * FROM baskets WHERE user_id = @user_id;
            ";

            var param = new
            {
                user_id = userId
            };

            var queryResult = await ExecuteQueryAsync<Basket>(query, param);
            return queryResult.FirstOrDefault();
        }
        else
        {
            var query = @"
                SELECT * FROM baskets WHERE ip_address = @ip_address;
            ";

            var param = new
            {
                ip_address = ipAddress
            };

            var queryResult = await ExecuteQueryAsync<Basket>(query, param);
            return queryResult.FirstOrDefault();
        }
    }

    public async Task<bool> CheckIfExistsById(string basketId)
    {
        var query = @"
            SELECT 1 WHERE EXISTS (select ""id"" from baskets where ""id"" = @id);
        ";

        var param = new
        {
            id = basketId
        };

        var queryResult = await ExecuteQueryAsync<string?>(query, param);

        return queryResult.Count() < 1 ? false : true;
    }
}