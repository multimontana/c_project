using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Services
{
    public class TokenAuthenticationService : IAuthenticateService
    {
        private readonly IUserManagementService _userManagementService;
        private readonly TokenManagement _tokenManagement;

        public TokenAuthenticationService(IUserManagementService service,
            IOptions<TokenManagement> tokenManagement)
        {
            _userManagementService = service;
            _tokenManagement = tokenManagement.Value;
        }

        public bool IsAuthenticated(AuthenticateModel requestModel, out string token)
        {
            token = default;

            // Check user name and password
            if (_userManagementService.IsValidUser(requestModel.Login, requestModel.Password))
            {
                var claim = new[]
                {
                    new Claim(ClaimTypes.Name, requestModel.Login),
                    new Claim(JwtRegisteredClaimNames.Sub, requestModel.HostName ?? "hostname"),
                    new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString(CultureInfo.InvariantCulture))
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));

                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var jwtToken = new JwtSecurityToken(
                    _tokenManagement.Issuer,
                    _tokenManagement.Audience,
                    claim,
                    expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                    signingCredentials: credentials
                );
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                return true;
            }

            return false;
        }
    }
}