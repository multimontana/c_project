namespace InfraManager.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using InfraManager.WebApi.BLL.Repositories.Contracts;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;

    using Call = InfraManager.WebApi.BLL.Calls.Call;

    /// <summary>
    /// The Call controller.
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
        // private readonly IRepositoryWrapper repositoryWrapper;

        /// <summary>
        /// The cache.
        /// </summary>
        private IMemoryCache cache;

        /// <summary>
        /// The Call.
        /// </summary>
        private readonly Call call;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallController"/> class.
        /// </summary>
        /// <param name="repositoryWrapper">
        /// The repository wrapper.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public CallController(IRepositoryWrapper repositoryWrapper, ILogger<CallController> logger, IMemoryCache memoryCache)
        {
            // this.repositoryWrapper = repositoryWrapper;
            this.logger = logger;

            if (this.call == null)
            {
                this.call = new BLL.Calls.Call(repositoryWrapper);
            }

        }

        /// <summary>
        /// The Get api/Call{filter/search/limit}.
        /// </summary>
        /// params filter, search, limit
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpGet]
        public IActionResult Get([FromQuery] string filter, string search, int limit = 10)
        {
            try
            {
                var calls = this.call.GetCallsOrderByNumber();

                if (!string.IsNullOrEmpty(search))
                {
                    calls = this.call.CallSearchBySummaryName(calls, search);
                }

                calls = this.call.CallFiltering(calls, filter);

                if (calls.Any())
                {
                    return this.Ok(
                        calls.Include(p => p.Priority).Select(
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
        public IActionResult Post([FromBody]int number)
        {
            try
            {
                if (number <= 0)
                {
                    this.logger.LogTrace("The call number have incorrect value");
                    return this.BadRequest();
                }

                // Get a call through number
                var callDto = this.call.GetCallsByNumber(number);

                if (callDto != null)
                {
                    var info = this.call.GetCallInfo(callDto);

                    var callGeneralData = this.call.GetCallGeneralData(callDto);

                    return this.Ok(callGeneralData);
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