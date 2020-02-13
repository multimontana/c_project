namespace InfraManager.WebApi.Controllers
{
    using System;
    using System.Linq;

    using InfraManager.WebApi.BLL.Repositories.Contracts;

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
    public class CallController : ControllerBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<CallController> logger;

        /// <summary>
        /// The repository wrapper.
        /// </summary>
        private readonly IRepositoryWrapper repositoryWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallController"/> class.
        /// </summary>
        /// <param name="repositoryWrapper">
        /// The repository wrapper.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public CallController(IRepositoryWrapper repositoryWrapper, ILogger<CallController> logger)
        {
            this.repositoryWrapper = repositoryWrapper;
            this.logger = logger;
        }

        /// <summary>
        /// The Get api/Call{filter/search/limit}.
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
                    query = query.Where(o => o.CallSummaryName != null).Where(
                        p => p.CallSummaryName.ToLower().Contains(search.Trim().ToLower()));
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

        /// <summary>
        /// The Post api/Call
        /// </summary>
        /// Param Call number
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        public IActionResult Post(int number)
        {
            try
            {
                if (number <= 0)
                {
                    this.logger.LogTrace("The call number have incorrect value");
                    return this.BadRequest();
                }

                // Get a proposal through number
                var query = this.repositoryWrapper.Call
                    .Get(callOrder => callOrder.OrderByDescending(p => p.Number))
                    .Include(p => p.Priority)
                    .Include(p => p.CallType)
                    .FirstOrDefault(p => p.Number == number);

                if (query != null)
                {
                    // Receipt date/time
                    // Today, Yesterday, Date
                    string date;

                    if (DateTime.Compare(query.UtcDateOpened ?? DateTime.MinValue, DateTime.Today) > 0)
                    {
                        date = $"{query.UtcDateOpened:HH:mm}";
                    }
                    else if (DateTime.Compare(query.UtcDateOpened ?? DateTime.MinValue, DateTime.Today.AddDays(-1)) > 0)
                    {
                        date = "Вчера";
                    }
                    else
                    {
                        date = $"{query.UtcDateOpened:yyyy:dd:MM-HH:mm}";
                    }

                    var info = new
                                   {
                                       query.Number,
                                       Date = date,
                                       PriorityColor = query.Priority.Color,
                                       SummaryName = query.CallSummaryName,
                                       Client = query.ClientFullName,
                                       Role = query.OwnerId.HasValue ? "Исполнитель" : "Владелец"
                                   };

                    var term = new
                                   {
                                       DateCreate = query.UtcDateOpened ?? DateTime.MinValue,
                                       DateRegist = query.UtcDateRegistered ?? DateTime.MinValue,
                                       DateClose = query.UtcDateClosed ?? DateTime.MinValue
                                   };
                    var people = new
                                     {
                                         Client = query.ClientFullName,
                                         Owner = query.OwnerFullName,
                                         Executor = query.ExecutorFullName,
                                         Accomplisher = query.AccomplisherFullName
                                     };
                    var classification = new
                                             {
                                                 Type = query.CallType.Name,

                                                 // WayOfReception =
                                                 Sercice = query.ServiceItemFullName
                                             };
                    var essenceOfTask = new
                                            {
                                                Description = query.Htmldescription, Solution = query.Htmlsolution,

                                                // Attachments =       
                                            };
                    var general = new
                                      {
                                          Terms = term,
                                          People = people,
                                          Classification = classification,
                                          EssenceOfTask = essenceOfTask
                                      };

                    return this.Ok(general);
                }

                return this.NotFound("Список карточка заявок пуст");
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
            }

            return this.BadRequest();
        }
    }
}