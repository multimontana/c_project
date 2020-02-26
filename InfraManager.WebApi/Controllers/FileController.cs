namespace InfraManager.WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;

    using InfraManager.WebApi.BLL.Repositories.Contracts;
    using InfraManager.WebApi.DAL;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Document = BLL.Document;

    /// <summary>
    /// The file controller.
    /// </summary>
    [ApiController]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<FileController> logger;

        /// <summary>
        /// The context.
        /// </summary>
        private readonly TmContext context;

        /// <summary>
        /// The document.
        /// </summary>
        private readonly Document document;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileController"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="repositoryWrapper">
        /// The repository wrapper.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        public FileController(ILogger<FileController> logger, IRepositoryWrapper repositoryWrapper, TmContext context)
        {   
            this.logger = logger;
            this.context = context;

            if (this.document == null)
            {
                this.document = new Document(context);
            }
        }

        /// <summary>
        /// The get document list.
        /// </summary>
        /// <param name="objectId">
        /// The object id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet, Route("fileApi/getDocumentList", Name = "GetDocumentList")]
        public async Task<object> GetDocumentList(Guid objectId)
        {
            this.logger.LogInformation("FileController->GetDocumentList, {ObjectID}!", objectId);

            try
            {
                 var documents = await this.document.GetDocumentList(objectId);

                return documents;
            }
            catch (Exception ex)
            {
                this.logger.LogTrace(ex, "Ошибка получения списка прикреплений по объекту");
                return null;
            }
        }
    }
}