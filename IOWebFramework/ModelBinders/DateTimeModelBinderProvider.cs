﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;

namespace IOWebFramework.ModelBinders
{
    /// <summary>
    /// Author: Stamo Petkov
    /// Created: 07.01.2017
    /// Description: Корекция на формата на датата
    /// </summary>
    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        private readonly string _customFormat;


        public DateTimeModelBinderProvider(string dateFormat)
        {
            _customFormat = dateFormat;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(DateTime) || context.Metadata.ModelType == typeof(DateTime?))
            {
                return new DateTimeModelBinder(_customFormat);
            }

            return null;
        }
    }
}
