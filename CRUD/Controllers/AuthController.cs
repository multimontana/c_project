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
        [Route(template: "Login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _repositoryWrapper.User
                        .Get().FirstOrDefault(predicate: p => p.Name == model.Login);

                    if (user != null && CalculatePasswordService.CalculatePassword(model.Password) == user.SdwebPassword)
                    {
                        if (_authService.IsAuthenticated(requestModel: model, token: out var token))
                        {
                            return Ok(value: new
                            {
                                user.Name,
                                user.Workspace,
                                token
                            });
                        }
                    }

                    return Unauthorized(value: model.Login);
                }
                catch (Exception e)
                {
                    _logger.LogError(exception: e, message: e.Message);
                }
            }

            return BadRequest(modelState: ModelState);
        }
    }
}