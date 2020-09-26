using CDN.Core3.Data.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IOWebFramework.Controllers
{
    public class CdnController : BaseController
    {
        private readonly ICdnService cdnService;

        public CdnController(ICdnService _cdnService)
        {
            cdnService = _cdnService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}