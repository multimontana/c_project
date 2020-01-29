namespace CRUD.Controllers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Auth.Model;
    using Auth.Services;

    using DataAccess.Repositories.Contracts;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The Authorization controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// The _auth service.
        /// </summary>
        private readonly IAuthenticateService authService;

        /// <summary>
        /// The repository wrapper.
        /// </summary>
        private readonly IRepositoryWrapper repositoryWrapper;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<AuthController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">
        /// The auth service.
        /// </param>
        /// <param name="repositoryWrapper">
        /// The repository wrapper.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public AuthController(
            IAuthenticateService authService,
            IRepositoryWrapper repositoryWrapper,
            ILogger<AuthController> logger)
        {
            this.authService = authService;
            this.repositoryWrapper = repositoryWrapper;
            this.logger = logger;
        }

        /// <summary>
        /// Route : api/auth/login.
        /// </summary>
        /// <param name="model">
        /// The auth model.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [Route(template: "Login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = this.repositoryWrapper.User.Get()
                        .FirstOrDefault(predicate: p => p.LoginName == model.Login);

                    // if (user != null && CalculatePasswordService.CalculatePassword(model.Password) == user.SdwebPassword)
                    if (user != null)
                    {
                        if (this.authService.IsAuthenticated(requestModel: model, token: out var token))
                        {
                            return Ok(value: new
                            {
                                Id = user.Id,
                                Name = user.Name,
                                Token = token
                            });
                        }
                    }

                    return Unauthorized(value: "The username or password is incorrect");
                }
                catch (Exception e)
                {
                    this.logger.LogError(exception: e, message: e.Message);
                }
            }

            return BadRequest(modelState: ModelState);
        }
    }
}