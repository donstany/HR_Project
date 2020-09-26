namespace IOWebFramework.Core.Contracts
{
    using CDN.Core3.Data.Models;
    //using CDN.Core3.Objects;
    using System;

    public interface IFileService
    {
        /// <summary>
        /// Uploads a generated PDF file to Database for given Document and returns TRUE if success
        /// </summary>
        /// <param name="documentId">Id of Document</param>
        /// <param name="documentType">Type of Document</param>
        /// <param name="fileName">Name of the file</param>
        /// <param name="fileContent">Content of the file</param>
        /// <returns>bool</returns>
        bool UploadPdfFileForDocument(long documentId, int documentType, string fileName, byte[] fileContent);

        /// <summary>
        /// Returns ContentId of file with given SourceId and SourceType or String.Empty if not found
        /// </summary>
        /// <param name="sourceId">string sourceId</param>
        /// <param name="sourceType">int sourceType</param>
        /// <returns>string</returns>
        string GetFileContentIdBySourceIdAndSourceType(string sourceId, int sourceType);

        /// <summary>
        /// Gets file content from CDN
        /// </summary>
        /// <param name="sourceId">Identifier of the source document</param>
        /// <param name="sourceType">Type of the attached file</param>
        /// <exception cref="ArgumentException">If file not found or file is not PDF</exception>
        /// <returns></returns>
        (string fileName, byte[] fileContent, string contentId) GetPdfContent(string sourceId, int sourceType);

        /// <summary>
        /// Uploads file to CDN
        /// </summary>
        /// <param name="sourceId">Identifier of the source document</param>
        /// <param name="sourceType">Type of the attached file</param>
        /// <param name="fileName">Name of the uploaded file</param>
        /// <param name="fileContent">Content of the uploaded file</param>
        void UploadFile(string sourceId, int sourceType, string fileName, byte[] fileContent);

        /// <summary>
        /// Dowload file from CDN
        /// </summary>
        /// <param name="contentId">Identifier of file in CDN</param>
        /// <returns></returns>
        FileInfo Download(string contentId);
    }
}
