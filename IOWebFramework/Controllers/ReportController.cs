using IOWebFramework.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IOWebFramework.Controllers
{
    [Authorize(Roles = "HR, Managers")]
    public class ReportController: BaseController
    {
        [HttpGet]
        [MenuItem("report")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
