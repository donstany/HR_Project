using IOWebFramework.Core.Models.Nomenclatures;
using IOWebFramework.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace IOWebFramework.Core.Contracts
{
    /// <summary>
    /// Общи номенклатури
    /// </summary>
    public interface INomenclatureService
    {
        /// <summary>
        /// Списък за показване в таблица, всички елементи
        /// </summary>
        /// <typeparam name="T">Тип на номенклатурата</typeparam>
        /// <returns></returns>
        IQueryable<CommonNomenclatureListItem> GetList<T>() where T : class, ICommonNomenclature;

        /// <summary>
        /// ЛСписък за показване в таблица, само активни елементи
        /// </summary>
        /// <typeparam name="T">Тип на номенклатурата</typeparam>
        /// <returns></returns>
        IQueryable<CommonNomenclatureListItem> GetActiveList<T>() where T : class, ICommonNomenclature;

        /// <summary>
        /// Конкретен елемент от номенклатура
        /// </summary>
        /// <typeparam name="T">Тип на номенклатурата</typeparam>
        /// <param name="id">Идентификатор на запис</param>
        /// <returns></returns>
        T GetItem<T>(int id) where T : class, ICommonNomenclature;

        /// <summary>
        /// Запис на елемент от номенклатура
        /// </summary>
        /// <typeparam name="T">Тип на номенклатурата</typeparam>
        /// <param name="entity">Елемент за запис</param>
        /// <returns></returns>
        bool SaveItem<T>(T entity) where T : class, ICommonNomenclature;

        /// <summary>
        /// Промяна на подредбата с бутони
        /// </summary>
        /// <typeparam name="T">Тип на номенклатурата</typeparam>
        /// <param name="model">Посока и идентификатор на запис</param>
        /// <returns></returns>
        bool ChangeOrder<T>(ChangeOrderModel model) where T : class, ICommonNomenclature;

        /// <summary>
        /// Генерира списък от елементи на номенклатура за комбо
        /// </summary>
        /// <typeparam name="T">Тип на номенклатурата</typeparam>
        /// <param name="addDefaultElement">Дали да добави елемент "Изберете"
        /// по подразбиране е изтина</param>
        /// <returns></returns>
        List<SelectListItem> GetDropDownList<T>(bool addDefaultElement = true, bool addAllElement = false, bool orderByNumber = true) where T : class, ICommonNomenclature;

        /// <summary>
        /// Информация за autocomplete контрола за Екатте
        /// </summary>
        /// <param name="query">Част от име на обект</param>
        /// <returns></returns>
        HierarchicalNomenclatureDisplayModel GetEkatte(string query);

        /// <summary>
        /// Стойност на Екатте по идентификатор
        /// </summary>
        /// <param name="id">Код по Екатте</param>
        /// <returns></returns>
        HierarchicalNomenclatureDisplayItem GetEkatteById(string id);

        /// <summary>
        /// Информация за autocomplete контрола за Street
        /// </summary>
        /// <param name="query">Част от име на обект</param>
        /// <returns></returns>
        IEnumerable<LabelValueVM> GetStreet(string ekatte, string query);

        /// <summary>
        /// Информация за autocomplete контрола за Street
        /// </summary>
        /// <param name="code">Код на улица</param>
        /// <returns></returns>
        LabelValueVM GetStreetByCode(string ekatte, string code);
    }
}
