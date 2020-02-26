namespace InfraManager.WebApi.BLL
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InfraManager.WebApi.DAL;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The document.
    /// </summary>
    public sealed class Document
    {
        /// <summary>
        /// The repository wrapper.
        /// </summary>
        private readonly TmContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class. 
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public Document(TmContext context)
        {
            this.context = context;
        }

        public async Task<object> GetDocumentList(Guid objectId)
        {
            var query = await this.GetDocumentByObjectId(objectId);

            return query;
        }

        private async Task<object> GetDocumentByObjectId(Guid objectId)
        {
            var docs = await this.context.ViewDocuments
                .FromSqlRaw($"EXEC [dbo].[Document_GetListByObjectID] '{objectId}'").ToListAsync();

            var retval = docs.Select(
                p => new
                         {
                             p.Id,
                             p.Size,
                             Name = string.Concat(p.Name, ".", p.Extension).Trim(new char[] { '.' }),
                             Count = docs.Count(),
                             p.Data
                         });

            return retval;
        }
    }
}