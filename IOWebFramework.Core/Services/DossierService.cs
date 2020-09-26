using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Models.Dossier;
using IOWebFramework.Core.Models.Nomenclatures;
using IOWebFramework.Infrastructure.Data.Common;
using IOWebFramework.Infrastructure.Data.Models;
using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static IOWebFramework.Shared.Common.CommonConstant;

namespace IOWebFramework.Core.Services
{
    public class DossierService : IDossierService
    {
        private readonly IRepository repo;
        private readonly ILogger<DossierService> logger;
        private readonly IMapper mapper;
        public DossierService(IRepository _repo,
                              ILogger<DossierService> _logger,
                              IMapper _mapper)
        {
            this.repo = _repo;
            this.logger = _logger;
            this.mapper = _mapper;
        }
        public Person GetPersonById(int personId)
        {
            return repo.All<Person>().FirstOrDefault(x => x.Id == personId);
        }

        //public Person GetLatestRecordId()
        //{
        //    return repo.All<Person>().FirstOrDefault(x => x.Id == personId);
        //}

        public IQueryable<DiplomaListViewModel> GetPersonDiplomas(int personId)
        {
            var filteredDiploma = repo.AllReadonly<Diploma>()
                    .Where(d => d.PersonId == personId)
                    .Include(d => d.EducationInstitution)
                    .Include(d => d.Degree)
                    .Include(d => d.Classifier);
            var result = mapper.ProjectTo<DiplomaListViewModel>(filteredDiploma);
            return result;
        }

        //public IQueryable<DiplomaListViewModel> GetEmployeeDiplomas(int employeeId)
        //{
        //    var filteredDiploma = repo.AllReadonly<Diploma>()
        //            .Where(d => d.EmployeeId == employeeId)
        //            .Include(d => d.EducationInstitution)
        //            .Include(d => d.Degree)
        //            .Include(d => d.Classifier);

        //    var result = mapper.ProjectTo<DiplomaListViewModel>(filteredDiploma);
        //    return result;
        //}


        public int DiplomaSaveData(DiplomaViewModel diplomaViewModel)
        {
            try
            {
                var diplomaFromDb = mapper.Map<Diploma>(diplomaViewModel);
                if (diplomaViewModel.Id > 0)  //edit
                {
                    repo.Update(diplomaFromDb);
                }
                else                          //add
                {
                    repo.Add<Diploma>(diplomaFromDb);
                }
                repo.SaveChanges();
                return diplomaFromDb.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Грешка при запис на диплома ({ nameof(DossierService) })");
                return default(int);
            }
        }
        public DiplomaViewModel GetDiplomaViewModelById(int diplomaId)
        {
            var model = repo.GetById<Diploma>(diplomaId);
            return mapper.Map<DiplomaViewModel>(model);
        }

        public List<SelectListItem> GetTrainingsDropDownByPersonId(int personId, bool addDefaultElement = true)
        {
            var result = repo.AllReadonly<Training>()
                                .Include(t => t.Person)
                                .Where(t => t.PersonId == personId)
                                .OrderBy(t => t.TrainingName.Label)
                              .Select(i => new SelectListItem()
                              {
                                  Text = i.TrainingName.Label,
                                  Value = i.Id.ToString()
                              }).ToList();
            if (addDefaultElement)
            {
                result = result
                    .Prepend(new SelectListItem() { Text = "Избери", Value = "-1" })
                    .ToList();
            }

            return result;
        }
        
        //public List<SelectListItem> GetTrainingsDropDownByEmployeeId(int employeeId, bool addDefaultElement = true)
        //{
        //    var result = repo.AllReadonly<Training>()
        //                        .Include(t => t.Employee)
        //                        .Where(t => t.EmployeeId == employeeId)
        //                        .OrderBy(t => t.TrainingName.Label)
        //                      .Select(i => new SelectListItem()
        //                      {
        //                          Text = i.TrainingName.Label,
        //                          Value = i.Id.ToString()
        //                      }).ToList();
        //    if (addDefaultElement)
        //    {
        //        result = result
        //            .Prepend(new SelectListItem() { Text = "Избери", Value = "-1" })
        //            .ToList();
        //    }

        //    return result;
        //}

        public IQueryable<TrainingListViewModel> GetTrainingsByPersonId(int personId)
        {
            var filteredTraining = repo.AllReadonly<Training>()
                                         .Include(t => t.TrainingName)
                                         .Include(t => t.TrainingCenter)
                                         .Where(t => (t.PersonId == personId) && (t.IsDeleted == false));
            var result = mapper.ProjectTo<TrainingListViewModel>(filteredTraining);
            return result;
        }

