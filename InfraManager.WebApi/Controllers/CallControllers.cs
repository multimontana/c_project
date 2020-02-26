namespace InfraManager.WebApi.Controllers
{
    using System;
    using System.Linq;

    using InfraManager.WebApi.BLL.Repositories.Contracts;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using Call = BLL.Calls.Call;

    /// <summary>
    /// The Call controller.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CallController : ControllerBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<CallController> logger;

        /// <summary>
        /// The call.
        /// </summary>
        private readonly Call call;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallController"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="repositoryWrapper">
        /// The repositoryWrapper.
        /// </param>
        public CallController(ILogger<CallController> logger, IRepositoryWrapper repositoryWrapper)
        {
            this.logger = logger;

            if (this.call == null)
            {
                this.call = new Call(repositoryWrapper);
            }
        }

        /// <summary>
        /// The get list.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <param name="search">
        /// The search.
        /// </param>
        /// <param name="limit">
        /// The limit.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpGet]
        [ActionName("GetList")]
        public IActionResult GetList([FromQuery] string filter, string search, int limit = 10)
        {
            this.logger.LogInformation(
                $"GET: Call->GetList, Params (filter = {filter}, search = {search}, limit = {limit})");
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
                                         p.Id,
                                         p.Number,
                                         SummaryName = p.CallSummaryName,
                                         Client = p.ClientFullName,
                                         DateRegist = p.UtcDateRegistered,
                                         PriorityColor = p.Priority.Color,
                                         State = p.EntityStateName,
                                         p.CallType.Icon
                                     }).Take(limit));
                }

                this.logger.LogInformation("GET: Call, Список заявок пуст");

                return this.NotFound("Список заявок пуст");
            }
            catch (Exception e)
            {
                this.logger.LogError("Список заявок", e);
                return null;
            }
        }

        /// <summary>
        /// The GetByNumber Call
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        /// Param Call number
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpGet("{number}")]
        [ActionName("GetByNumber")]
        public IActionResult GetByNumber(int number)
        {
            this.logger.LogInformation($"GET: Call->GetByNumber(CallNumber = {number})");

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
                    var callGeneralData = this.call.GetCallGeneralData(callDto);

                    return this.Ok(callGeneralData);
                }

                this.logger.LogInformation("Список карточка заявок пуст");
                return this.NotFound("Список карточка заявок пуст");
            }
            catch (Exception e)
            {
                this.logger.LogError("Список карточка заявок", e);
                return null;
            }
        }

        /// <summary>
        /// The get info by number.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpGet("{number}")]
        [ActionName("GetInfoByNumber")]
        public IActionResult GetInfoByNumber(int number)
        {
            this.logger.LogInformation($"GET: Call->GetInfoByNumber, Param (CallNumber = {number})");

            try
            {
                if (number <= 0)
                {
                    this.logger.LogTrace("The call number have incorrect value");
                    return null;
                }

                // Get a call through number
                var callDto = this.call.GetCallsByNumber(number);

                if (callDto != null)
                {
                    var info = this.call.GetCallInfo(callDto);

                    return this.Ok(info);
                }

                this.logger.LogInformation("Список карточка заявок пуст");
                return this.NotFound("Список карточка заявок пуст");
            }
            catch (Exception e)
            {
                this.logger.LogError("Список карточка заявок", e);
                return null;
            }
        }
    }
}