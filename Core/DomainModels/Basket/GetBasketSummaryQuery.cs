using MediatR;

namespace Core.DomainModels.BasketModel;

public record GetBasketSummaryQuery(string IpAddress, string? UserId) : IRequest<BasketSummaryDto>;

public class GetBasketSummaryQueryHandler : IRequestHandler<GetBasketSummaryQuery, BasketSummaryDto>
{
    private readonly IBasketRepository _basketRepository;

    public GetBasketSummaryQueryHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<BasketSummaryDto> Handle(GetBasketSummaryQuery request, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.GetBasket(request.IpAddress, request.UserId);

        if (basket != null)
        {
            return new BasketSummaryDto
            {
                Id = basket.Id,
                ItemCount = basket.GetItemsCount(),
                TotalPrice = basket.GetBasketPrice()
            };
        }

        return null;
    }
}

public class BasketSummaryDto
{
    public string Id { get; set; }
    public int ItemCount { get; set; }
    public decimal TotalPrice { get; set; }
}