using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using IOWebFramework.Components;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Models.ProjectDetail;
using IOWebFramework.Extensions;
using IOWebFramework.Infrastructure.Data.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOWebFramework.Controllers
{
    public class MyDossierController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMyDossierService _myDossierService;

        public MyDossierController(IEmployeeService employeeService,
                                UserManager<ApplicationUser> userManager,
                                IMyDossierService myDossierService)
        {
            this._employeeService = employeeService;
            this._userManager = userManager;
            _myDossierService = myDossierService;
        }

        [MenuItem("myDossier")]
        [Authorize(Roles = "HR, Employee")]
        public async Task<IActionResult> Index()
        {
            var principalUserName = HttpContext.User.Identity.Name; //ststanev
            var user = await _userManager.FindByNameAsync(principalUserName);
            int employeeId = _employeeService.GetEmployeeIdByEmail(user.Email);

            if (employeeId == 0)
            {
                this.ShowWarningNotificationMessageOnUI("В системата не съществува Ваше Досие!");
                return RedirectToAction("Index", "Home");
            }

            int personId = _employeeService.GetPersonIdByEmployeeId(employeeId);
            ViewBag.PersonId = personId;

            var model = _myDossierService.GetMyDossierViewModelByIds(personId, employeeId);

            return View(model);
        }

        [HttpPost]
        public IActionResult ProjectPersonListData(IDataTablesRequest request, int personId)
        {
            IQueryable<ProjectPersonListViewModel> data = _myDossierService.GetProjectPersonListData(personId);
            var response = request.GetResponse(data);

            return new DataTablesJsonResult(response, true);
        }
    }
}
