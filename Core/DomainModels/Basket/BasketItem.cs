namespace Core.DomainModels.BasketModel;

/*
    VALUE OBJECT
*/
public class BasketItem
{
    public string Id { get; set; }
    public string ProductId { get; set; }
    public int Count { get; set; }
    public decimal PriceInBasket { get; set; }
}