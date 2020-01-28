using Auth.Model;

namespace Auth.Services
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(AuthenticateModel requestModel, out string token);
    }
}