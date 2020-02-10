namespace InfraManager.WebApi.Controllers
{
    using System;
    using System.Linq;

    using Auth.Model;
    using Auth.Services;

    using InfraManager.WebApi.DAL;
    using InfraManager.WebApi.DAL.Repositories.Contracts;

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
        /// The logger.
        /// </summary>
        private readonly ILogger<AuthController> logger;

        /// <summary>
        /// The repository wrapper.
        /// </summary>
        private readonly IRepositoryWrapper repositoryWrapper;

        private readonly TmContext _context;

        #region Ctor

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
            ILogger<AuthController> logger,
            TmContext context)
        {
            this.authService = authService;
            this.repositoryWrapper = repositoryWrapper;
            this.logger = logger;
            this._context = context;
        }

        #endregion

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
        [Route("Login")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = "Windows")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var user = this._context.Users.FirstOrDefault(p => p.LoginName == model.Login);

                    if (user != null && user.SdwebPassword != null)
                    {
                        byte[] password = Array.ConvertAll(user.SdwebPassword, x => x);

                        if (this.authService.IsAuthenticated(user.LoginName, password, model, out var token))
                        {
                            return this.Ok(new { Id = user.Id, Name = user.Name, Token = token });
                        }
                    }

                    return this.Unauthorized("The username or password is incorrect");
                }
                catch (Exception e)
                {
                    this.logger.LogError(message: e.Message, exception: e);
                }
            }

            return this.BadRequest();
        }
    }
}