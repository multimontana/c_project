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

    public class TokenAuthenticationService : IAuthenticateService
    {
        private readonly IUserManagementService userManagementService;

        private readonly TokenManagement tokenManagement;

        public TokenAuthenticationService(IUserManagementService service, IOptions<TokenManagement> tokenManagement)
        {
            this.userManagementService = service;
            this.tokenManagement = tokenManagement.Value;
        }

        public bool IsAuthenticated(string login, byte[] password, AuthenticateModel requestModel, out string token)
        {
            token = default;

            // Check user name and password
            if (this.userManagementService.IsValidUser(login, password, requestModel))
            {
                var claim = new[]
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
                    claim,
                    expires: DateTime.Now.AddMinutes(this.tokenManagement.AccessExpiration),
                    signingCredentials: credentials);

                token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                return true;
            }

            return false;
        }
    }
}