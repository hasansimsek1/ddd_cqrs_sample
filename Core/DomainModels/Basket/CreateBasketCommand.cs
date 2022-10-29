using MediatR;

namespace Core.DomainModels.BasketModel;

public record CreateBasketCommand(string Id, string IpAddress, string? UserId) : IRequest;

public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand>
{
    private readonly IBasketRepository _basketRepository;

    public CreateBasketCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<Unit> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        var newBasket = new Basket()
        {
            Id = request.Id,
            IpAddress = request.IpAddress,
            UserId = request.UserId
        };

        await newBasket.Persist(_basketRepository);
        return Unit.Value;
    }
}