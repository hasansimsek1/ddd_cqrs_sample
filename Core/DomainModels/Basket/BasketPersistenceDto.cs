namespace Core.DomainModels.BasketModel;

public class BasketPersistenceDto
{
    public string Id { get; set; }
    public string? UserId { get; set; }
    public string IpAddress { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public DateTime? DateDeleted { get; set; }
}