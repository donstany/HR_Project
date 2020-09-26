using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Models.Attachments;
using IOWebFramework.Infrastructure.Data.Common;
using IOWebFramework.Infrastructure.Data.Models;
using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using DocumentTypesConstant = IOWebFramework.Shared.Common.DocumentTypesConstant;

namespace IOWebFramework.Core.Services.AttachedDocuments
{
    /// <summary>
    /// Manipulates AttachedDocument data
    /// </summary>
    public class AttachedDocumentService : IAttachedDocumentService
    {
        private readonly ILogger logger;
        private readonly IRepository repo;
        //private readonly IDossierService dossierService;

        public AttachedDocumentService(
            ILogger<AttachedDocumentService> logger,
            IRepository _repo)
        {
            this.logger = logger;
            this.repo = _repo;
        }

        /// <summary>
        /// Adds/Saves AttachedDocument and returns Id of the entity
        /// </summary>
        /// <param name="model">AttachedDocumentViewModel</param>
        /// <param name="fileContentId">Id of fileContent</param>
        /// <returns>long</returns>
        public long SaveAttachedDocument(AttachedDocumentViewModel model, string fileContentId = null)
        {
            long result = 0;
            try
            {
                AttachedDocument entity = null;

                if (model.Id > 0)       //Find AttachedDocument
                {
                    entity = this.repo.GetById<AttachedDocument>(model.Id);

                    if (entity != null)  //override
                    {
                        if (fileContentId != null)
                        {
                            //entity.HasFile = true;
                            entity.FileContentId = fileContentId;
                            entity.DateUploaded = DateTime.Now;
                        }
                        entity.Number = model.Number;
                        entity.Date = model.Date;
                        entity.Description = model.Description;
                        //entity.AttachmentTypeId = model.AttachmentTypeId;
                    }
                }
                else                    //Create AttachedDocument
                {
                    entity = new AttachedDocument
                    {
                        IsActive = true,
                        Number = model.Number,
                        //HasFile = model.HasFile,
                        Date = model.Date,
                        Description = model.Description,
                        //AttachmentTypeId = model.AttachmentTypeId,
                        FileContentId = model.FileContentId
                    };

                    if (fileContentId != null)
                    {
                        entity.DateUploaded = DateTime.Now;
                    }

                    switch (model.DocumentType)
                    {
                        case DocumentTypesConstant.Diploma:
                            var diploma = this.repo.GetById<Diploma>(model.DocumentId);
                            if (diploma != null)
                            {
                                diploma.DiplomaAttachments.Add(new DiplomaAttachment
                                {
                                    AttachedDocument = entity
                                });
                            }
                            break;

                        case DocumentTypesConstant.Certificate:
                            var certificate = this.repo.GetById<Certificate>(model.DocumentId);
                            if (certificate != null)
                            {
                                certificate.CertificateAttachments.Add(new CertificateAttachment
                                {
                                    AttachedDocument = entity
                                });
                            }
                            break;  

                        case DocumentTypesConstant.Training:
                            var training = this.repo.GetById<Training>(model.DocumentId);
                            if (training != null)
                            {
                                training.TrainingAttachments.Add(new TrainingAttachment
                                {
                                    AttachedDocument = entity
                                });
                            }
                            break;

                        default:
                            return result;
                    }

                    this.repo.Add<AttachedDocument>(entity);
                }
                this.repo.SaveChanges();
                model.Id = entity.Id;
                result = entity.Id;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Returns collection of AttachedDocuments
        /// </summary>
        /// <param name="documentId">Id of Medical Document</param>
        /// <param name="documentType">Type of Medical Document</param>
        /// <returns>IQueryable<AttachedDocumentViewModel></returns>
        public IQueryable<AttachedDocumentViewModel> GetAllAttachmentsByDocumentId(int documentId, int documentType)
        {
            //var attachmentTypeNames = this.repo.All<AttachmentType>()
            //    .Where(a => a.IsActive == true)
            //    .ToDictionary(t => t.Id, t => t.Label);

            IQueryable<AttachedDocumentViewModel> result = new List<AttachedDocumentViewModel>().AsQueryable();

            if (documentType == DocumentTypesConstant.Diploma)
            {
                var document = this.repo.All<Diploma>()
                .Where(d => d.Id == documentId)
                .Include(d => d.DiplomaAttachments)
                .ThenInclude(da => da.AttachedDocument)
                .FirstOrDefault();

                result = document?.DiplomaAttachments
                .Where(da => da.AttachedDocument.IsActive == true)
                .Select(da => new AttachedDocumentViewModel
                {
                    Id = da.AttachedDocument.Id,
                    Number = da.AttachedDocument.Number,
                    IsActive = da.AttachedDocument.IsActive,
                    //AttachmentTypeId = da.AttachedDocument.AttachmentTypeId,
                    //AttachmentTypeName = attachmentTypeNames[da.AttachedDocument.AttachmentTypeId],
                    Date = da.AttachedDocument.Date,
                    Description = da.AttachedDocument.Description,
                    //HasFile = da.AttachedDocument.HasFile,
                    DocumentId = documentId,
                    DocumentType = DocumentTypesConstant.Diploma,
                    FileContentId = da.AttachedDocument.FileContentId,
                    DateUploaded = da.AttachedDocument.DateUploaded,
                })
                .ToList()
                .AsQueryable();
            }

            else if (documentType == DocumentTypesConstant.Certificate)
            {
                var document = this.repo.All<Certificate>()
                .Where(x => x.Id == documentId)
                .Include(x => x.CertificateAttachments)
                .ThenInclude(xx => xx.AttachedDocument)
                .FirstOrDefault();

                result = document?.CertificateAttachments
                .Where(da => da.AttachedDocument.IsActive == true)
                .Select(da => new AttachedDocumentViewModel
                {
                    Id = da.AttachedDocument.Id,
                    Number = da.AttachedDocument.Number,
                    IsActive = da.AttachedDocument.IsActive,
                    //AttachmentTypeId = da.AttachedDocument.AttachmentTypeId,
                    //AttachmentTypeName = attachmentTypeNames[da.AttachedDocument.AttachmentTypeId],
                    Date = da.AttachedDocument.Date,
                    Description = da.AttachedDocument.Description,
                    //HasFile = da.AttachedDocument.HasFile,
                    DocumentId = documentId,
                    DocumentType = DocumentTypesConstant.Certificate,
                    FileContentId = da.AttachedDocument.FileContentId,
                    DateUploaded = da.AttachedDocument.DateUploaded,
                })
                .ToList()
                .AsQueryable();
            }
            else if (documentType == DocumentTypesConstant.Training)
            {
                var document = this.repo.All<Training>()
                .Where(x => x.Id == documentId)
                .Include(x => x.TrainingAttachments)
                .ThenInclude(xx => xx.AttachedDocument)
                .FirstOrDefault();

                result = document?.TrainingAttachments
                .Where(da => da.AttachedDocument.IsActive == true)
                .Select(da => new AttachedDocumentViewModel
                {
                    Id = da.AttachedDocument.Id,
                    Number = da.AttachedDocument.Number,
                    IsActive = da.AttachedDocument.IsActive,
                    //AttachmentTypeId = da.AttachedDocument.AttachmentTypeId,
                    //AttachmentTypeName = attachmentTypeNames[da.AttachedDocument.AttachmentTypeId],
                    Date = da.AttachedDocument.Date,
                    Description = da.AttachedDocument.Description,
                    //HasFile = da.AttachedDocument.HasFile,
                    DocumentId = documentId,
                    DocumentType = DocumentTypesConstant.Training,
                    FileContentId = da.AttachedDocument.FileContentId,
                    DateUploaded = da.AttachedDocument.DateUploaded,
                })
                .ToList()
                .AsQueryable();
            }

            if (result == null) result = new List<AttachedDocumentViewModel>().AsQueryable();

            return result;
        }

        /// <summary>
        /// Sets AttachmentDocument as InActive by given Id
        /// </summary>
        /// <param name="attachedDocumentId">Id of AttachmentDocument</param>
        /// <returns>bool</returns>
        public bool SetAttachedDocumentAsInActive(long attachedDocumentId)
        {
            var result = false;
            try
            {
                var entity = this.repo.GetById<AttachedDocument>(attachedDocumentId);
                if (entity != null)
                {
                    entity.IsActive = false;
                    this.repo.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
            }
            return result;
        }
         /// <summary>
        /// Deletes AttachmentDocument from DB by given Id
        /// </summary>
        /// <param name="attachedDocumentId">Id of AttachmentDocument</param>
        /// <returns>bool</returns>
        public bool HardDeleteAttachedDocument(long attachedDocumentId)
        {
            var result = false;
            try
            {
                var entity = this.repo.GetById<AttachedDocument>(attachedDocumentId);
                if (entity != null)
                {
                    this.repo.Delete<AttachedDocument>(attachedDocumentId);

                    this.repo.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Gets AttachedDocument by given Id
        /// </summary>
        /// <param name="attachedDocumentId">Id of AttachedDocument</param>
        /// <returns>AttachedDocumentViewModel</returns>
        public AttachedDocumentViewModel GetAttachedDocumentById(long attachedDocumentId)
        {
            var attachedDocument = this.repo.AllReadonly<AttachedDocument>()
                .Select(ad => new AttachedDocumentViewModel
                {
                    Id = ad.Id,
                    //AttachmentTypeId = ad.AttachmentTypeId,
                    Date = ad.Date,
                    Description = ad.Description,
                    FileContentId = ad.FileContentId,
                    //HasFile = string.IsNullOrEmpty(ad.FileContentId) ? false : true,
                    IsActive = ad.IsActive,
                    Number = ad.Number,
                    IsEditMode = true,
                    // activityId = ad.activityId,
                    DateUploaded = ad.DateUploaded,
                    //activityName = ad.activityId != null ? ad.activity.Activity : null
                })
                .FirstOrDefault(ad => ad.Id == attachedDocumentId);

            return attachedDocument;
        }
    }
}
