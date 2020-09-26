using CDN.Core3.Data.Contracts;
using CDN.Core3.Data.Models;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Helper.GlobalConstants;
using IOWebFramework.Core.Models.Attachments;
using IOWebFramework.Extensions;
using IOWebFramework.Infrastructure.Data.Common;
using IOWebFramework.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;

namespace IOWebFramework.Controllers
{
    //[Authorize(Roles = "HR")]
    public class FileController : BaseController
    {
        private readonly ICdnService cdnRepo = null;
        private readonly IAttachedDocumentService attachedDocumentService;
        private readonly INomenclatureService nomenclatureService;
        private readonly IRepository repo;
        private readonly ILogger logger;
        private readonly IFileService fileService;

        public FileController(IAttachedDocumentService _attachedDocumentService,
                              INomenclatureService _nomenclatureService,
                              ICdnService _cdnRepo,
                              IRepository _repo,
                              ILogger<FileController> _logger,
                              IFileService fileService)
        {
            this.attachedDocumentService = _attachedDocumentService;
            this.nomenclatureService = _nomenclatureService;
            this.cdnRepo = _cdnRepo;
            this.repo = _repo;
            this.logger = _logger;
            this.fileService = fileService;
        }

        public PartialViewResult AddAttachedDocument(int documentId, int documentType)
        {
            var model = new AttachedDocumentViewModel()
            {
                DocumentId = documentId,
                DocumentType = documentType,
                IsEditMode = true,
            };

            return PartialView(model);
        }


        [HttpPost]
        public IActionResult AddAttachedDocument(AttachedDocumentViewModel model, List<IFormFile> upload_file)
        {
            try
            {
                using var scope = new TransactionScope();
                var result = this.attachedDocumentService.SaveAttachedDocument(model);
                //if (model.HasFile)
                //{
                if (upload_file.Count > 0 && upload_file != null)
                {
                    Stream fileStream = upload_file[0].OpenReadStream();
                    BinaryReader br = new BinaryReader(upload_file[0].OpenReadStream());

                    FileRequest request = new FileRequest()
                    {
                        SourceID = model.Id.ToString(),
                        SourceType = FileSourceTypeConstant.AttachedDocument,
                        FileName = upload_file[0].FileName,
                        FileContent = br.ReadBytes((int)fileStream.Length)
                    };
                    var uploadResponse = cdnRepo.Upload(request);
                    if (uploadResponse.SavedOK)
                    {
                        model.FileContentId = uploadResponse.ContentID.ToString();
                        var attachedDocumentEntity = this.repo.GetById<AttachedDocument>(result);
                        attachedDocumentEntity.FileContentId = uploadResponse.ContentID.ToString();
                        //attachedDocumentEntity.HasFile = true;
                        attachedDocumentEntity.DateUploaded = DateTime.Now;
                        this.repo.SaveChanges();
                    }
                    else
                    {
                        return Content("Проблем при качване на файл!");
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(model.FileContentId))
                    {
                        return Content("Моля изберете файл!");
                    }
                }
                //}
                if (result > 0)
                {
                    scope.Complete();
                    return Content("OK");
                }
                return Content("Проблем при прикачване на документ!");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                return Content("Проблем при прикачване на документ!");
            }
        }