        //public IQueryable<CertificateListViewModel> GetCertificatesByEmployeeId(int employeeId)
        //{
        //    IQueryable<Certificate> filteredTraining = repo.AllReadonly<Certificate>()
        //                                                         .Include(c => c.CertificateNameIssuer)
        //                                                         .Include(c => c.CertificateType)
        //                                                         .Where(c => (c.EmployeeId == employeeId) && (c.IsDeleted == false));

        //    var result = mapper.ProjectTo<CertificateListViewModel>(filteredTraining);
        //    return result;
        //}
         public IQueryable<CertificateListViewModel> GetCertificatesByPersonId(int personId)
        {
            IQueryable<Certificate> filteredTraining = repo.AllReadonly<Certificate>()
                                                                 .Include(c => c.CertificateNameIssuer)
                                                                 .Include(c => c.CertificateType)
                                                                 .Where(c => (c.PersonId == personId) && (c.IsDeleted == false));

            var result = mapper.ProjectTo<CertificateListViewModel>(filteredTraining);
            return result;
        }

        public TrainingViewModel GetTrainingById(int id)
        {
            var result = repo.GetById<Training>(id);
            var trainingViewModel = mapper.Map<TrainingViewModel>(result);
            return trainingViewModel;
        }

        public int TrainingSaveData(TrainingViewModel trainingViewModel)
        {
            try
            {
                var trainingFromDb = mapper.Map<Training>(trainingViewModel);
                if (trainingViewModel.Id > 0)
                {
                    repo.Update(trainingFromDb);
                }
                else
                {
                    repo.Add<Training>(trainingFromDb);
                }
                repo.SaveChanges();
                return trainingFromDb.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Грешка при запис на обучение({ nameof(DossierService) })");
                return default(int);
            }
        }

        //public List<SelectListItem> GetTrainingsDropDownByTrainingCenterId(int trainingCenterId)
        //{
        //    var result = repo.AllReadonly<TrainingName>()
        //                     .Where(x => x.TrainingCenterId == trainingCenterId)
        //                     .Select(x =>
        //                     new SelectListItem
        //                     {
        //                         Text = x.Label,
        //                         Value = x.Id.ToString()
        //                     }).ToList();

        //    return result;
        //}

        public CertificateViewModel GetCertificateById(int id)
        {
            var result = repo.GetById<Certificate>(id);
            var certificateViewModel = mapper.Map<CertificateViewModel>(result);
            return certificateViewModel;
        }

        public int CertificateSaveData(CertificateViewModel certificateViewModel)
        {
            try
            {
                var certificateFromDb = mapper.Map<Certificate>(certificateViewModel);
                if (certificateViewModel.Id > 0)
                {
                    repo.Update(certificateFromDb);
                }
                else
                {
                    repo.Add<Certificate>(certificateFromDb);
                }
                repo.SaveChanges();
                return certificateFromDb.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Грешка при запис на сертификат({ nameof(DossierService) })");
                return default(int);
            }
        }

        //public List<SelectListItem> GetCertificateNameByCertificateTypeId(int certificateTypeId)
        //{
        //    var result = repo.AllReadonly<CertificateName>()
        //            .Where(x => x.CertificateTypeId == certificateTypeId)
        //            .Select(x =>
        //            new SelectListItem
        //            {
        //                Text = x.Label,
        //                Value = x.Id.ToString()
        //            }).ToList();
        //    return result;
        //}

        public bool DeleteTrainingById(int id)
        {
            bool result = false;
            Training entity = null;
            try
            {
                entity = repo.GetById<Training>(id);
                entity.IsDeleted = true;

                repo.Update(entity);
                repo.SaveChanges();

                result = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Грешка при изтриване на обучение ({ nameof(DossierService) })");
                result = false;
            }
            return result;
        }

        public bool DeleteCertificateById(int id)
        {
            bool result = false;
            Certificate entity = null;
            try
            {
                entity = repo.GetById<Certificate>(id);
                entity.IsDeleted = true;

                repo.Update(entity);
                repo.SaveChanges();

                result = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Грешка при изтриване на сертификат ({ nameof(DossierService) })");
                result = false;
            }
            return result;
        }

        public HierarchicalNomenclatureDisplayItem GetCertificateNameIssuerById(int id)
        {
            var result = new HierarchicalNomenclatureDisplayItem();
            var allCertificateNameIssuer = repo.AllReadonly<CertificateNameIssuer>().ToList();
            var certificateNameIssuer = allCertificateNameIssuer.FirstOrDefault(a => a.Id == id);

            if (certificateNameIssuer != null)
            {
                string certificateName = String.Empty;

                GetCertificateNameIssuer(certificateNameIssuer, certificateName, allCertificateNameIssuer, out int rootId);

                result.Id = certificateNameIssuer.Id.ToString();
                result.Label = certificateNameIssuer.Name;
                result.Category = certificateName;
            }

            return result;
        }

