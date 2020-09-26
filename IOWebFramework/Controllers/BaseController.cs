using IO.LogOperation.Models;
using IO.LogOperation.Service;
using IOWebFramework.Components;
using IOWebFramework.Infrastructure.Contracts;
using IOWebFramework.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace IOWebFramework.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private IUserContext _userContext;

        protected IUserContext userContext
        {
            get
            {
                if (_userContext == null)
                {
                    _userContext = (IUserContext)HttpContext
                         .RequestServices
                         .GetService(typeof(IUserContext));
                }

                return _userContext;
            }
        }

        //public string LanguageCode = "bg";


        protected void SaveLogOperation(OperationTypes operation, object objectKey, object masterKey = null)
        {
            ILogOperationService<ApplicationDbContext> logOperation =
                (ILogOperationService<ApplicationDbContext>)HttpContext
                .RequestServices
                .GetService(typeof(ILogOperationService<ApplicationDbContext>));

            var html = string.Empty;
            if (Request.Form["hfContainer"].FirstOrDefault() != null)
            {
                html = Request.Form["hfContainer"].FirstOrDefault();
                logOperation.Save(this.ControllerName?.ToLower(), logOperation.MakeActionName(this.ActionName, ActionTransformType.AddToEdit)?.ToLower(), objectKey.ToString(), operation, html, userContext.LogName, userContext.UserId, masterKey?.ToString());
            }
        }

        public string ActionName { get; set; }
        public string ControllerName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            /*
             *      Управление на активния елемент на менюто
             *      ViewBag.MenuItemValue съдържа ключовата дума, отговорна за отварянето на менюто
             *      Ако не намери атрибут на action-а MenuItem("{keyword}"), се използва името на action-а
             *      Ако action-а е от вида List_Edit се подава list (отрязва до последния символ долна подчертавка)
             */
            ControllerActionDescriptor controllerActionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;

            if (controllerActionDescriptor != null)
            {
                ActionName = controllerActionDescriptor.ActionName;
                ControllerName = controllerActionDescriptor.ControllerName;
                object currentMenuItem = null;
                var menuAttrib = controllerActionDescriptor
                                    .MethodInfo
                                    .CustomAttributes
                                    .FirstOrDefault(a => a.AttributeType == typeof(MenuItemAttribute));
                if (menuAttrib != null)
                {
                    currentMenuItem = menuAttrib.ConstructorArguments[0].Value;
                }
                if (currentMenuItem == null)
                {
                    var actionName = controllerActionDescriptor.ActionName;
                    if (actionName.Contains('_'))
                    {
                        currentMenuItem = actionName.Substring(0, actionName.LastIndexOf('_')).ToLower();
                    }
                    else
                    {
                        currentMenuItem = actionName.ToLower();
                    }
                }
                ViewBag.MenuItemValue = currentMenuItem;
            }
            // ---------Управление на активния елемент на менюто, край
        }

    }
}