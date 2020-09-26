using IOWebFramework.Core.Models.Attachments;
using IOWebFramework.Infrastructure.Data.Models;
using System.Linq;

namespace IOWebFramework.Core.Contracts
{
    /// <summary>
    /// Manipulates AttachedDocument data
    /// </summary>
    public interface IAttachedDocumentService
    {
        /// <summary>
        /// Returns collection of AttachedDocuments
        /// </summary>
        /// <param name="documentId">Id of Document</param>
        /// <param name="documentType">Type of Document</param>
        /// <returns>IQueryable<AttachedDocumentViewModel></returns>
        IQueryable<AttachedDocumentViewModel> GetAllAttachmentsByDocumentId(int documentId, int documentType);

        /// <summary>
        /// Adds/Saves AttachedDocument and returns Id of the entity
        /// </summary>
        /// <param name="model">AttachedDocumentViewModel</param>
        /// <param name="fileContentId">Id of fileContent</param>
        /// <returns>long</returns>
        long SaveAttachedDocument(AttachedDocumentViewModel model, string fileContentId = null);

        /// <summary>
        /// Sets AttachmentDocument as InActive by given Id
        /// </summary>
        /// <param name="attachedDocumentId">Id of AttachmentDocument</param>
        /// <returns>bool</returns>
        bool SetAttachedDocumentAsInActive(long attachedDocumentId);

        /// Deletes AttachmentDocument from DB by given Id
        /// </summary>
        /// <param name="attachedDocumentId">Id of AttachmentDocument</param>
        /// <returns>bool</returns>
        bool HardDeleteAttachedDocument(long attachedDocumentId); /// <summary>

        /// <summary>
		/// Returns collection of AttachedDocuments from person ID
		/// </summary>
		/// <param name="perosnId">Id of person</param>
		/// <returns>IQueryable<AttachedDocumentViewModel></returns>
        //IQueryable<AttachedDocumentViewModel> GetAllAttachmentsByPersonId(int perosnId);

        /// <summary>
        /// Gets AttachedDocument by given Id
        /// </summary>
        /// <param name="attachedDocumentId">Id of AttachedDocument</param>
        /// <returns>AttachedDocumentViewModel</returns>
        AttachedDocumentViewModel GetAttachedDocumentById(long attachedDocumentId);

    }
}
