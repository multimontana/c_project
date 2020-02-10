namespace InfraManager.WebApi.Controllers
{
    using System;
    using System.Linq;

    using InfraManager.WebApi.DAL.Repositories.Contracts;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The proposal controller.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProposalController : ControllerBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<ProposalController> logger;

        /// <summary>
        /// The repository wrapper.
        /// </summary>
        private readonly IRepositoryWrapper repositoryWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProposalController"/> class.
        /// </summary>
        /// <param name="repositoryWrapper">
        /// The repository wrapper.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public ProposalController(IRepositoryWrapper repositoryWrapper, ILogger<ProposalController> logger)
        {
            this.repositoryWrapper = repositoryWrapper;
            this.logger = logger;
        }

        /// <summary>
        /// The Get api/proposal.
        /// </summary>
        /// params filter, search, limit
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpGet]
        public IActionResult Get([FromQuery] string filter, string search, int limit = 20)
                {
            try
            {
                var query = this.repositoryWrapper.Call
                    .Get(callOrder => callOrder.OrderByDescending(p => p.Number));

                if (!string.IsNullOrEmpty(search))
                {
                    // Take lowercase strings(SummaryName and search field) for comparison
                    // and get those proposals whose <SummaryName> field
                    // contain received string from the query string
                    query = query.Where(o => o.CallSummaryName != null)
                        .Where(p => p.CallSummaryName.ToLower()
                            .Contains(search.Trim()
                                .ToLower()));
                }

                switch (filter)
                {
                    case "Unassigned":
                        query = query.Where(p => p.OwnerId == null).Where(p => p.ExecutorId == null)
                                     .Where(p => p.QueueId == null);
                        break;

                    case "MineAtwork":
                        query = query.Where(p => p.ExecutorId != null || p.OwnerId != null)
                                     .Where(p => p.UtcDateOpened != null);
                        break;
                }

                if (query.Any())
                {
                    return this.Ok(
                        query.Include(p => p.Priority).Select(
                            p => new
                            {
                                p.Number,
                                SummaryName = p.CallSummaryName,
                                Client = p.ClientFullName,
                                DateRegist = p.UtcDateRegistered,
                                PriorityColor = p.Priority.Color,
                                State = p.EntityStateName,
                                p.CallType.Icon
                            }).Take(limit));
                }

                return this.NotFound("Список заявок пуст");
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
            }

            return this.BadRequest();
        }
    }
}