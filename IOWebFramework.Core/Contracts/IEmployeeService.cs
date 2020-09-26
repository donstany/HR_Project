using System.Collections.Generic;
using System.Linq;
using IOWebFramework.Core.Models;
using IOWebFramework.Core.Models.Cv;
using IOWebFramework.Core.Models.Dossier;
using IOWebFramework.Core.Models.Employees;
using IOWebFramework.Infrastructure.Data.Models;
using IOWebFramework.Shared.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IOWebFramework.Core.Contracts
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Списък за показване в таблица, всички елементи
        /// </summary>
        /// <returns></returns>
        IQueryable<EmployeeListViewModel> GetEmployees(bool isActive = true);

        /// <summary>
        /// Запазване на служителя при добавяне в базата данни
        /// </summary>
        /// <param name="model">Модел</param>
        /// <returns></returns>
        bool SaveData(EmployeeViewModel model);

        /// <summary>
        /// Списък с всички департаменти по подаден идентификатор от клона
        /// </summary>
        /// <param name="branchId">Идентификатор</param>
        /// <returns></returns>
        public List<SelectListItem> GetDepartmentsDropDownByBranchId(int branchId);

        /// <summary>
        /// Списък с всички Person, които започват с дадено начало
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public List<SelectListItem> GetPersonDropDown();

        /// <summary>
        /// Връща конкатениран стринг от трите имена и ЕГН
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public string GetPersonDetailsByPersonId(int personId);

        /// <summary>
        /// Списък с всички неактивни служители
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetInactiveEmployeesDropDown();

        /// <summary>
        /// Връща конкатениран стринг от трите имена и ЕГН
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public string GetPersonDetailsByEmployeeId(int employeeId);

        /// <summary>
        /// Връща основна информация за служителя
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public IQueryable<EmployeeMainInfoViewModel> GetEmployeeMainInfo(int employeeId);

        /// <summary>
        /// Връща EmployeeViewModel от подадено employeeId и personId
        /// </summary>
        /// <param name="employeeId">идентификатор</param>
        /// <returns></returns>
        EmployeeViewModel GetEmployeeViewModelById(int employeeId, int personId);

        /// <summary>
        /// Връща дали Тд е уникален или вече го има в ситемата
        /// </summary>
        /// <param name="tdNumber">идентификатор</param>
        /// <returns></returns>
        bool CheckTdIsUnique(string tdNumber);

        /// <summary>
        /// Връща идентификатор на човек по идентификатор на служител
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>PersonId</returns>
        public int GetPersonIdByEmployeeId(int employeeId);

        /// <summary>
        /// Връща идентификатор на последното назначение на служител
        /// </summary>
        /// <param name="personId"></param>
        /// <returns>EmployeeId</returns>
        public int GetLastEmployeeIdByPersonId(int personId);

        /// <summary>
        /// Импортира всички записи от Активната Директория
        /// </summary>
        public bool ImportDataFromAD();
        /// <summary>
        /// Импортира детайлите за запис от Активната Директория по email
        /// </summary>
        public EmployeeViewModel SyncDataFromADByEmail(string emailInAD);

        /// <summary>
        /// Връща импортираните данни от Aктивната Директория по personalId
        /// </summary>
        public EmployeeViewModel GetPersonDetails(int personalId);

        /// <summary>
        /// Връща идентификатор по подаден и-мейл
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int GetEmployeeIdByEmail(string email);
        /// <summary>
        /// взема всички хора с активно назначение
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetPersonsWithActiveEmployeesDropDown();


        /// <summary>
        /// Връща CvViewModel по идентификатор на служител
        /// </summary>
        /// <param name="employeeId">идентификатор на служител</param>
        /// <param name="personId">идентификатор на човек</param>
        /// <returns></returns>
        CvViewModel GetCvViewModelById(int employeeId);


        /// <summary>
        /// Списък с всички Дати (Години, Месеци или Дни)
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public List<SelectListItem> GetDateDropDown(int startFrom, int endTill);


        /// <summary>
        /// Синхроницзиране с Активната Директория
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        void SyncJobScheduler();

        /// <summary>
        /// Информация свързана с показване на синхронизираните данни в UI от автоматичния джоб
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public SyncedJobViewModel GetSyncInfoForUI();

    }
}
