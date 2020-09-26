using System.Collections.Generic;
using System.Linq;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using IOWebFramework.Components;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Models;
using IOWebFramework.Core.Models.CertificateNameIssuer;
using IOWebFramework.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IOWebFramework.Controllers
{
    [Authorize(Roles = "HR")]
    public class CertificateNameIssuerController : BaseController
    {
        private readonly ICertificateNameIssuerService _certificateNameIssuerService;

        public CertificateNameIssuerController(ICertificateNameIssuerService certificateNameIssuerService)
        {
            _certificateNameIssuerService = certificateNameIssuerService;
        }
        [HttpGet]
        [MenuItem("certificateNameIssuer")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(int parentId = 0)
        {
            var model = new CertificateNameIssuerViewModel() { ParentId = parentId };
            return View("Edit", model);
        }
        public IActionResult Edit(int certificateNameIssuerId)
        {
            CertificateNameIssuerViewModel certificateNameIssuerViewModel = _certificateNameIssuerService.GetCertificateNameIssuerViewModelById(certificateNameIssuerId);
            ViewBag.HideTable = _certificateNameIssuerService.HasChildren(certificateNameIssuerId);
            GetBreadcrumb(certificateNameIssuerViewModel);
            return View(certificateNameIssuerViewModel);
        }

        [HttpPost]
        public IActionResult Edit(CertificateNameIssuerViewModel certificateNameIssuerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(certificateNameIssuerViewModel);
            }

            ViewBag.HideTable = _certificateNameIssuerService.HasChildren(certificateNameIssuerViewModel.Id);
            GetBreadcrumb(certificateNameIssuerViewModel);

            var result = _certificateNameIssuerService.SaveData(certificateNameIssuerViewModel);
            this.ShowNotificationMessageOnUI(result);

            return View(certificateNameIssuerViewModel);
        }

        [HttpPost]
        public IActionResult CertificateNameIssuerListData(IDataTablesRequest request, int parentId = 0)
        {
            IQueryable<CertificateNameIssuerListViewModel> data = _certificateNameIssuerService.GetCertificateNameIssuers(parentId);

            var filteredData = data;

            if (request.Search.Value != null)
            {
                filteredData = data.Where(a =>
                a.Name.ToLower().Contains(request.Search.Value.ToLower()));
            }
            var response = request.GetResponse(data, filteredData);
            return new DataTablesJsonResult(response, true);
        }

        [HttpPost]
        public IActionResult DeleteCertificateNameIssuer(int certificateNameIssuerId)
        {
            var result = false;
            if (!_certificateNameIssuerService.HasChildren(certificateNameIssuerId) && !_certificateNameIssuerService.CheckIfItHasRelation(certificateNameIssuerId))
            {
                result = _certificateNameIssuerService.DeleteCertificateNameIssuerById(certificateNameIssuerId);
            }

            return Json(result);
        }

        private void GetBreadcrumb(CertificateNameIssuerViewModel model)
        {
            List<BreadcrumbInfoModel> result = new List<BreadcrumbInfoModel>();
            if (model.Id != model.ParentId)
            {
                result = _certificateNameIssuerService.GetBreadcrumbByParentId(model.ParentId);
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
                Title = "Издатели"
            });

            model.BreadcrumbInfo = result
                .OrderBy(r => r.Level)
                .ToList();
        }
    }
}
