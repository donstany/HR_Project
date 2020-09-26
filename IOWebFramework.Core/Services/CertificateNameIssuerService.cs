using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Models;
using IOWebFramework.Core.Models.CertificateNameIssuer;
using IOWebFramework.Infrastructure.Data.Common;
using IOWebFramework.Infrastructure.Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IOWebFramework.Core.Services
{
    public class CertificateNameIssuerService : ICertificateNameIssuerService
    {
        private readonly IRepository repo;
        private readonly ILogger<CertificateNameIssuerService> logger;

        public CertificateNameIssuerService(IRepository _repo, ILogger<CertificateNameIssuerService> _logger)
        {
            repo = _repo;
            logger = _logger;
        }

        public IQueryable<CertificateNameIssuerListViewModel> GetCertificateNameIssuers(int parentId)
        {
            IQueryable<CertificateNameIssuerListViewModel> result = null;

            if (parentId > 0)
            {
                result = repo.AllReadonly<CertificateNameIssuer>()
                    .Where(a => a.ParentId == parentId)
                    .Where(a => a.Id != a.ParentId)
                    .Select(a => new CertificateNameIssuerListViewModel()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        IsActive = a.IsActive
                    });
            }
            else
            {
                result = repo.AllReadonly<CertificateNameIssuer>()
                    .Where(a => a.ParentId == a.Id)
                    .Select(a => new CertificateNameIssuerListViewModel()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        IsActive = a.IsActive
                    });
            }

            return result;
        }

        public CertificateNameIssuerViewModel GetCertificateNameIssuerViewModelById(int certificateNameIssuerId)
        {
            var model = repo.AllReadonly<CertificateNameIssuer>()
                .Where(c => c.Id == certificateNameIssuerId)
                .Select(c => new CertificateNameIssuerViewModel()
                {
                    Id = c.Id,
                    ParentId = c.ParentId,
                    Name = c.Name,
                    IsActive = c.IsActive
                })
                .FirstOrDefault();
            return model;
        }

        public bool HasChildren(int certificateNameIssuerId)
        {
            bool result = false;
            var model = repo.AllReadonly<CertificateNameIssuer>()
                            .Where(c => c.ParentId == certificateNameIssuerId && c.Id != c.ParentId);
            if (model != null && model.Any())
            {
                result = true;
            }

            return result;
        }

        public bool SaveData(CertificateNameIssuerViewModel certificateNameIssuerViewModel)
        {
            bool result = false;
            CertificateNameIssuer entity = null;
            try
            {
                if (certificateNameIssuerViewModel.Id > 0)
                {
                    entity = repo.GetById<CertificateNameIssuer>(certificateNameIssuerViewModel.Id);
                    entity.Name = certificateNameIssuerViewModel.Name;
                    entity.IsActive = certificateNameIssuerViewModel.IsActive;

                    repo.SaveChanges();
                    result = true;
                }
                else
                {
                    entity = new CertificateNameIssuer()
                    {
                        Name = certificateNameIssuerViewModel.Name,
                        IsActive = certificateNameIssuerViewModel.IsActive,
                        ParentId = certificateNameIssuerViewModel.ParentId
                    };

                    repo.Add<CertificateNameIssuer>(entity);
                    repo.SaveChanges();

                    if (entity.ParentId == 0)
                    {
                        entity.ParentId = entity.Id;
                        repo.SaveChanges();
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }

            return result;
        }
        public bool DeleteCertificateNameIssuerById(int id)
        {
            bool result = false;
            try
            {
                repo.Delete<CertificateNameIssuer>(id);
                repo.SaveChanges();

                result = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Грешка при изтриване на сертификат по издател ({ nameof(CertificateNameIssuerService) })");
                result = false;
            }
            return result;
        }

        public bool CheckIfItHasRelation(int id)
        {
            bool result = false;
            var model = repo.AllReadonly<Certificate>()
                            .Where(c => c.CertificateNameIssuerId == id);
            if (model != null && model.Any())
            {
                result = true;
            }

            return result;
        }

        public List<BreadcrumbInfoModel> GetBreadcrumbByParentId(int parentId)
        {
            var result = new List<BreadcrumbInfoModel>();
            var entity = repo.GetById<CertificateNameIssuer>(parentId);
            int level = 1000;

            result.Add(new BreadcrumbInfoModel()
            {
                Level = level,
                Id = entity.Id,
                Title = entity.Name
            });

            while (entity.Id != entity.ParentId)
            {
                level--;
                entity = repo.GetById<CertificateNameIssuer>(entity.ParentId);

                result.Add(new BreadcrumbInfoModel()
                {
                    Level = level,
                    Id = entity.Id,
                    Title = entity.Name
                });
            }

            return result;
        }
    }
}
