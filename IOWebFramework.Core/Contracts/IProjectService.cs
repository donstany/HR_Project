using IOWebFramework.Core.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOWebFramework.Core.Contracts
{
    public interface IProjectService
    {
        /// <summary>
        /// Списък за показване в таблица, всички елементи
        /// </summary>
        /// <returns></returns>
        IQueryable<ProjectListViewModel> GetProjects(bool isActive = true);

        /// <summary>
        /// Връща ProjectViewModel от подадено projectId
        /// </summary>
        /// <param name="projectId">идентификатор</param>
        /// <returns></returns>
        ProjectViewModel GetProjectViewModelById(int projectId);

        /// <summary>
        /// Запазване на проект при добавяне в базата данни
        /// </summary>
        /// <param name="model">Модел</param>
        /// <returns></returns>
        bool SaveData(ProjectViewModel model);

        /// <summary>
        /// Запазване Клиент към номенклатурата
        /// </summary>
        /// <param name="code">Код на Клиент</param>
        /// <param name="clientName">Име на клиент</param>
        /// <param name="description">Описание на клиент</param>
        /// <returns></returns>
        bool ClientNomenclatureSaveData(string code, string clientName, string description, bool isActive);
    }
}
