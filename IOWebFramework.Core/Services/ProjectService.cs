using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Models.Project;
using IOWebFramework.Infrastructure.Data.Common;
using IOWebFramework.Infrastructure.Data.Models;
using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace IOWebFramework.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository _repo;
        private readonly ILogger<ProjectService> _logger;

        public ProjectService(IRepository repo, ILogger<ProjectService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public IQueryable<ProjectListViewModel> GetProjects(bool isActive = true)
        {
            var result = _repo.AllReadonly<Project>()
                    .Where(x => x.IsActive == isActive)
                    .Select(x => new ProjectListViewModel()
                    {
                        Id = x.Id,
                        Code = x.Code,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        Name = x.Name,
                        Client = x.Client.Label,
                        Manager = x.Person.FullName
                    });
            return result;
        }

        public ProjectViewModel GetProjectViewModelById(int projectId)
        {
            var result = _repo.AllReadonly<Project>()
                              .Where(p => p.Id == projectId)
                              .Select(p => new ProjectViewModel()
                              {
                                  Id = p.Id,
                                  Code = p.Code,
                                  Name = p.Name,
                                  FullName = p.FullName,
                                  Description = p.Description,
                                  StartDate = p.StartDate,
                                  EndDate = p.EndDate,
                                  IsActive = p.IsActive,
                                  ClientId = p.ClientId,
                                  ManagerId = p.ManagerId
                              })
                              .FirstOrDefault();
            return result;
        }

        public bool SaveData(ProjectViewModel model)
        {
            bool result = false;
            Project entity = null;
            try
            {
                if (model.Id > 0)
                {
                    entity = _repo.GetById<Project>(model.Id);
                    entity.Code = model.Code;
                    entity.Name = model.Name;
                    entity.FullName = model.FullName;
                    entity.Description = model.Description;
                    entity.StartDate = model.StartDate;
                    entity.EndDate = model.EndDate;
                    entity.ClientId = model.ClientId;
                    entity.ManagerId = model.ManagerId;
                    entity.IsActive = model.IsActive;
                    _repo.Update(entity);
                }
                else
                {
                    entity = new Project()
                    {
                        //Id = 1,
                        Code = model.Code,
                        Name = model.Name,
                        FullName = model.FullName,
                        Description = model.Description,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        IsActive = model.IsActive,
                        ClientId = model.ClientId,
                        ManagerId = model.ManagerId
                    };

                    _repo.Add<Project>(entity);
                    
                }
                
                _repo.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Грешка при запис на проект ({ nameof(ProjectService) })");
            }

            return result;
        }

        public bool ClientNomenclatureSaveData(string code, string clientName, string description, bool isActive)
        {
            Client clientEntity = new Client() { Code = code, Label = clientName, Description = description, IsActive = isActive, DateStart = DateTime.Now.AddDays(-1) };
            try
            {
                int maxOrderNumber = _repo.AllReadonly<Client>()
                       .Select(x => x.OrderNumber)
                       .OrderByDescending(x => x)
                       .FirstOrDefault();

                clientEntity.OrderNumber = maxOrderNumber + 1;
                _repo.Add<Client>(clientEntity);
                _repo.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Грешка при запис на Клиент ({ nameof(ProjectService) })");
                return false;
            }
        }
    }
}
