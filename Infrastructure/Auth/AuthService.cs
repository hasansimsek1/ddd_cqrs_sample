using Infrastructure.Auth;

namespace Infrastructure.Auth;

public class AuthService : IAuthService
{
    public async Task<LoginStatus> GetLoginStatus()
    {
        return await Task.Run(() => { return LoginStatus.LoggedIn; });
    }

    public async Task<string> GetUserId()
    {
        return await Task.Run(() => "df6c4862-ea5d-46dc-b041-9b13ce11eb20");
    }
}