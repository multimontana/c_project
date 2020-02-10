namespace InfraManager.WebApi.Controllers
{
    using System;
    using System.Linq;

    using InfraManager.WebApi.DAL.Repositories.Contracts;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CardProposalController : ControllerBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<CardProposalController> logger;

        /// <summary>
        /// The repository wrapper.
        /// </summary>
        private readonly IRepositoryWrapper repositoryWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardProposalController"/> class.
        /// </summary>
        /// <param name="repositoryWrapper">
        /// The repository wrapper.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public CardProposalController(IRepositoryWrapper repositoryWrapper, ILogger<CardProposalController> logger)
        {
            this.repositoryWrapper = repositoryWrapper;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get(int number)
        {
            try
            {
                // Get a proposal through number
                var query = this.repositoryWrapper.Call.Get(callOrder => callOrder.OrderByDescending(p => p.Number))
                    .Include(p => p.Priority).Include(p => p.CallType).FirstOrDefault(p => p.Number == number);

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
                                                Description = query.Htmldescription,
                                                Solution = query.Htmlsolution,
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