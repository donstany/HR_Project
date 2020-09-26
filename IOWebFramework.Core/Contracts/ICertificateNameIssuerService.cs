using IOWebFramework.Core.Models;
using IOWebFramework.Core.Models.CertificateNameIssuer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOWebFramework.Core.Contracts
{
    public interface ICertificateNameIssuerService
    {
        /// <summary>
        /// Връща списък с издатели на сертификати за визуализация
        /// </summary>
        /// <param name="parentId">Идентификатор на родител за йерархична номенклатура</param>
        /// <returns></returns>
        IQueryable<CertificateNameIssuerListViewModel> GetCertificateNameIssuers(int parentId);
        /// <summary>
        /// Връща CertificateNameIssuerViewModel от подадено id
        /// </summary>
        /// <param name="certificateNameIssuerId"></param>
        /// <returns></returns>
        CertificateNameIssuerViewModel GetCertificateNameIssuerViewModelById(int certificateNameIssuerId);
        /// <summary>
        /// Проверява дали даден обект има деца
        /// </summary>
        /// <returns></returns>
        bool HasChildren(int certificateNameIssuerId);
        /// <summary>
        /// Запис в базата данни
        /// </summary>
        /// <param name="certificateNameIssuerViewModel"></param>
        /// <returns></returns>
        bool SaveData(CertificateNameIssuerViewModel certificateNameIssuerViewModel);
        /// <summary>
        /// Изтриване на сертификат по издател в номенклатурата
        /// </summary>
        /// <param name="id">Идентификатор на сертификат по издател</param>
        /// <returns></returns>
        bool DeleteCertificateNameIssuerById(int id);
        /// <summary>
        /// Проверка дали има вече направен сертификат с име по издател
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckIfItHasRelation(int id);
        /// <summary>
        /// Генерира пътечка за йерархичната номенклатура
        /// </summary>
        /// <param name="parentId">Идентификатор на родител</param>
        /// <returns></returns>
        List<BreadcrumbInfoModel> GetBreadcrumbByParentId(int parentId);
    }
}
