using Infrastructure.Auth;

namespace Infrastructure.Auth;

public interface IAuthService
{
    Task<LoginStatus> GetLoginStatus();
    Task<string> GetUserId();
}