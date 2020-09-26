using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IOWebFramework.Core.Extensions
{
    /// <summary>
    /// Разширения на методите на LINQ
    /// </summary>
    public static class LinqExtension
    {

        /// <summary>
        /// Маркира определен елемент, като избран
        /// </summary>
        /// <typeparam name="TSource">Тип на колекцията</typeparam>
        /// <param name="source">Изходна колекция</param>
        /// <param name="selected">Елемент, който да бъде избран</param>
        /// <returns></returns>
        public static SelectList SetSelected<TSource>(
        this IEnumerable<TSource> source, object selected)
        {
            if (source == null)
            {
                return new SelectList(new List<SelectListItem>());
            }
            return new SelectList(source, "Value", "Text", selected);
        }

        /// <summary>
        /// Добавя "Изберете" в SelectList, 
        /// стойността му по подразбиране е -1
        /// </summary>
        /// <param name="source">Изходна колекция</param>
        /// <param name="allText">Текст, по подразбиране "Изберете"</param>
        /// <param name="allValue">Стойност, по подразбиране -1</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> AddAllItem(
         this IEnumerable<SelectListItem> source, string allText = "Изберете", string allValue = "-1")
        {
            var allList = new List<SelectListItem>();
            allList.Add(new SelectListItem() { Text = allText, Value = allValue });
            return allList.Union(source);
        }

        /// <summary>
        /// Добавяне на елемент
        /// </summary>
        /// <param name="source">Изходна колекция</param>
        /// <param name="Text">Име на елемента</param>
        /// <param name="Value">Стойност на елемента</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> AppendItem(
         this IEnumerable<SelectListItem> source, string Text, string Value)
        {
            var newList = new List<SelectListItem>();
            newList.Add(new SelectListItem() { Text = Text, Value = Value });
            return source.Union(newList);
        }

        /// <summary>
        /// Проверява, дали колекцията е празна
        /// </summary>
        /// <typeparam name="T">Тип на колекцията</typeparam>
        /// <param name="source">Изходна колекция</param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
                return true;

            if (source.Count() == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Търси съвпадение в текст в базата данни, case sensitive 
        /// </summary>
        /// <typeparam name="T">Тип на таблицата от базата</typeparam>
        /// <param name="source">Колекция от данни</param>
        /// <param name="property">Колона с която сравняваме</param>
        /// <param name="query">Търсен текст</param>
        /// <returns></returns>
        public static IQueryable<T> Like<T>(this IQueryable<T> source, Expression<Func<T, string>> property, string query)
        {
            return LikeInternal(source, property, query, "Like");
        }

        /// <summary>
        /// Търси съвпадение в текст в базата данни, case insensitive 
        /// </summary>
        /// <typeparam name="T">Тип на таблицата от базата</typeparam>
        /// <param name="source">Колекция от данни</param>
        /// <param name="property">Колона с която сравняваме</param>
        /// <param name="query">Търсен текст</param>
        /// <returns></returns>
        public static IQueryable<T> LikeCI<T>(this IQueryable<T> source, Expression<Func<T, string>> property, string query)
        {
            return LikeInternal(source, property, query, "ILike");
        }

        private static IQueryable<T> LikeInternal<T>(IQueryable<T> source, Expression<Func<T, string>> property, string query, string function)
        {
            var selector = property.Body as MemberExpression;

            if (String.IsNullOrEmpty(query) || selector == null || selector.Type != typeof(string))
            {
                return source;
            }

            var parameter = Expression.Parameter(source.ElementType, "x");
            Expression matchExpression = Expression.PropertyOrField(parameter, selector.Member.Name); ;
            var pattern = Expression.Constant($"%{query}%");
            Type dbFunctionType;

            if (function == "Like")
            {
                dbFunctionType = typeof(DbFunctionsExtensions);
            }
            else if (function == "ILike")
            {
                dbFunctionType = typeof(NpgsqlDbFunctionsExtensions);
            }
            else
            {
                return source;
            }

            var filter = Expression.Call(
                dbFunctionType, function, Type.EmptyTypes,
                Expression.Constant(EF.Functions), matchExpression, pattern);

            MethodCallExpression whereCallExpression = Expression.Call(
                typeof(Queryable),
                "Where",
                new Type[] { source.ElementType },
                source.Expression,
                Expression.Lambda<Func<T, bool>>(filter, new ParameterExpression[] { parameter }));

            return source.Provider.CreateQuery<T>(whereCallExpression);
        }
    }
}
