using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Persistence.Postgres;

public class DapperBase
{
    private readonly string _appDbConStr;
    private readonly string _masterConStr;

    public DapperBase(IConfiguration config)
    {
        _appDbConStr = config["POSTGRES_APPDB_CON_STR"];
        _masterConStr = config["POSTGRES_MASTER_CON_STR"];
    }

    public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query, object? param = null, string? conStr = null)
    {
        using var connection = new NpgsqlConnection(conStr ?? _appDbConStr);
        await connection.OpenAsync();
        return await connection.QueryAsync<T>(query, param);
    }

    public async Task ExecuteCommandAsync(string command, object? param = null, string? conStr = null)
    {
        using var connection = new NpgsqlConnection(conStr ?? _appDbConStr);
        await connection.OpenAsync();
        await connection.ExecuteAsync(command, param);
    }
}