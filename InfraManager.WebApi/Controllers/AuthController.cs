namespace InfraManager.WebApi.Controllers
{
    using System;
    using System.Linq;

    using Auth.Model;
    using Auth.Services;

    using InfraManager.WebApi.DAL;

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
        /// The service.
        /// </summary>
        private readonly IAuthenticateService authService;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<AuthController> logger;

        /// <summary>
        /// The context.
        /// </summary>
        private readonly TmContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">
        /// The service.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        public AuthController(
            IAuthenticateService authService,
            ILogger<AuthController> logger,
            TmContext context)
        {
            this.authService = authService;
            this.logger = logger;
            this.context = context;
        }

        /// <summary>
        /// The login.
        /// </summary>
        /// <param name="model">
        /// The model.
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
                    var user = this.context.Users.FirstOrDefault(p => p.LoginName == model.Login);

                    if (user?.SdwebPassword != null)
                    {
                        byte[] password = Array.ConvertAll(user.SdwebPassword, x => x);

                        if (this.authService.IsAuthenticated(user.LoginName, password, model, out var token))
                        {
                            return this.Ok(new { user.Id, user.Name, Token = token });
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