using IOWebFramework.Components;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace IOWebFramework.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _employeeService;

        public HomeController(ILogger<HomeController> logger,
                              IEmployeeService employeeService)
        {
            this._logger = logger;
            this._employeeService = employeeService;
        }

        [MenuItem("home")]
        public IActionResult Index()
        {
            var syncedEmployees = _employeeService.GetSyncInfoForUI();

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("LastSyncDate")))
            {
                HttpContext.Session.SetString("SyncedEmployeesCount", syncedEmployees.Count.ToString());
                HttpContext.Session.SetString("LastSyncDate", syncedEmployees.SyncDate.ToString("dd.MM.yyyyг. HH:mm"));
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
