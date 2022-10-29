using Application.Contracts;
using Core.DomainModels.BasketModel;
using Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class BasketService : IBasketService
{
    private readonly IStockService _stockService;
    private readonly IAuthService _authService;
    private readonly HttpContext _httpContext;
    private readonly IMediator _mediator;

    public BasketService(
        IStockService stockService,
        IAuthService authService,
        IHttpContextAccessor httpContextAccessor,
        IMediator mediator)
    {
        _stockService = stockService;
        _authService = authService;
        _httpContext = httpContextAccessor.HttpContext;
        _mediator = mediator;
    }

    public async Task<string> CreateBasketAsync(string? ip = null)
    {
        var basketId = Guid.NewGuid().ToString();
        var loginStatus = await _authService.GetLoginStatus();
        var userId = loginStatus == LoginStatus.LoggedIn ? await _authService.GetUserId() : null;
        var ipAddress = _httpContext.Connection.RemoteIpAddress;
        await _mediator.Send(new CreateBasketCommand(basketId, ipAddress.ToString(), userId));
        return basketId;
    }

    public async Task<BasketSummaryDto> GetBasketAsync()
    {
        var loginStatus = await _authService.GetLoginStatus();
        var userId = loginStatus == LoginStatus.LoggedIn ? await _authService.GetUserId() : null;
        var ipAddress = _httpContext.Connection.RemoteIpAddress;
        return await _mediator.Send(new GetBasketSummaryQuery(ipAddress.ToString(), userId));
    }
}