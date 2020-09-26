using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using IOWebFramework.Components;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Models.Employees;
using IOWebFramework.Extensions;
using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using IOWebFramework.Infrastructure.Helper_Classes.EGN_Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace IOWebFramework.Controllers
{
    [Authorize(Roles = "HR")]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly INomenclatureService _nomenclatureService;
        public EmployeeController(IEmployeeService employeeService,
                               INomenclatureService nomenclatureService)
        {
            this._employeeService = employeeService;
            this._nomenclatureService = nomenclatureService;
        }

        [HttpGet]
        [MenuItem("employee")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            var model = new EmployeeViewModel();
            GetViewBags(model);

            return View("Edit", model);
        }
        public IActionResult Edit(int employeeId, int personId)
        {
            EmployeeViewModel model = _employeeService.GetEmployeeViewModelById(employeeId, personId);
            model.IsEditMode = true;
            GetViewBags(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel employeeViewModel)
        {
            BasicEGNValidation egnValidation = new BasicEGNValidation(employeeViewModel.UserDetailDTO.PID.Trim());
            if (!egnValidation.Validate())
            {
                ModelState.AddModelError("UserDetailDTO.PID", "Невалидно ЕГН!");
            }
            GetViewBags(employeeViewModel);
            //Backend validation is mandatory
            if (!ModelState.IsValid)
            {
                return View(employeeViewModel);
            }

            var result = _employeeService.SaveData(employeeViewModel);
            this.ShowNotificationMessageOnUI(result);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult EmployeeListData(IDataTablesRequest request, bool onlyActive = true)
        {
            IQueryable<EmployeeListViewModel> data = _employeeService.GetEmployees(onlyActive);
            var response = request.GetResponse(data);
            return new DataTablesJsonResult(response, true);
        }

        public JsonResult GetDepartmentsByBranchId(int branchId)
        {
            var model = _employeeService.GetDepartmentsDropDownByBranchId(branchId);

            return new JsonResult(model);
        }

        public JsonResult GetNameDropDownByCheckBoxValue(bool isInactiveEmployee)
        {
            List<SelectListItem> model;
            if (isInactiveEmployee)
            {
                model = _employeeService.GetInactiveEmployeesDropDown();
            }
            else
            {
                model = _employeeService.GetPersonDropDown();
            }
            return new JsonResult(model);
        }

        public JsonResult CheckTdIsUnique(string tdNumber)
        {
            if (string.IsNullOrEmpty(tdNumber))
            {
                return new JsonResult(false);
            }

            var isUnique = _employeeService.CheckTdIsUnique(tdNumber);

            return new JsonResult(isUnique);
        }

        public JsonResult SyncWithAD()
        {

            var isSyncSuccessfully = _employeeService.ImportDataFromAD();

            return new JsonResult(isSyncSuccessfully);
        }
        public JsonResult SyncWithADByEmail(string emailInAd)
        {
            var employeeDTO = _employeeService.SyncDataFromADByEmail(emailInAd);

            return new JsonResult(employeeDTO);
        }

        [HttpPost]
        public IActionResult GetDataFromDbPersonDetails(int personId)
        {
            if (personId < 1)
            {
                return new JsonResult(new EmployeeViewModel());
            }
            var resultDto = _employeeService.GetPersonDetails(personId);

            return new JsonResult(resultDto);
        }
        private void GetViewBags(EmployeeViewModel model = null)
        {
            ViewBag.DepartmentId_ddl = _nomenclatureService.GetDropDownList<Department>();
            ViewBag.BranchId_ddl = _nomenclatureService.GetDropDownList<Branch>();
            ViewBag.PositionId_ddl = _nomenclatureService.GetDropDownList<Position>();
            ViewBag.PersonId_ddl = _employeeService.GetPersonDropDown();

            if (model != null)
            {
                ViewBag.ExpirienceInIOYearsId_ddl = _employeeService.GetDateDropDown(0, 60);
                ViewBag.ExpirienceInIOMonthsId_ddl = _employeeService.GetDateDropDown(0, 12);
                ViewBag.ExpirienceInIODaysId_ddl = _employeeService.GetDateDropDown(0, 30);

                ViewBag.ExpirienceOutIOYearsId_ddl = _employeeService.GetDateDropDown(0, 60);
                ViewBag.ExpirienceOutIOMonthsId_ddl = _employeeService.GetDateDropDown(0, 12);
                ViewBag.ExpirienceOutIODaysId_ddl = _employeeService.GetDateDropDown(0, 30);
            }
        }
    }
}