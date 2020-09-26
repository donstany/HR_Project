using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Models.Project;
using IOWebFramework.Core.Models.ProjectDetail;
using IOWebFramework.Infrastructure.Data.Common;
using IOWebFramework.Infrastructure.Data.Models;
using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IOWebFramework.Core.Services
{
    public class ProjectDetailService : IProjectDetailService
    {
        private readonly IRepository _repo;
        private readonly ILogger<ProjectDetailService> _logger;

        public ProjectDetailService(IRepository repo, ILogger<ProjectDetailService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public IQueryable<TeamListViewModel> GetTeam(int projectId, bool isActive = true)
        {
            var teams = _repo.AllReadonly<Team>()
                             .Include(x => x.Person)
                             .ThenInclude(x => x.Employee)
                             .Include(x => x.ProjectRole)
                             .Where(c => c.ProjectId == projectId && c.IsActive == isActive)
                             .ToList();

            var result = new List<TeamListViewModel>();
            foreach (var team in teams)
            {
                if (!result.Any(x => x.PersonId == team.PersonId))
                {
                    result.Add(new TeamListViewModel()
                    {
                        Name = team.Person.FullName,
                        Department = team.Person.Employee.OrderByDescending(e => e.HireDate).FirstOrDefault() != null ? team.Person.Employee.OrderByDescending(e => e.HireDate).FirstOrDefault().Departament : string.Empty,
                        ProjectRole = string.Join(',', teams.Where(x => x.PersonId == team.PersonId).Select(c => c.ProjectRole.Label)),
                        StartDate = team.StartDate,
                        EndDate = team.EndDate,
                        PersonId = team.PersonId,
                        ProjectId = team.ProjectId
                    });
                }
            }

            //var result = _repo.AllReadonly<Team>()
            //                  .Where(c => c.ProjectId == projectId && c.IsActive == isActive)
            //                  .GroupBy(g => g.PersonId)
            //                  .Select(g => new TeamListViewModel()
            //                  {
            //                      Id = g.FirstOrDefault().Id,
            //                      Name = g.FirstOrDefault().Person.FullName,
            //                      Department = g.FirstOrDefault().Person.Employee.OrderByDescending(e => e.HireDate).First() != null ? g.FirstOrDefault().Person.Employee.OrderByDescending(e => e.HireDate).First().Departament : string.Empty,
            //                      ProjectRole = string.Join(',', g.Select(c => c.ProjectRole.Label)),
            //                      StartDate = g.FirstOrDefault().StartDate,
            //                      EndDate = g.FirstOrDefault().EndDate
            //                  }).ToList();
            return result.AsQueryable();
        }

        public TeamViewModel GetTeamViewModelByIds(int personId, int projectId)
        {
            var result = _repo.AllReadonly<Team>()
                              .Include(c => c.ProjectRole)
                              .Where(c => c.PersonId == personId && c.ProjectId == projectId)
                              .Select(c => new TeamViewModel()
                              {
                                  Id = c.Id,
                                  StartDate = c.StartDate,
                                  EndDate = c.EndDate,
                                  IsActive = c.IsActive,
                                  ProjectId = c.ProjectId,
                                  PersonId = c.PersonId
                              }).FirstOrDefault();
            if (result != null)
            {
                List<int> roles = _repo.AllReadonly<Team>()
                                       .Where(t => t.PersonId == result.PersonId && t.ProjectId == result.ProjectId)
                                       .Select(t => t.ProjectRoleId)
                                       .ToList();

                result.ProjectRoles = roles;
            }

            return result;
        }

        public ProjectMainInfo GetProjectMainInfo(int projectId)
        {
            var res = _repo.AllReadonly<Project>()
                           .Where(p => p.Id == projectId)
                           .Select(p => new ProjectMainInfo()
                           {
                               Code = p.Code,
                               Name = p.Name,
                               FullName = p.FullName,
                               StartDate = p.StartDate,
                               EndDate = p.EndDate,
                               Description = p.Description,
                               Client = p.Client.Description,
                               Manager = p.Person.FullName
                           }).FirstOrDefault();
            return res;
        }

        public string GetProjectNameByProjectId(int projectId)
        {
            var result = _repo.GetById<Project>(projectId);
            if (result == null)
            {
                return string.Empty;
            }
            else
            {
                return result.FullName;
            }
        }

        public bool SaveData(TeamViewModel teamViewModel)
        {
            bool result = false;
            Team[] entities = new Team[teamViewModel.ProjectRoles.Count];

            try
            {
                //first we check we there are old entities. We delete them and after that add new entities
                if (teamViewModel.Id > 0) //update
                {
                    var old = _repo.AllReadonly<Team>().FirstOrDefault(t => t.Id == teamViewModel.Id);
                    if(old.PersonId != teamViewModel.PersonId)
                    {
                        var oldEntities = _repo.AllReadonly<Team>()
                        .Where(t => t.PersonId == old.PersonId && t.ProjectId == old.ProjectId);

                        _repo.DeleteRange<Team>(oldEntities);
                    }

                    var entitiesToEdit = _repo.AllReadonly<Team>()
                                                .Where(t => t.PersonId == teamViewModel.PersonId && t.ProjectId == teamViewModel.ProjectId);

                    _repo.DeleteRange<Team>(entitiesToEdit);
                }

                for (int i = 0; i < teamViewModel.ProjectRoles.Count; i++)
                {
                    entities[i] = new Team()
                    {
                        StartDate = teamViewModel.StartDate,
                        EndDate = teamViewModel.EndDate,
                        ProjectId = teamViewModel.ProjectId,
                        PersonId = teamViewModel.PersonId,
                        IsActive = teamViewModel.IsActive,
                        ProjectRoleId = teamViewModel.ProjectRoles[i]
                    };
                }

                _repo.AddRange(entities);
                _repo.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Грешка при запис на проект ({ nameof(ProjectDetailService) })");
            }

            return result;

        }

        //public bool CheckUniqueProjDetailsByIds(int[] roleIds, int personId, int projectId)
        //{
        //    var data = _repo.AllReadonly<Team>()
        //                      .Where(c => c.ProjectId == projectId && c.PersonId == personId);

        //    foreach (var item in data)
        //    {
        //        for (int i = 0; i < roleIds.Length; i++)
        //        {
        //            if(item.ProjectRoleId == roleIds[i])
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}

        public IQueryable<TechnologyListViewModel> GetTechnology(int projectId, bool isActive = true)
        {
            var result = _repo.AllReadonly<TechnologyProject>()
                               .Where(t => t.ProjectId == projectId)
                               .Select(t => new TechnologyListViewModel()
                               {
                                   Id = t.Id,
                                   Name = t.Technology.Label,
                                   Description = t.Technology.Description
                               }
                               );

            return result;
        }

        public TechnologyViewModel GetTechnologyViewModelById(int projectTechnologyId)
        {
            var result = _repo.AllReadonly<TechnologyProject>()
                              .Where(t => t.Id == projectTechnologyId)
                              .Select(t => new TechnologyViewModel()
                              {
                                  Id = t.Id,
                                  ProjectId = t.ProjectId,
                                  TechnologyId = t.TechnologyId
                              })
                              .FirstOrDefault();
            return result;
        }

        public bool SaveDataTechnology(TechnologyViewModel technologyViewModel)
        {
            bool result = false;
            TechnologyProject entity = null;

            try
            {
                if (technologyViewModel.Id > 0) //update
                {
                    entity = _repo.GetById<TechnologyProject>(technologyViewModel.Id);
                    entity.ProjectId = technologyViewModel.ProjectId;
                    entity.TechnologyId = technologyViewModel.TechnologyId;
                    _repo.Update<TechnologyProject>(entity);
                }
                else //insert
                {
                    entity = new TechnologyProject()
                    {
                        ProjectId = technologyViewModel.ProjectId,
                        TechnologyId = technologyViewModel.TechnologyId
                    };
                    _repo.Add<TechnologyProject>(entity);
                }
                _repo.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Грешка при запис на проект ({ nameof(ProjectDetailService) })");
            }

            return result;
        }

        public bool DeleteTechnologyProjectById(int technologyProjectId)
        {
            bool result = false;

            try
            {
                _repo.Delete<TechnologyProject>(technologyProjectId);
                _repo.SaveChanges();

                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Грешка при изтриване на технология от дадения проект ({ nameof(ProjectDetailService) })");
                result = false;
            }

            return result;
        }

        public bool CheckUniqueTechAndProjByIds(int technologyId, int projectId)
        {
            var result = _repo.AllReadonly<TechnologyProject>()
                  .FirstOrDefault(t => t.ProjectId == projectId && t.TechnologyId == technologyId) == null ? true : false;
            return result;
        }

        public bool TechnologyNomenclatureSaveData(string code, string technologyName, string description, bool isActive)
        {
            Technology technologyEntity = new Technology() { Code = code, Label = technologyName, Description = description, IsActive = isActive, DateStart = DateTime.Now.AddDays(-1) };
            try
            {
                int maxOrderNumber = _repo.AllReadonly<Technology>()
                       .Select(x => x.OrderNumber)
                       .OrderByDescending(x => x)
                       .FirstOrDefault();

                technologyEntity.OrderNumber = maxOrderNumber + 1;
                _repo.Add<Technology>(technologyEntity);
                _repo.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Грешка при запис на Технология({ nameof(ProjectDetailService) })");
                return false;
            }
        }

        public bool ProjectRoleNomenclatureSaveData(string code, string projectRoleName, string description, bool isActive)
        {
            ProjectRole projectRoleEntity = new ProjectRole() { Code = code, Label = projectRoleName, Description = description, IsActive = isActive, DateStart = DateTime.Now.AddDays(-1) };
            try
            {
                int maxOrderNumber = _repo.AllReadonly<ProjectRole>()
                       .Select(x => x.OrderNumber)
                       .OrderByDescending(x => x)
                       .FirstOrDefault();

                projectRoleEntity.OrderNumber = maxOrderNumber + 1;
                _repo.Add<ProjectRole>(projectRoleEntity);
                _repo.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Грешка при запис на Роля в проекта ({ nameof(ProjectDetailService) })");
                return false;
            }
        }
    }
}
