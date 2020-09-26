using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IOWebFramework.Extensions
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Създава SelectList от колекция
        /// </summary>
        /// <typeparam name="TSource">Тип на колекцията</typeparam>
        /// <typeparam name="TValue">Тип на стойността на елементите</typeparam>
        /// <typeparam name="TText">Тип на имената на елементите</typeparam>
        /// <param name="source">Изходна колекция</param>
        /// <param name="dataValueField">Стойност на елемента</param>
        /// <param name="dataTextField">Име на елемента</param>
        /// <returns></returns>
        public static SelectList ToSelectList<TSource, TValue, TText>(
            this IHtmlHelper<TSource> htmlHelper,
            IEnumerable<TSource> source,
            Expression<Func<TSource, TValue>> dataValueField,
            Expression<Func<TSource, TText>> dataTextField)
        {
            return ToSelectList(htmlHelper, source, dataValueField, dataTextField, null);
        }

        /// <summary>
        /// Създава SelectList от колекция
        /// </summary>
        /// <typeparam name="TSource">Тип на колекцията</typeparam>
        /// <typeparam name="TValue">Тип на стойността на елементите</typeparam>
        /// <typeparam name="TText">Тип на имената на елементите</typeparam>
        /// <param name="source">Изходна колекция</param>
        /// <param name="dataValueField">Стойност на елемента</param>
        /// <param name="dataTextField">Име на елемента</param>
        /// <param name="selected">Избран елемент</param>
        /// <returns></returns>
        public static SelectList ToSelectList<TSource, TValue, TText>(
            this IHtmlHelper<TSource> htmlHelper,
            IEnumerable<TSource> source,
            Expression<Func<TSource, TValue>> dataValueField,
            Expression<Func<TSource, TText>> dataTextField,
            object selected)
        {
            if (source == null)
            {
                return new SelectList(new List<SelectListItem>());
            }

            var expresionProvider = htmlHelper.ViewContext.HttpContext.RequestServices
                .GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;

            string dataName = expresionProvider.GetExpressionText(dataValueField);
            string textName = expresionProvider.GetExpressionText(dataTextField);
            
            return new SelectList(source, dataName, textName, selected);
        }
    }
}
