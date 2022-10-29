namespace Infrastructure.Persistence;

public interface IDbInitializer
{
    Task InitializeDbAsync();
}