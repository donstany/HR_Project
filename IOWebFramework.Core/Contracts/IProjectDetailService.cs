using IOWebFramework.Core.Models.Project;
using IOWebFramework.Core.Models.ProjectDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOWebFramework.Core.Contracts
{
    public interface IProjectDetailService
    {
        /// <summary>
        /// Връща името на проекта по индентификатор
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        string GetProjectNameByProjectId(int projectId);

        /// <summary>
        /// Връща детайли за проекта
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        ProjectMainInfo GetProjectMainInfo(int projectId);

        /// <summary>
        /// връща всички участници в проект по идентификатор на проекта
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        IQueryable<TeamListViewModel> GetTeam(int projectId, bool isActive = true);

        /// <summary>
        /// връща всички технологии в проект по идентификатор на проекта
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IQueryable<TechnologyListViewModel> GetTechnology(int projectId, bool isActive = true);

        /// <summary>
        /// запис на ред в базата от данни
        /// </summary>
        /// <param name="teamViewModel"></param>
        /// <returns></returns>
        bool SaveData(TeamViewModel teamViewModel);

        /// <summary>
        /// запис на ред в базата данни
        /// </summary>
        /// <param name="technologyViewModel"></param>
        /// <returns></returns>
        bool SaveDataTechnology(TechnologyViewModel technologyViewModel);

        /// <summary>
        /// връща TeamViewModel по идентификатор
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        TeamViewModel GetTeamViewModelByIds(int personId, int projectId);

        /// <summary>
        /// връща TechnologyViewModel по индентификатор
        /// </summary>
        /// <param name="projectTechnologyId"></param>
        /// <returns></returns>
        TechnologyViewModel GetTechnologyViewModelById(int projectTechnologyId);

        /// <summary>
        /// Проверка за вече съществуващ служител с съответна роля в съответен проект
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="personId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        //bool CheckUniqueProjDetailsByIds(int[] roleIds, int personId, int projectId);

        /// <summary>
        /// Проверка за вече съществуваща технология в съответен проект
        /// </summary>
        /// <param name="technologyId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        bool CheckUniqueTechAndProjByIds(int technologyId, int projectId);

        /// <summary>
        /// Изтриване на запис от базата данни
        /// </summary>
        /// <param name="technologyProjectId"></param>
        /// <returns></returns>
        bool DeleteTechnologyProjectById(int technologyProjectId);

        /// <summary>
        /// Запазване Технология към номенклатурата
        /// </summary>
        /// <param name="code">Код на технология</param>
        /// <param name="technologyName">Име на технология</param>
        /// <param name="description">Описание на технология</param>
        /// <returns></returns>
        bool TechnologyNomenclatureSaveData(string code, string technologyName, string description, bool isActive);

        /// <summary>
        /// Запазване Роля в проекта към номенклатурата
        /// </summary>
        /// <param name="code">Код на роля в проекта</param>
        /// <param name="projectRoleName">Име на роля в проекта</param>
        /// <param name="description">Описание на роля в проекта</param>
        /// <returns></returns>
        bool ProjectRoleNomenclatureSaveData(string code, string projectRoleName, string description, bool isActive);
    }
}
