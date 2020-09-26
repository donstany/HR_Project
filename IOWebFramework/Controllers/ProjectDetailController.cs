using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Extensions;
using IOWebFramework.Core.Models.ProjectDetail;
using IOWebFramework.Extensions;
using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using Microsoft.AspNetCore.Mvc;


namespace IOWebFramework.Controllers
{
    public class ProjectDetailController : BaseController
    {
        private readonly IProjectDetailService _projectDetailService;
        private readonly IEmployeeService _employeeService;
        private readonly INomenclatureService _nomenclatureService;

        public ProjectDetailController(IProjectDetailService projectDetailService, IEmployeeService employeeService, INomenclatureService nomenclatureService)
        {
            _projectDetailService = projectDetailService;
            _employeeService = employeeService;
            _nomenclatureService = nomenclatureService;
        }
        public IActionResult Index(int projectId, string tabToOpen = null)
        {
            if (!string.IsNullOrEmpty(tabToOpen))
            {
                ViewBag.TabToOpen = tabToOpen;
            }
            ViewBag.ProjectId = projectId;
            ViewBag.ProjectName = _projectDetailService.GetProjectNameByProjectId(projectId);

            var projectMainInfo = _projectDetailService.GetProjectMainInfo(projectId);
            return View(projectMainInfo);
        }
        [HttpPost]
        public IActionResult TeamListData(IDataTablesRequest request, int projectId, bool onlyActive = true)
        {
            IQueryable<TeamListViewModel> data = _projectDetailService.GetTeam(projectId, onlyActive);
            var response = request.GetResponse(data);
            return new DataTablesJsonResult(response, true);
        }

        [HttpPost]
        public IActionResult TechnologyListData(IDataTablesRequest request, int projectId, bool onlyActive = true)
        {
            IQueryable<TechnologyListViewModel> data = _projectDetailService.GetTechnology(projectId, onlyActive);
            var response = request.GetResponse(data);
            return new DataTablesJsonResult(response, true);
        }
        [HttpGet]
        public IActionResult AddTeam(int projectId)
        {
            TeamViewModel model = new TeamViewModel();
            model.ProjectId = projectId;
            model.IsAddingMode = true;
            SetViewBagsTeam(projectId);

            return View("EditTeam", model);
        }

        [HttpGet]
        public IActionResult EditTeam(int personId, int projectId, string tabToOpen = null)
        {
            if (!string.IsNullOrEmpty(tabToOpen))
            {
                ViewBag.TabToOpen = tabToOpen;
            }
            TeamViewModel model = _projectDetailService.GetTeamViewModelByIds(personId, projectId);
            SetViewBagsTeam(model.ProjectId);

            return View("EditTeam", model);
        }

        [HttpPost]
        public IActionResult EditTeam(TeamViewModel teamViewModel)
        {
            SetViewBagsTeam(teamViewModel.ProjectId);
            if (!ModelState.IsValid)
            {
                return View(teamViewModel);
            }

            var isSavedInDb = _projectDetailService.SaveData(teamViewModel);
            if (isSavedInDb)
            {
                this.ShowNotificationMessageOnUI(isSavedInDb);
                return RedirectToAction(nameof(Index), new { projectId = teamViewModel.ProjectId, tabToOpen = "#nav-team-tab" });
            }
            else
            {
                this.ShowNotificationMessageOnUI(isSavedInDb);
            }
            return View(nameof(EditTeam), teamViewModel);

        }

        [HttpGet]
        public IActionResult AddTechnology(int projectId)
        {
            TechnologyViewModel technologyViewModel = new TechnologyViewModel();
            technologyViewModel.ProjectId = projectId;

            SetViewBagsTechnology(projectId);

            return View("EditTechnology", technologyViewModel);
        }

        //[HttpGet]
        //public IActionResult EditTechnology(int projectTechnologyId, string tabToOpen = null)
        //{
        //    if (!string.IsNullOrEmpty(tabToOpen))
        //    {
        //        ViewBag.TabToOpen = tabToOpen;
        //    }
        //    TechnologyViewModel model = _projectDetailService.GetTechnologyViewModelById(projectTechnologyId);
        //    SetViewBagsTechnology(model.ProjectId);

        //    return View(model);
        //}

        [HttpPost]
        public IActionResult EditTechnology(TechnologyViewModel technologyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(technologyViewModel);
            }
            SetViewBagsTeam(technologyViewModel.ProjectId);

            var isSavedInDb = _projectDetailService.SaveDataTechnology(technologyViewModel);
            if (isSavedInDb)
            {
                this.ShowNotificationMessageOnUI(isSavedInDb);
                return RedirectToAction(nameof(Index), new { projectId = technologyViewModel.ProjectId, tabToOpen = "#nav-technology-tab" });
            }
            else
            {
                this.ShowNotificationMessageOnUI(isSavedInDb);
            }
            return View(technologyViewModel);

        }

        private void SetViewBagsTechnology(int projectId)
        {
            ViewBag.ProjectId = projectId;
            ViewBag.TechnologyId_ddl = _nomenclatureService.GetDropDownList<Technology>();
        }

        //public JsonResult CheckProjectDetailsAreUnique(string[] projectRoles, string personId, int projectId)
        //{
        //    if(projectRoles.IsEmpty() || personId == null)
        //    {
        //        return new JsonResult(true);
        //    }

        //    int[] roleIds = Array.ConvertAll(projectRoles, s => int.Parse(s));
        //    int pId = int.Parse(personId);
        //    bool result = _projectDetailService.CheckUniqueProjDetailsByIds(roleIds, pId, projectId);
        //    return new JsonResult(result);
        //}

        public JsonResult CheckProjectTechnologyAreUnique(string technologyId, int projectId)
        {
            if (technologyId == null)
            {
                return new JsonResult(false);
            }
            int techId = int.Parse(technologyId);
            bool result = _projectDetailService.CheckUniqueTechAndProjByIds(techId, projectId);
            return new JsonResult(result);
        }

        [HttpPost]
        public IActionResult DeleteTechnology(int technologyProjectId)
        {
            var result = _projectDetailService.DeleteTechnologyProjectById(technologyProjectId);

            return Json(result);
        }

        [HttpPost]
        public IActionResult AddTechnologyFromPopUp(string code, string technologyName, string description, bool isActive)
        {
            var result = _projectDetailService.TechnologyNomenclatureSaveData(code, technologyName, description, isActive);
            if (result)
            {
                this.ShowNotificationMessageOnUI(result);
            }
            else
            {
                this.ShowNotificationMessageOnUI(result);
            }

            return Json("ok");
        }

        [HttpPost]
        public IActionResult AddProjectRole(string code, string projectRoleName, string description, bool isActive)
        {
            var result = _projectDetailService.ProjectRoleNomenclatureSaveData(code, projectRoleName, description, isActive);
            if (result)
            {
                this.ShowNotificationMessageOnUI(result);
            }
            else
            {
                this.ShowNotificationMessageOnUI(result);
            }

            return Json("ok");
        }
        private void SetViewBagsTeam(int projectId)
        {
            ViewBag.ProjectId = projectId;
            ViewBag.PersonId_ddl = _employeeService.GetPersonsWithActiveEmployeesDropDown();
            ViewBag.ProjectRoles_ddl = _nomenclatureService.GetDropDownList<ProjectRole>(false).OrderBy(i => i.Text);
        }
    }
}
