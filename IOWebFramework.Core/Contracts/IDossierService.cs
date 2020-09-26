using System.Collections.Generic;
using System.Linq;
using IOWebFramework.Core.Models.Dossier;
using IOWebFramework.Core.Models.Employees;
using IOWebFramework.Core.Models.Nomenclatures;
using IOWebFramework.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IOWebFramework.Core.Contracts
{
    public interface IDossierService
    {

        /// <summary>
        /// Намиране на човек по ID
        /// </summary>
        /// <param name="personId">Идентификатор на човек</param>
        /// <returns>Person</returns>
        Person GetPersonById(int personId); 
        
        /// <summary>
        /// Списък за показване на дипломите в таблица, всички елементи
        /// </summary>
        /// <param name="personId">Идентификатор на човек</param>
        /// <returns></returns>
        IQueryable<DiplomaListViewModel> GetPersonDiplomas(int personId);

        ///// <summary>
        ///// Списък за показване в таблица, всички елементи
        ///// </summary>
        ///// <param name="employeeId">Идентификатор на служител</param>
        ///// <returns></returns>
        //IQueryable<DiplomaListViewModel> GetEmployeeDiplomas(int employeeId);

        /// <summary>
        /// Запазване на промени по/Добавяне на диплома на служител
        /// </summary>
        /// <param name="model">Модел на диплома</param>
        /// <returns></returns>
        int DiplomaSaveData(DiplomaViewModel model);

        /// <summary>
        /// Списък за показване на всички обучения за съответния човек
        /// </summary>
        /// <param name="Person">Идентификатор на човек</param>
        /// <returns></returns>
        public List<SelectListItem> GetTrainingsDropDownByPersonId(int personId, bool addDefaultElement);
        //public List<SelectListItem> GetTrainingsDropDownByEmployeeId(int employeeId, bool addDefaultElement);

        /// <summary>
        /// Списък за показване на всички обучения за съответния служител
        /// </summary>
        /// <param name="employeeId">Идентификатор на служител</param>
        /// <returns></returns>
        IQueryable<TrainingListViewModel> GetTrainingsByPersonId(int personId);

        /// <summary>
        /// Списък за показване на всички сертификати за съответния човек
        /// </summary>
        /// <param name="personId">Идентификатор на човек</param>
        /// <returns></returns>
        IQueryable<CertificateListViewModel> GetCertificatesByPersonId(int personId);

        // /// <summary>
        ///// Списък за показване на всички сертификати за съответния служител
        ///// </summary>
        ///// <param name="employeeId">Идентификатор на служител</param>
        ///// <returns></returns>
        //IQueryable<CertificateListViewModel> GetCertificatesByEmployeeId(int employeeId);

        /// <summary>
        /// Показване на обучение на служител
        /// </summary>
        /// <param name="id">Идентификатор на обучение</param>
        /// <returns></returns>
        TrainingViewModel GetTrainingById(int id);

        /// <summary>
        /// Изтриване на обучение на служител
        /// </summary>
        /// <param name="id">Идентификатор на обучение</param>
        /// <returns></returns>
        bool DeleteTrainingById(int id);

        /// <summary>
        /// Запазване на промени по/Добавяне на обучение на служител
        /// </summary>
        /// <param name="model">Модел на обучение</param>
        /// <returns></returns>
        int TrainingSaveData(TrainingViewModel model);

        /// <summary>
        /// Показване на сертификат на служител
        /// </summary>
        /// <param name="id">Идентификатор на сертификат</param>
        /// <returns></returns>
        CertificateViewModel GetCertificateById(int id);

        /// <summary>
        /// Изтриване на сертификат на служител
        /// </summary>
        /// <param name="id">Идентификатор на сертификат</param>
        /// <returns></returns>
        bool DeleteCertificateById(int id);

        /// <summary>
        /// Изважда диплома вч модел по ID
        /// </summary>
        /// <param name="diplomaId">Идентификатор на диплома</param>
        /// <returns></returns>
        DiplomaViewModel GetDiplomaViewModelById(int diplomaId);

        /// <summary>
        /// Запазване на промени по/Добавяне на сертификат на служител
        /// </summary>
        /// <param name="model">Модел на сертификат</param>
        /// <returns></returns>
        int CertificateSaveData(CertificateViewModel model);

        /// <summary>
        /// Списък с обучения според избраният обучителен център
        /// </summary>
        /// <param name="trainingCenterId">Идентификатор на обучителен център</param>
        /// <returns></returns>
        ///public List<SelectListItem> GetTrainingsDropDownByTrainingCenterId(int trainingCenterId);
        
        ///// <summary>
        ///// Списък с на имената на сертификатите според типа на сертификата
        ///// </summary>
        ///// <param name="educationInstitutionId">Идентификатор на типа на сертификата</param>
        ///// <returns></returns>
        //public List<SelectListItem> GetCertificateNameByCertificateTypeId(int certificateTypeId);

        /// <summary>
        /// Стойност на Сертификата по идентификатор
        /// </summary>
        /// <param name="id">Идентификатор на сертификата</param>
        /// <returns></returns>
        HierarchicalNomenclatureDisplayItem GetCertificateNameIssuerById(int id);

        /// <summary>
        /// Информация за autocomplete контрола за Сертификата
        /// </summary>
        /// <param name="query">Част от име на сертификата</param>
        /// <returns></returns>
        HierarchicalNomenclatureDisplayModel SearcCertificateNameIssuer(string query);

      /// <summary>
      /// Запазване Обучителен център към номенклатурата
      /// </summary>
      /// <param name="code">Код на обучителен център</param>
      /// <param name="trainingCenterName">Име на обучителен център</param>
      /// <param name="description">Описание на обучителен център</param>
      /// <returns></returns>
        bool TrainingCenterNomenclatureSaveData(string code, string trainingCenterName, string description, bool isActive);

        /// <summary>
        /// Запазване Име на обучение към номенклатурата
        /// </summary>
        /// <param name="code">Код на Име на обучение</param>
        /// <param name="trainingName">Име на обучение</param>
        /// <param name="description">Описание на обучение</param>
        /// <returns></returns>
        bool TrainingNameNomenclatureSaveData(string code, string trainingName, string description, bool isActive);

        /// <summary>
        /// Запазване Висше учебно заведение към номенклатурата
        /// </summary>
        /// <param name="code">Код на Висше учебно заведение</param>
        /// <param name="educationInstitutionName">Име на висше учебно заведение</param>
        /// <param name="description">Описание на висше учебно заведение</param>
        /// <returns></returns>
        bool EducationInstitutionNomenclatureSaveData(string code, string educationInstitutionName, string description, bool isActive);
    }
}
