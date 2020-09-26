using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using IOWebFramework.Components;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Models.Project;
using IOWebFramework.Extensions;
using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace IOWebFramework.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly INomenclatureService _nomenclatureService;
        private readonly IEmployeeService _employeeService;

        public ProjectController(IProjectService projectService, INomenclatureService nomenclatureService, IEmployeeService employeeService)
        {
            _projectService = projectService;
            _nomenclatureService = nomenclatureService;
            _employeeService = employeeService;
        }

        [HttpGet]
        [MenuItem("project")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            GetViewBags();
            var model = new ProjectViewModel();

            return View("Edit", model);
        }
        public IActionResult Edit(int projectId)
        {
            GetViewBags();
            var model = _projectService.GetProjectViewModelById(projectId);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProjectViewModel projectViewModel)
        {
            GetViewBags();
            //Backend validation is mandatory
            if (!ModelState.IsValid)
            {
                return View(projectViewModel);
            }

            var result = _projectService.SaveData(projectViewModel);
            this.ShowNotificationMessageOnUI(result);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ProjectListData(IDataTablesRequest request, bool onlyActive = true)
        {
            IQueryable<ProjectListViewModel> data = _projectService.GetProjects(onlyActive);
            var response = request.GetResponse(data);
            return new DataTablesJsonResult(response, true);
        }

        [HttpPost]
        public IActionResult AddClient(string code, string clientName, string description, bool isActive)
        {
            var result = _projectService.ClientNomenclatureSaveData(code, clientName, description, isActive);
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

        private void GetViewBags()
        {
            ViewBag.ClientId_ddl = _nomenclatureService.GetDropDownList<Client>();
            ViewBag.ManagerId_ddl = _employeeService.GetPersonsWithActiveEmployeesDropDown();
        }
    }
}
