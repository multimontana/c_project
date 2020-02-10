namespace InfraManager.WebApi.Auth.Services
{
    using InfraManager.WebApi.Auth.Model;

    public interface IAuthenticateService
    {
        /// <summary>
        /// Check request login and password with database fields(login and password), return token if matching
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="requestModel"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        bool IsAuthenticated(string login, byte[] password, AuthenticateModel requestModel, out string token);
    }
}