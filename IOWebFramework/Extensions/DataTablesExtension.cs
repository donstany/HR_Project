using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IOWebFramework.Extensions
{
    public static class DataTablesExtension
    {
        /// <summary>
        /// Генерира отговор на AJAX заявка на DataTables
        /// </summary>
        /// <typeparam name="T">Тип на изходните данни</typeparam>
        /// <param name="request">Заявка на DataTables</param>
        /// <param name="data">Пълен сет от данни</param>
        /// <param name="filteredData">Филтриран сет от данни</param>
        /// <returns></returns>
        public static DataTablesResponse GetResponse<T>(this IDataTablesRequest request, IQueryable<T> data, IQueryable<T> filteredData = null, Dictionary<string, object> additionalParameters = null)
        {
            if (filteredData == null)
            {
                filteredData = request.GetFilteredData(data);
            }

            var orderColums = request.Columns.Where(x => x.Sort != null);
            IQueryable<T> dataPage = null;

            if (request.Length < 0)
            {
                dataPage = filteredData;
            }
            else
            {
                dataPage = filteredData.OrderBy(orderColums).Skip(request.Start).Take(request.Length);
            }

            return DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);
        }

        /// <summary>
        /// Използва текста в полето за търсене за филтрация на данните по колоните, 
        /// маркирани като колони за търсене
        /// </summary>
        /// <typeparam name="T">Тип на изходните данни</typeparam>
        /// <param name="request">Заявка на DataTables</param>
        /// <param name="data">Пълен сет от данни</param>
        /// <returns></returns>
        public static IQueryable<T> GetFilteredData<T>(this IDataTablesRequest request, IQueryable<T> data)
        {
            var filteredData = data;

            if (request.Search.Value != null)
            {
                var searchColumns = request.Columns.Where(c => c.IsSearchable);
                filteredData = data.SearchFor(searchColumns, request.Search.Value);
            }

            return filteredData;
        }

        /// <summary>
        /// Сортиране в DataTables
        /// </summary>
        /// <typeparam name="T">Тип на данните</typeparam>
        /// <param name="source">Дърво, което трябва да бъде подредено</param>
        /// <param name="sortModels">Модел с данни за начина на сортиране</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IEnumerable<DataTables.AspNet.Core.IColumn> sortModels)
        {
            var expression = source.Expression;
            int count = 0;

            foreach (var item in sortModels)
            {
                var parameter = Expression.Parameter(source.ElementType, "x");
                var selector = Expression.PropertyOrField(parameter, item.Name);
                var method = item.Sort.Direction == DataTables.AspNet.Core.SortDirection.Descending ?
                    (count == 0 ? "OrderByDescending" : "ThenByDescending") :
                    (count == 0 ? "OrderBy" : "ThenBy");
                expression = Expression.Call(typeof(Queryable), method,
                    new Type[] { source.ElementType, selector.Type },
                    expression, Expression.Quote(Expression.Lambda(selector, parameter)));
                count++;
            }

            return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
        }

        /// <summary>
        /// Търсене в DataTables
        /// </summary>
        /// <typeparam name="T">Тип на изходните данни</typeparam>
        /// <param name="source">Пълен сет данни</param>
        /// <param name="searchModel">Модел с колони, по които се търси</param>
        /// <param name="query">Стойност на полето за търсене</param>
        /// <returns></returns>
        public static IQueryable<T> SearchFor<T>(this IQueryable<T> source, IEnumerable<DataTables.AspNet.Core.IColumn> searchModel, string query)
        {
            if (searchModel?.Count() == 0 || String.IsNullOrEmpty(query))
            {
                return source;
            }

            var parameter = Expression.Parameter(source.ElementType, "x");
            Expression predicate = Expression.Constant(false, typeof(Boolean));

            foreach (var item in searchModel)
            {
                var selector = Expression.PropertyOrField(parameter, item.Name);
                Expression matchExpression = selector;

                if (selector.Type != typeof(string))
                {
                    break;
                    //matchExpression = Expression.Convert(matchExpression, typeof(object));
                    //matchExpression = Expression.Convert(matchExpression, typeof(string));
                }

                var pattern = Expression.Constant($"%{query}%");
                Expression filter = Expression.Call(
                    typeof(NpgsqlDbFunctionsExtensions), "ILike", Type.EmptyTypes,
                    Expression.Constant(EF.Functions), matchExpression, pattern);

                predicate = Expression.OrElse(predicate, filter);
            }

            MethodCallExpression whereCallExpression = Expression.Call(
                typeof(Queryable),
                "Where",
                new Type[] { source.ElementType },
                source.Expression,
                Expression.Lambda<Func<T, bool>>(predicate, new ParameterExpression[] { parameter }));

            return source.Provider.CreateQuery<T>(whereCallExpression);
        }
    }
}
