using System;
using System.Linq;
using Auth.Model;
using Auth.Services;
using DataAccess.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticateService _authService;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthenticateService authService, 
            IRepositoryWrapper repositoryWrapper,
            ILogger<AuthController> logger)
        {
            _authService = authService;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _repositoryWrapper.User
                        .Get().FirstOrDefault(p => p.Name == model.Username);

                    //if (user != null && CalculatePasswordService.CalculatePassword(model.Password) == user.SdwebPassword)
                    if (user != null)
                    {
                        if (_authService.IsAuthenticated(model, out var token))
                        {
                            return Ok(new
                            {
                                token = token
                            });
                        }
                    }

                    return Unauthorized(model.Username);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                }
            }

            return BadRequest(ModelState);
        }
    }
}