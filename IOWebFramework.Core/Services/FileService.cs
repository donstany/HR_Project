using CDN.Core3.Data.Contracts;
using CDN.Core3.Data.Data;
using CDN.Core3.Data.Models;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Infrastructure.Data.Common;
using System;
using System.Linq;

namespace IOWebFramework.Core.Services.File
{
    public class FileService : IFileService
    {
        private readonly ICdnService cdnService = null;
        private readonly IRepository repo;

        public FileService(ICdnService cdnService,
                           IRepository _repo)
        {
            this.cdnService = cdnService;
            this.repo = _repo;
        }

        /// <summary>
        /// Uploads a generated PDF file to Database for given Document and returns TRUE if success
        /// </summary>
        /// <param name="documentId">Id of Document</param>
        /// <param name="documentType">Type of Document</param>
        /// <param name="fileName">Name of the file</param>
        /// <param name="fileContent">Content of the file</param>
        /// <returns>bool</returns>
        public bool UploadPdfFileForDocument(long documentId, int documentType, string fileName, byte[] fileContent)
        {
            bool result = true;

            try
            {
                UploadFile(documentId.ToString(), documentType, fileName, fileContent);
            }
            catch (ArgumentException)
            {
                result = false;
            }
            
            return result;
        }

        /// <summary>
        /// Uploads file to CDN
        /// </summary>
        /// <param name="sourceId">Identifier of the source document</param>
        /// <param name="sourceType">Type of the attached file</param>
        /// <param name="fileName">Name of the uploaded file</param>
        /// <param name="fileContent">Content of the uploaded file</param>
        public void UploadFile(string sourceId, int sourceType, string fileName, byte[] fileContent)
        {
            FileRequest request = new FileRequest()
            {
                SourceID = sourceId,
                SourceType = sourceType,
                FileName = fileName,
                FileContent = fileContent
            };

            var uploadResponse = cdnService.Upload(request);

            if (!uploadResponse.SavedOK)
            {
                throw new ArgumentException(uploadResponse.ErrorMessage);
            }
        }

        /// <summary>
        /// Returns ContentId of file with given SourceId and SourceType or String.Empty if not found
        /// </summary>
        /// <param name="sourceId">string sourceId</param>
        /// <param name="sourceType">int sourceType</param>
        /// <returns>string</returns>
        public string GetFileContentIdBySourceIdAndSourceType(string sourceId, int sourceType)
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(sourceId))
            {
                sourceId = sourceId.Trim();
            }

            if(string.IsNullOrEmpty(sourceId) || sourceType < 1) { return result; }

            var file = this.repo.AllReadonly<CdnFile>()
                         .Where(f => f.SourceId == sourceId && f.SourceType == sourceType && f.IsActive == true)
                         .OrderByDescending(f => f.Id)
                         .FirstOrDefault();

            if(file == null) { return result; }

            result = file.ContentId;

            return result;
        }

        /// <summary>
        /// Gets file content from CDN
        /// </summary>
        /// <param name="sourceId">Identifier of the source document</param>
        /// <param name="sourceType">Type of the attached file</param>
        /// <exception cref="ArgumentException">If file not found or file is not PDF</exception>
        /// <returns></returns>
        public (string fileName, byte[] fileContent, string contentId) GetPdfContent(string sourceId, int sourceType)
        {
            string contentId = GetFileContentIdBySourceIdAndSourceType(sourceId, sourceType);

            if (string.IsNullOrEmpty(contentId))
            {
                throw new ArgumentException("File not found");
            }

            FileSelect request = new FileSelect() { ContentID = contentId };
            FileInfo file = cdnService.Download(request);
            string type = file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower();

            if (type != ".pdf")
            {
                throw new ArgumentException("File is not pdf");
            }

            return (file.FileName, file.FileContent, contentId);
        }

        /// <summary>
        /// Dowload file from CDN
        /// </summary>
        /// <param name="contentId">Identifier of file in CDN</param>
        /// <returns></returns>
        public FileInfo Download(string contentId) 
        {
            FileSelect request = new FileSelect() { ContentID = contentId };

            return cdnService.Download(request);
        }
    }
}
