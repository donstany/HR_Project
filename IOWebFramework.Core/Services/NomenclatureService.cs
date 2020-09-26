using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Extensions;
using IOWebFramework.Core.Models.Nomenclatures;
using IOWebFramework.Infrastructure.Contracts;
using IOWebFramework.Infrastructure.Data.Common;
using IOWebFramework.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IOWebFramework.Core.Services
{
    public class NomenclatureService : INomenclatureService
    {
        private readonly ILogger logger;

        private readonly IRepository repo;

        public NomenclatureService(
            ILogger<NomenclatureService> _logger,
            IRepository _repo)
        {
            logger = _logger;
            repo = _repo;
        }

        public HierarchicalNomenclatureDisplayModel GetEkatte(string query)
        {
            var result = new HierarchicalNomenclatureDisplayModel();
            query = query?.ToLower();

            var ekatte = repo.AllReadonly<EkEkatte>()
                .Include(e => e.Munincipality)
                .Include(e => e.District)
                .Where(e => e.Name.ToLower().Contains(query ?? e.Name.ToLower()))
                .Select(e => new HierarchicalNomenclatureDisplayItem()
                {
                    Id = e.Ekatte,
                    Label = String.Format("{0} {1}", e.TVM, e.Name),
                    Category = String.Format("общ. {0}, обл. {1}", e.Munincipality.Name, e.District.Name)
                });

            result.Data.AddRange(ekatte);

            var sobr = repo.AllReadonly<EkSobr>()
                .Where(s => s.Name.ToLower().Contains(query ?? s.Name.ToLower()))
                .Select(s => new HierarchicalNomenclatureDisplayItem()
                {
                    Id = s.Ekatte,
                    Label = s.Name,
                    Category = s.Area1 != null ? s.Area1.Substring(s.Area1.IndexOf(')') + 1).Trim() : "Селищни образования"
                });

            result.Data.AddRange(sobr);

            result.Data = result.Data
                .OrderBy(d => d.Category)
                .ToList();

            return result;
        }

        public HierarchicalNomenclatureDisplayItem GetEkatteById(string id)
        {
            var result = repo.AllReadonly<EkEkatte>()
                .Include(e => e.Munincipality)
                .Include(e => e.District)
                .Where(e => e.Ekatte == id)
                .Select(e => new HierarchicalNomenclatureDisplayItem()
                {
                    Id = e.Ekatte,
                    Label = String.Format("{0} {1}", e.TVM, e.Name),
                    Category = String.Format("общ. {0}, обл. {1}", e.Munincipality.Name, e.District.Name)
                })
                .FirstOrDefault();

            if (result == null)
            {
                result = repo.AllReadonly<EkSobr>()
                .Where(s => s.Ekatte == id)
                .Select(s => new HierarchicalNomenclatureDisplayItem()
                {
                    Id = s.Ekatte,
                    Label = s.Name,
                    Category = s.Area1 != null ? s.Area1.Substring(s.Area1.IndexOf(')') + 1).Trim() : "Селищни образования"
                })
                .FirstOrDefault();
            }

            return result;
        }

        public IEnumerable<LabelValueVM> GetStreet(string ekatte, string query)
        {
            query = query?.ToLower();
            return repo.AllReadonly<EkStreet>().Where(x => x.Ekatte == ekatte && x.Name.ToLower().Contains(query))
                        .OrderBy(x => x.Name)
                        .Select(x => new LabelValueVM
                        {
                            Value = x.Code,
                            Label = x.Name
                        });
        }

        public LabelValueVM GetStreetByCode(string ekatte, string code)
        {
            return repo.AllReadonly<EkStreet>().Where(x => x.Ekatte == ekatte && x.Code == code)
                        .OrderBy(x => x.Name)
                        .Select(x => new LabelValueVM
                        {
                            Value = x.Code,
                            Label = x.Name
                        }).FirstOrDefault();
        }

        public bool ChangeOrder<T>(ChangeOrderModel model) where T : class, ICommonNomenclature
        {
            bool result = false;

            try
            {
                var nomList = repo.All<T>()
                    .ToList();

                int maxOrderNumber = nomList
                    .Max(x => x.OrderNumber);
                int minOrderNumber = nomList
                    .Min(x => x.OrderNumber);
                var currentElement = nomList
                    .Where(x => x.Id == model.Id)
                    .FirstOrDefault();

                if (currentElement != null)
                {
                    if (model.Direction == "up" && currentElement.OrderNumber > minOrderNumber)
                    {
                        var previousElement = nomList
                            .Where(x => x.OrderNumber == currentElement.OrderNumber - 1)
                            .FirstOrDefault();

                        if (previousElement != null)
                        {
                            previousElement.OrderNumber = currentElement.OrderNumber;
                        }

                        currentElement.OrderNumber -= 1;
                    }

                    if (model.Direction == "down" && currentElement.OrderNumber < maxOrderNumber)
                    {
                        var nextElement = nomList
                            .Where(x => x.OrderNumber == currentElement.OrderNumber + 1)
                            .FirstOrDefault();

                        if (nextElement != null)
                        {
                            nextElement.OrderNumber = currentElement.OrderNumber;
                        }

                        currentElement.OrderNumber += 1;
                    }

                    repo.SaveChanges();
                }

                result = true;
            }
            catch (Exception ex)
            {
                logger.LogError("ChangeOrder", ex);
            }

            return result;
        }

        public List<SelectListItem> GetDropDownList<T>(bool addDefaultElement, bool addAllElement, bool orderByNumber) where T : class, ICommonNomenclature
        {
            var result = repo.AllReadonly<T>()
                        .ToSelectList(addDefaultElement, addAllElement, orderByNumber);

            return result;
        }

        public T GetItem<T>(int id) where T : class, ICommonNomenclature
        {
            var item = repo.GetById<T>(id);

            return item;
        }

        public IQueryable<CommonNomenclatureListItem> GetList<T>() where T : class, ICommonNomenclature
        {
            return repo.AllReadonly<T>()
                .Select(x => new CommonNomenclatureListItem()
                {
                    Id = x.Id,
                    Code = x.Code,
                    DateStart = x.DateStart,
                    DateEnd = x.DateEnd,
                    Description = x.Description,
                    IsActive = x.IsActive,
                    Label = x.Label,
                    OrderNumber = x.OrderNumber
                }).OrderBy(x => x.OrderNumber);
        }

        public IQueryable<CommonNomenclatureListItem> GetActiveList<T>() where T : class, ICommonNomenclature
        {
            DateTime now = DateTime.Today;
            return repo.AllReadonly<T>()
                .Where(n => n.IsActive && n.DateStart <= now)
                .Where(n => n.DateEnd == null || n.DateEnd >= now)
                .Select(x => new CommonNomenclatureListItem()
                {
                    Id = x.Id,
                    Code = x.Code,
                    DateStart = x.DateStart,
                    DateEnd = x.DateEnd,
                    Description = x.Description,
                    IsActive = x.IsActive,
                    Label = x.Label,
                    OrderNumber = x.OrderNumber
                }).OrderBy(x => x.OrderNumber);
        }

        public bool SaveItem<T>(T entity) where T : class, ICommonNomenclature
        {
            bool result = false;

            try
            {
                if (entity.Id > 0)
                {
                    repo.Update(entity);
                }
                else
                {
                    int maxOrderNumber = repo.AllReadonly<T>()
                        .Select(x => x.OrderNumber)
                        .OrderByDescending(x => x)
                        .FirstOrDefault();

                    entity.OrderNumber = maxOrderNumber + 1;
                    repo.Add(entity);
                }

                repo.SaveChanges();

                result = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Грешка при запис на номенклатура ({ typeof(T).ToString() })");
            }

            return result;
        }
    }
}
