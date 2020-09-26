using IOWebFramework.Core.Models;
using IOWebFramework.Core.Models.Classifier;
using IOWebFramework.Core.Models.Nomenclatures;
using System.Collections.Generic;
using System.Linq;

namespace IOWebFramework.Core.Contracts
{
    public interface IClassifierService
    {
        /// <summary>
        /// Връща списък с области за визуализация
        /// </summary>
        /// <param name="parentId">Идентификатор на родител за йерархична номенклатура</param>
        /// <returns></returns>
        IQueryable<ClassifierListViewModel> GetClassifiers(int parentId);

        /// <summary>
        /// Стойност на Специалност по идентификатор
        /// </summary>
        /// <param name="id">Идентификатор на специалност</param>
        /// <returns></returns>
        HierarchicalNomenclatureDisplayItem GetSpecialtyById(int id);

        /// <summary>
        /// Информация за autocomplete контрола за Специалност
        /// </summary>
        /// <param name="query">Част от име на специалност</param>
        /// <returns></returns>
        HierarchicalNomenclatureDisplayModel SearchActivity(string query);
        /// <summary>
        /// Връща ClassifierViewModel от подадено id
        /// </summary>
        /// <param name="classifierId"></param>
        /// <returns></returns>
        ClassifierViewModel GetClassifierViewModelById(int classifierId);
        /// <summary>
        /// Запис в базата данни
        /// </summary>
        /// <param name="classifierViewModel"></param>
        /// <returns></returns>
        bool SaveData(ClassifierViewModel classifierViewModel);
        /// <summary>
        /// Проверява дали даден обект има деца
        /// </summary>
        /// <returns></returns>
        bool HasChildren(int classifierId);
        /// <summary>
        /// Генерира пътечка за йерархичната номенклатура
        /// </summary>
        /// <param name="parentId">Идентификатор на родител</param>
        /// <returns></returns>
        List<BreadcrumbInfoModel> GetBreadcrumbByParentId(int parentId);
    }
}
