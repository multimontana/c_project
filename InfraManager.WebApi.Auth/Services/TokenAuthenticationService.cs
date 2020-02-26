namespace InfraManager.WebApi.Auth.Services
{
    using System;
    using System.Globalization;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    using InfraManager.WebApi.Auth.Model;

    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// The token authentication service.
    /// </summary>
    public class TokenAuthenticationService : IAuthenticateService
    {
        /// <summary>
        /// The user management service.
        /// </summary>
        private readonly IUserManagementService userManagementService;

        /// <summary>
        /// The token management.
        /// </summary>
        private readonly TokenManagement tokenManagement;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenAuthenticationService"/> class.
        /// </summary>
        /// <param name="service">
        /// The service.
        /// </param>
        /// <param name="tokenManagement">
        /// The token management.
        /// </param>
        public TokenAuthenticationService(IUserManagementService service, IOptions<TokenManagement> tokenManagement)
        {
            this.userManagementService = service;
            this.tokenManagement = tokenManagement.Value;
        }

        /// <summary>
        /// The is authenticated.
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
        public bool IsAuthenticated(string login, byte[] password, AuthenticateModel requestModel, out string token)
        {
            token = default;

            // Check user name and password
            if (this.userManagementService.IsValidUser(login, password, requestModel))
            {
                var claims = new[]
                                {
                                    new Claim(ClaimTypes.Name, requestModel.Login),
                                    new Claim(JwtRegisteredClaimNames.Sub, requestModel.HostName ?? "hostname"),
                                    new Claim(
                                        JwtRegisteredClaimNames.AuthTime,
                                        DateTime.Now.ToString(CultureInfo.InvariantCulture))
                                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.tokenManagement.Secret));

                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var jwtToken = new JwtSecurityToken(
                    this.tokenManagement.Issuer,
                    this.tokenManagement.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(this.tokenManagement.AccessExpiration),
                    signingCredentials: credentials);

                token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                return true;
            }

            return false;
        }
    }
}