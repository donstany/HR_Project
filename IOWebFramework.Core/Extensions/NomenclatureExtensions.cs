using IOWebFramework.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace IOWebFramework.Core.Extensions
{
    public static class NomenclatureExtensions
    {
        /// <summary>
        /// Creates SelectList from IQueryable<ICommonNomenclature>
        /// </summary>
        /// <param name="model">Common Nomenclature list of entities</param>
        /// <param name="addDefaultElement">Add 'Choose' to list</param>
        /// <paramref name="addAllElement">Add 'All' to list</param>
        /// <paramref name="orderByNumber">Order by order number</param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectList(this IQueryable<ICommonNomenclature> model, bool addDefaultElement = false, bool addAllElement = false, bool orderByNumber = true)
        {
            DateTime today = DateTime.Today;

            Expression<Func<ICommonNomenclature, object>> order = x => x.OrderNumber;
            if (!orderByNumber)
            {
                order = x => x.Label;
            }

            var result = model
                .Where(x => x.IsActive)
                .Where(x => x.DateStart <= today)
                .Where(x => (x.DateEnd ?? today) >= today)
                .OrderBy(order)
                .Select(x => new SelectListItem()
                {
                    Text = x.Label,
                    Value = x.Id.ToString()
                }).ToList() ?? new List<SelectListItem>();

            if (addDefaultElement)
            {
                result = result
                    .Prepend(new SelectListItem() { Text = "Избери", Value = null })
                    .ToList();
            }

            if (addAllElement)
            {
                result = result
                    .Prepend(new SelectListItem() { Text = "Всички", Value = "-2" })
                    .ToList();
            }

            return result;
        }
    }
}
