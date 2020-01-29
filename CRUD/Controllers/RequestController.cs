namespace CRUD.Controllers
{
    using System;
    using System.Linq;
    using DataAccess.Repositories.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The request controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        /// <summary>
        /// The repository wrapper.
        /// </summary>
        private readonly IRepositoryWrapper repositoryWrapper;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<RequestController> logger;

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestController"/> class.
        /// </summary>
        /// <param name="repositoryWrapper">
        /// The repository wrapper.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public RequestController(IRepositoryWrapper repositoryWrapper, ILogger<RequestController> logger)
        {
            this.repositoryWrapper = repositoryWrapper;
            this.logger = logger;
        }

        #endregion

        /// <summary>
        /// The Get api/requests.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="search">
        /// The search.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// params filter, search, page
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] string filter, string search, int page = 20)
        {
            try
            {
                // get via repository
                var query = this.repositoryWrapper.User.Get(
                    orderBy: userOrder => userOrder.OrderByDescending(p => p.Id));
                if (query.Any())
                {
                    if (!string.IsNullOrEmpty(search))
                    {
                        query = query.Where(o => o.Name != null && o.Name.ToLower().Contains(search.Trim().ToLower()))
                            .Take(page);
                    }

                    switch (filter)
                    {
                        case "AllBid":
                            break;

                        case "Unassigned":

                            query = query.Take(page);
                            break;

                        case "MineAtwork":

                            query = query.Take(page);
                            break;
                    }

                    return this.Ok(query.Select(p => new {Login = p.Name, p.LoginName}));
                }
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
            }

            return this.NotFound("the bid list is empty");
        }
    }
}