        public HierarchicalNomenclatureDisplayModel SearcCertificateNameIssuer(string query)
        {
            query = query?.ToLower();
            var result = new HierarchicalNomenclatureDisplayModel();
            List<CertificateNameIssuer> allActiveCertificateNameIssuers = repo.AllReadonly<CertificateNameIssuer>().Where(c => c.IsActive == true).ToList();

            var parents = allActiveCertificateNameIssuers
                .Select(a => a.ParentId)
                .Distinct()
                .ToArray();

            var certificateNameIssuers = allActiveCertificateNameIssuers
                .Where(a => !parents.Contains(a.Id))
                .Where(a => a.Name.ToLower().Contains(query))
                .ToList();

            foreach (var certificateNameIssuer in certificateNameIssuers)
            {
                string certificateName = String.Empty;
                int rootId = 0;

                if (certificateNameIssuer.ParentId != certificateNameIssuer.Id)
                {
                    var parent = allActiveCertificateNameIssuers.FirstOrDefault(p => p.Id == certificateNameIssuer.ParentId);

                    if (parent != null)
                    {
                        certificateName = GetCertificateNameIssuer(parent, certificateName, allActiveCertificateNameIssuers, out rootId);
                    }
                }

                result.Data.Add(new HierarchicalNomenclatureDisplayItem()
                {
                    Id = certificateNameIssuer.Id.ToString(),
                    Label = String.Format("{0}{1} ", string.Empty, certificateNameIssuer.Name),
                    Category = certificateName,
                    RootId = rootId
                });
            }

            result.Data = result.Data
                .OrderBy(r => r.Category)
                .ToList();

            return result;
        }

        private string GetCertificateNameIssuer(CertificateNameIssuer certificateNameIssuer, string certificateName, List<CertificateNameIssuer> allCertificateNameIssuer, out int rootId)
        {
            string currentCertificateNameIssuer = certificateNameIssuer.Name;
            rootId = certificateNameIssuer.ParentId;

            if (String.IsNullOrEmpty(certificateName))
            {
                certificateName = currentCertificateNameIssuer;
            }
            else
            {
                certificateName = String.Format("{0} {1} {2}", currentCertificateNameIssuer, HierarchicalSeparator, certificateName);
            }

            if (certificateNameIssuer.ParentId != certificateNameIssuer.Id)
            {
                var parent = allCertificateNameIssuer
                    .FirstOrDefault(p => p.Id == certificateNameIssuer.ParentId);

                if (parent != null)
                {
                    certificateName = GetCertificateNameIssuer(parent, certificateName, allCertificateNameIssuer, out rootId);
                }
            }

            return certificateName;
        }

        public bool TrainingCenterNomenclatureSaveData(string code, string trainingCenterName, string description, bool isActive)
        {
            TrainingCenter trainingCenterEntity = new TrainingCenter() { Code = code, Label = trainingCenterName, Description = description, IsActive = isActive, DateStart = DateTime.Now.AddDays(-1) };
            try
            {
                int maxOrderNumber = repo.AllReadonly<TrainingCenter>()
                       .Select(x => x.OrderNumber)
                       .OrderByDescending(x => x)
                       .FirstOrDefault();

                trainingCenterEntity.OrderNumber = maxOrderNumber + 1;
                repo.Add<TrainingCenter>(trainingCenterEntity);
                repo.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Грешка при запис на обучителен център({ nameof(DossierService) })");
                return false;
            }
        }

        public bool TrainingNameNomenclatureSaveData(string code, string trainingName, string description, bool isActive)
        {
            TrainingName trainingNameEntity = new TrainingName() { Code = code, Label = trainingName, Description = description, IsActive = isActive, DateStart = DateTime.Now.AddDays(-1) };
            try
            {
                int maxOrderNumber = repo.AllReadonly<TrainingName>()
                       .Select(x => x.OrderNumber)
                       .OrderByDescending(x => x)
                       .FirstOrDefault();

                trainingNameEntity.OrderNumber = maxOrderNumber + 1;
                repo.Add<TrainingName>(trainingNameEntity);
                repo.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Грешка при запис на Име на обучение({ nameof(DossierService) })");
                return false;
            }
        }

        public bool EducationInstitutionNomenclatureSaveData(string code, string educationInstitutionName, string description, bool isActive)
        {
            EducationInstitution educationInstitutionEntity = new EducationInstitution() { Code = code, Label = educationInstitutionName, Description = description, IsActive = isActive, DateStart = DateTime.Now.AddDays(-1) };
            try
            {
                int maxOrderNumber = repo.AllReadonly<EducationInstitution>()
                       .Select(x => x.OrderNumber)
                       .OrderByDescending(x => x)
                       .FirstOrDefault();

                educationInstitutionEntity.OrderNumber = maxOrderNumber + 1;
                repo.Add<EducationInstitution>(educationInstitutionEntity);
                repo.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Грешка при запис на Висше учебно заведение({ nameof(DossierService) })");
                return false;
            }
        }
    }
}