        [HttpGet]
        public PartialViewResult UploadFileToAttachedDocument(long attachedDocumentId)
        {
            var model = new AttachedDocumentViewModel { Id = attachedDocumentId };

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult UploadFileToAttachedDocument(AttachedDocumentViewModel model, List<IFormFile> upload_file)
        {
            try
            {
                using var scope = new TransactionScope();
                if (model.Id > 0)
                {
                    if (upload_file.Count > 0 && upload_file != null)
                    {
                        Stream fileStream = upload_file[0].OpenReadStream();
                        BinaryReader br = new BinaryReader(upload_file[0].OpenReadStream());

                        FileRequest request = new FileRequest()
                        {
                            SourceID = model.Id.ToString(),
                            SourceType = FileSourceTypeConstant.AttachedDocument,
                            FileName = upload_file[0].FileName,
                            FileContent = br.ReadBytes((int)fileStream.Length)
                        };
                        var uploadResponse = cdnRepo.Upload(request);
                        if (uploadResponse.SavedOK)
                        {
                            var fileContentId = uploadResponse.ContentID.ToString();

                            if (this.attachedDocumentService.SaveAttachedDocument(model, fileContentId) > 0)
                            {
                                scope.Complete();
                            }
                            return Content("OK");
                        }
                    }
                    else
                    {
                        return Content("Моля изберете файл!");
                    }

                }

                return Content("Проблем при качване на файл!");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                return Content("Проблем при качване на файл!");
            }
        }

        [HttpPost]
        public IActionResult SetAttachedDocumentAsInActive(long attachedDocumentId)
        {
            var result = this.attachedDocumentService.SetAttachedDocumentAsInActive(attachedDocumentId);
            if (result == true)
            {
                return new JsonResult("OK");
            }
            return new JsonResult("Fail");
        }

        [HttpPost]
        public IActionResult HardDeleteAttachedDocument(long attachedDocumentId)
        {
            var result = this.attachedDocumentService.HardDeleteAttachedDocument(attachedDocumentId);
            if (result == true)
            {
                return new JsonResult("OK");
            }
            return new JsonResult("Fail");
        }

        [HttpPost]
        public IActionResult AttachmentDocumentsListData(IDataTablesRequest request, int documentId, int documentType)
        {
            var data = this.attachedDocumentService.GetAllAttachmentsByDocumentId(documentId, documentType);

            var response = request.GetResponse(data);

            return new DataTablesJsonResult(response, true);
        }


		public FileResult Download(string id)
        {
            if (!string.IsNullOrEmpty(id)) { id = id.Trim(); }

            FileSelect request = new FileSelect()
            {
                ContentID = id
            };

            CDN.Core3.Data.Models.FileInfo model = cdnRepo.Download(request);

            return File(model.FileContent, "application/octet-stream", model.FileName);
        }

        public IActionResult DownloadFileBySourceIdAndSourceType(string sourceId, int sourceType)
        {
            if (!string.IsNullOrEmpty(sourceId)) { sourceId = sourceId.Trim(); }

            var contentId = this.fileService.GetFileContentIdBySourceIdAndSourceType(sourceId, sourceType);

            if (string.IsNullOrEmpty(contentId)) { return new NotFoundResult(); }

            FileSelect request = new FileSelect()
            {
                ContentID = contentId
            };

            CDN.Core3.Data.Models.FileInfo model = cdnRepo.Download(request);

            return File(model.FileContent, "application/octet-stream", model.FileName);
        }

        //public PartialViewResult EditAttachedDocument(long attachedDocumentId)
        //{
        //    var model = this.attachedDocumentService.GetAttachedDocumentById(attachedDocumentId);

        //    //model.HasFile = true;    //сетваме, че има файл, за да задължим да се качи такъв, тъй като е в Едит

        //    ViewBag.AttachmentTypeId_ddl = nomenclatureService.GetDropDownList<AttachmentType>();
        //    return PartialView("AddAttachedDocument", model);
        //}

        //public PartialViewResult AttachedDocumentsTableForDocument(long documentId, int documentType, string patientFullName, string patientPersonalId, string documentNumber)
        //{
        //    if (!string.IsNullOrEmpty(patientFullName)) { patientFullName = patientFullName.Trim(); }
        //    if (!string.IsNullOrEmpty(patientPersonalId)) { patientPersonalId = patientPersonalId.Trim(); }
        //    if (!string.IsNullOrEmpty(documentNumber)) { documentNumber = documentNumber.Trim(); }

        //    var model = new AttachedDocumentsTableForDocumentsViewModel
        //    {
        //        AttachedDocumentsTableViewModel = new AttachedDocumentsTableViewModel()
        //        {
        //            HideDeleteButton = true,
        //            HideAddNewDocumentButton = true
        //        },
        //        DocumentNumber = documentNumber
        //    };

        //    return PartialView(model);
        //}
    }
}
