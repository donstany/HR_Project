using System;
using System.Collections.Generic;
using System.Linq;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Models;
using IOWebFramework.Core.Models.Classifier;
using IOWebFramework.Core.Models.Nomenclatures;
using IOWebFramework.Infrastructure.Data.Common;
using IOWebFramework.Infrastructure.Data.Models;
using Microsoft.Extensions.Logging;
using static IOWebFramework.Shared.Common.CommonConstant;

namespace IOWebFramework.Core.Services
{
    public class ClassifierService : IClassifierService
    {
        private readonly IRepository repo;
        private readonly ILogger<ClassifierService> logger;

        public ClassifierService(IRepository _repo, ILogger<ClassifierService> _logger)
        {
            repo = _repo;
            logger = _logger;
        }

        public HierarchicalNomenclatureDisplayItem GetSpecialtyById(int id)
        {
            var result = new HierarchicalNomenclatureDisplayItem();
            var allSpecialties = repo.AllReadonly<Classifier>().ToList();
            var specialty = allSpecialties.FirstOrDefault(a => a.Id == id);

            if (specialty != null)
            {
                string category = String.Empty;
                //int rootId = 0;

                GetCategory(specialty, category, allSpecialties, out int rootId);

                result.Id = specialty.Id.ToString();
                result.Label = specialty.Name;
                result.Category = category;
            }

            return result;
        }

        public HierarchicalNomenclatureDisplayModel SearchActivity(string query)
        {
            query = query?.ToLower();
            var result = new HierarchicalNomenclatureDisplayModel();
            List<Classifier> allActiveSpecialties = repo.AllReadonly<Classifier>().Where(c => c.IsActive == true).ToList();

            var parents = allActiveSpecialties
                .Select(a => a.ParentId)
                .Distinct()
                .ToArray();

            var specialties = allActiveSpecialties
                .Where(a => !parents.Contains(a.Id))
                .Where(a => a.Name.ToLower().Contains(query))
                .ToList();

            foreach (var specialty in specialties)
            {
                string category = String.Empty;
                int rootId = 0;

                if (specialty.ParentId != specialty.Id)
                {
                    var parent = allActiveSpecialties.FirstOrDefault(p => p.Id == specialty.ParentId);

                    if (parent != null)
                    {
                        category = GetCategory(parent, category, allActiveSpecialties, out rootId);
                    }
                }

                result.Data.Add(new HierarchicalNomenclatureDisplayItem()
                {
                    Id = specialty.Id.ToString(),
                    Label = String.Format("{0}{1}", HierarchicalSeparator, specialty.Name).TrimStart(HierarchicalSeparator.ToCharArray()),
                    Category = category,
                    RootId = rootId
                });
            }

            result.Data = result.Data
                .OrderBy(r => r.Category)
                .ToList();

            return result;
        }

        private string GetCategory(Classifier specialty, string category, List<Classifier> allSpecialties, out int rootId)
        {
            string currentCategory = specialty.Name;
            rootId = specialty.ParentId;

            if (String.IsNullOrEmpty(category))
            {
                category = currentCategory;
            }
            else
            {
                category = String.Format("{0} -> {1}", currentCategory, category);
            }

            if (specialty.ParentId != specialty.Id)
            {
                var parent = allSpecialties
                    .FirstOrDefault(p => p.Id == specialty.ParentId);

                if (parent != null)
                {
                    category = GetCategory(parent, category, allSpecialties, out rootId);
                }
            }

            return category;
        }

        /// <summary>
        /// Връща списък с области за визуализация
        /// </summary>
        /// <param name="parentId">Идентификатор на родител за йерархична номенклатура</param>
        /// <returns></returns>
        public IQueryable<ClassifierListViewModel> GetClassifiers(int parentId)
        {
            IQueryable<ClassifierListViewModel> result = null;
           
            if (parentId > 0)
            {
                result = repo.AllReadonly<Classifier>()
                    .Where(a => a.ParentId == parentId)
                    .Where(a => a.Id != a.ParentId)
                    .Select(a => new ClassifierListViewModel()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        IsActive = a.IsActive
                    });
            }
            else
            {
                result = repo.AllReadonly<Classifier>()
                    .Where(a => a.ParentId == a.Id)
                    .Select(a => new ClassifierListViewModel()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        IsActive = a.IsActive
                    });
            }

            return result;
        }

        public ClassifierViewModel GetClassifierViewModelById(int classifierId)
        {
            var model = repo.AllReadonly<Classifier>()
                            .Where(c => c.Id == classifierId)
                            .Select(c => new ClassifierViewModel()
                            {
                                Id = c.Id,
                                ParentId = c.ParentId,
                                Name = c.Name,
                                IsActive = c.IsActive
                            })
                            .FirstOrDefault();
            return model;
        }

        public bool SaveData(ClassifierViewModel classifierViewModel)
        {
            bool result = false;
            Classifier entity = null;
            try
            {
                if(classifierViewModel.Id > 0)
                {
                    entity = repo.GetById<Classifier>(classifierViewModel.Id);
                    entity.Name = classifierViewModel.Name;
                    entity.IsActive = classifierViewModel.IsActive;

                    repo.SaveChanges();
                    result = true;
                }
                else
                {
                    entity = new Classifier()
                    {
                        Name = classifierViewModel.Name,
                        IsActive = classifierViewModel.IsActive,
                        ParentId = classifierViewModel.ParentId
                    };

                    repo.Add<Classifier>(entity);
                    repo.SaveChanges();

                    if(entity.ParentId == 0)
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

        public bool HasChildren(int classifierId)
        {
            bool result = false;
            var model = repo.AllReadonly<Classifier>()
                            .Where(c => c.ParentId == classifierId);
            if(model != null && model.Any())
            {
                result = true;
            }
            
            return result;
        }

        public List<BreadcrumbInfoModel> GetBreadcrumbByParentId(int parentId)
        {
            var result = new List<BreadcrumbInfoModel>();
            var entity = repo.GetById<Classifier>(parentId);
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
                entity = repo.GetById<Classifier>(entity.ParentId);

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
