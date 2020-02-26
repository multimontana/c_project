namespace InfraManager.WebApi.BLL.Calls
{
    using System;
    using System.Linq;

    using InfraManager.WebApi.BLL.Repositories.Contracts;
    using InfraManager.WebApi.DAL.DTOs;
    using InfraManager.WebApi.DAL.DTOs.Calls;

    using CallDto = InfraManager.WebApi.DAL.DTOs.Calls.Call;

    /// <summary>
    /// The call.
    /// </summary>
    public sealed class Call
    {
        /// <summary>
        /// The repository wrapper.
        /// </summary>
        private readonly IRepositoryWrapper repositoryWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="Call"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public Call(IRepositoryWrapper repository)
        {
            this.repositoryWrapper = repository;
        }

        /// <summary>
        /// The get calls order by number.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<CallDto> GetCallsOrderByNumber()
        {
            var call = this.repositoryWrapper.Call
                .Get(callOrder => callOrder.OrderByDescending(p => p.Number));

            return call;
        }

        /// <summary>
        /// The get calls by number.
        /// </summary>
        /// <param name="number">
        /// The call`s number.
        /// </param>
        /// <param name="includes">
        /// The includes.
        /// </param>
        /// <returns>
        /// The <see cref="Call"/>.
        /// </returns>
        public CallDto GetCallsByNumber(int number, params string[] includes)
        {
            var query = this.repositoryWrapper.Call.Get(
                callOrder => callOrder.OrderByDescending(p => p.Number), // order name by descending
                nameof(Priority), // order include properties
                nameof(CallType)).FirstOrDefault(p => p.Number == number);

            return query;
        }

        /// <summary>
        /// The call search by summary name.
        /// Take lowercase strings(SummaryName and search field) for comparison
        /// and get those proposals whose (SummaryName) field
        /// contain received string from the query string
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="search">
        /// The search.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<CallDto> CallSearchBySummaryName(IQueryable<DAL.DTOs.Calls.Call> query, string search)
        {
            return query.Where(o => o.CallSummaryName != null)
                .Where(p => p.CallSummaryName.ToLower().Contains(search.Trim().ToLower()));
        }

        /// <summary>
        /// The call filtering.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<CallDto> CallFiltering(IQueryable<DAL.DTOs.Calls.Call> query, string filter)
        {
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

            return query;
        }

        // Общая информация по заявке
        // 1. Номер заявки
        // 2. Дата / время поступления.
        //    Если поступила сегодня – отображается время,
        //    если поступила вчера – отображается текст «Вчера»,
        //    если поступила еще ранее – отображается дата поступления
        // 3. Приоритет. Кружок определенного цвета.
        //    Цвет берется с сервера из справочника «Таблица приоритетов»
        // 4. Краткое описание заявки
        // 5. Клиент
        // 6. Владелец / Исполнитель заявки.
        //    Если в заявке поле Исполнитель пустое – то отображается Владелец,
        //    если заполнено – то отображается Исполнитель.
        //    Исполнителем может быть не только человек, но и группа.
        /// <summary>
        /// The get call info.
        /// </summary>
        /// <param name="callDto">
        /// The call.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object GetCallInfo(CallDto callDto)
        {
            // Receipt date/time
            // Today, Yesterday, Date
            string date;

            if (DateTime.Compare(callDto.UtcDateOpened ?? DateTime.MinValue, DateTime.Today) > 0)
            {
                date = $"{callDto.UtcDateOpened:HH:mm}";
            }
            else if (DateTime.Compare(callDto.UtcDateOpened ?? DateTime.MinValue, DateTime.Today.AddDays(-1)) > 0)
            {
                date = "Вчера";
            }
            else
            {
                date = $"{callDto.UtcDateOpened:yyyy:dd:MM-HH:mm}";
            }

            var callInfo = new
                               {
                                   callDto.Number,
                                   Date = date,
                                   PriorityColor = callDto.Priority.Color,
                                   SummaryName = callDto.CallSummaryName,
                                   Client = callDto.ClientFullName,
                                   Role = callDto.OwnerId.HasValue ? "Исполнитель" : "Владелец"
                               };
            return callInfo;
        }

        /// <summary>
        /// The get call general data.
        /// </summary>
        /// <param name="callDto">
        /// The call.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object GetCallGeneralData(CallDto callDto)
        {
            var term = new
                           {
                               DateCreate = callDto.UtcDateOpened ?? DateTime.MinValue,
                               DateRegist = callDto.UtcDateRegistered ?? DateTime.MinValue,
                               DateClose = callDto.UtcDateClosed ?? DateTime.MinValue
                           };

            var people = new
                             {
                                 Client = callDto.ClientFullName,
                                 Owner = callDto.OwnerFullName,
                                 Executor = callDto.ExecutorFullName,
                                 Accomplisher = callDto.AccomplisherFullName
                             };

            var classification = new
                                     {
                                         Type = callDto.CallType.Name,
                                         CallReceiptType = (CallReceiptType)callDto.ReceiptType,
                                         Sercice = callDto.ServiceItemFullName
                                     };

            var essenceOfTask = new
                                    {
                                        Description = callDto.Htmldescription, Solution = callDto.Htmlsolution,

                                        // Attachments =        
                                    };

            var general = new
                              {
                                  Terms = term,
                                  People = people,
                                  Classification = classification,
                                  EssenceOfTask = essenceOfTask
                              };

            return general;
        }
    }
}