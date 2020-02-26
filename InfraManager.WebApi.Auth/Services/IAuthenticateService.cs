namespace InfraManager.WebApi.Auth.Services
{
    using InfraManager.WebApi.Auth.Model;

    /// <summary>
    /// The AuthenticateService interface.
    /// </summary>
    public interface IAuthenticateService
    {
        /// <summary>
        /// Check request login and password with database fields(login and password), return token if matching.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="requestModel">
        /// The request model.
        /// </param>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsAuthenticated(string login, byte[] password, AuthenticateModel requestModel, out string token);
    }
}