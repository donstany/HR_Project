using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using IOWebFramework.Components;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Models;
using IOWebFramework.Core.Models.Classifier;
using IOWebFramework.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace IOWebFramework.Controllers
{
    [Authorize(Roles = "HR")]
    public class ClassifierController : BaseController
    {
        private readonly IClassifierService _classifierService;

        public ClassifierController(IClassifierService classifierService)
        {
            _classifierService = classifierService;
        }

        [HttpGet]
        [MenuItem("classifier")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(int parentId = 0)
        {
            var model = new ClassifierViewModel() { ParentId = parentId };
            GetBreadcrumb(model);
            return View("Edit", model);
        }
        public IActionResult Edit(int classifierId)
        {
            ClassifierViewModel classifierViewModel = _classifierService.GetClassifierViewModelById(classifierId);
            ViewBag.HideTable = _classifierService.HasChildren(classifierId);
            GetBreadcrumb(classifierViewModel);

            return View(classifierViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ClassifierViewModel classifierViewModel)
        {
            ViewBag.HideTable = _classifierService.HasChildren(classifierViewModel.Id);
            if (!ModelState.IsValid)
            {
                return View(classifierViewModel);
            }
            GetBreadcrumb(classifierViewModel);

            var result = _classifierService.SaveData(classifierViewModel);
            this.ShowNotificationMessageOnUI(result);

            return View(classifierViewModel);
        }

        [HttpPost]
        public IActionResult ClassifierListData(IDataTablesRequest request, int parentId = 0)
        {
            IQueryable<ClassifierListViewModel> data = _classifierService.GetClassifiers(parentId);

            var filteredData = data;

            if (request.Search.Value != null)
            {
                filteredData = data.Where(a =>
                a.Name.ToLower().Contains(request.Search.Value.ToLower()));
            }
            var response = request.GetResponse(data, filteredData);
            return new DataTablesJsonResult(response, true) ;
        }

        private void GetBreadcrumb(ClassifierViewModel model)
        {
            List<BreadcrumbInfoModel> result = new List<BreadcrumbInfoModel>();
            if (model.Id != model.ParentId)
            {
                result = _classifierService.GetBreadcrumbByParentId(model.ParentId);
            }

            int level = result.Count > 0 ? result.Max(r => r.Level) + 1 : 1000;

            result.Add(new BreadcrumbInfoModel()
            {
                Level = level,
                Id = model.Id,
                Title = model.Name
            });

            result.Add(new BreadcrumbInfoModel()
            {
                Level = result.Min(r => r.Level) - 1,
                Id = 0,
                Title = "Области"
            });

            model.BreadcrumbInfo = result
                .OrderBy(r => r.Level)
                .ToList();
        }
    }
}