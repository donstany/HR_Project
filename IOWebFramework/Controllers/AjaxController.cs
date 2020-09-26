using IO.LogOperation.Models;
using IO.LogOperation.Service;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IOWebFramework.Controllers
{
    public class AjaxController : Controller
    {
        private readonly INomenclatureService nomenclatureService;

        protected readonly ILogOperationService<ApplicationDbContext> logOperation;
        private readonly IDossierService dossierService;
        private readonly IClassifierService classifierService;

        public AjaxController(INomenclatureService _nomenclatureService,
            ILogOperationService<ApplicationDbContext> _logOperation,
            IDossierService _dossierService,
            IClassifierService _classifierService)
        {
            nomenclatureService = _nomenclatureService;
            logOperation = _logOperation;
            dossierService = _dossierService;
            classifierService = _classifierService;
        }

        [HttpGet]
        public IActionResult SearchEkatte(string query)
        {
            return new JsonResult(nomenclatureService.GetEkatte(query));
        }

        [HttpGet]
        public IActionResult GetEkatte(string id)
        {
            var ekatte = nomenclatureService.GetEkatteById(id);

            if (ekatte == null)
            {
                return BadRequest();
            }

            return new JsonResult(ekatte);
        }

        [HttpPost]
        public IActionResult DeleteTraining(int id)
        {
            var model = dossierService.DeleteTrainingById(id);

            return Json(model);
        }

        [HttpPost]
        public IActionResult DeleteCertificate(int id)
        {
            var model = dossierService.DeleteCertificateById(id);

            return Json(model);
        }

        [HttpGet]
        public IActionResult GetSpecialty(int id)
        {
            var specialty = classifierService.GetSpecialtyById(id);

            if (specialty == null)
            {
                return BadRequest();
            }

            return new JsonResult(specialty);
        }
       
        [HttpGet]
        public IActionResult SearchSpecialty(string query)
        {
            return new JsonResult(classifierService.SearchActivity(query));
        }

        [HttpGet]
        public IActionResult GetCertificateNameIssuer(int id)
        {
            var specialty = dossierService.GetCertificateNameIssuerById(id);

            if (specialty == null)
            {
                return BadRequest();
            }

            return new JsonResult(specialty);
        }

        [HttpGet]
        public IActionResult SearchCertificateNameIssuer(string query)
        {
            return new JsonResult(dossierService.SearcCertificateNameIssuer(query));
        }

        #region История на промените

        public JsonResult Get_LogOperation(string controller_name, string action_name, string object_key)
        {
            var model = logOperation.Select(controller_name?.ToLower(), action_name?.ToLower(), object_key)
                .ToList()
                .Select(i => new
                {
                    oper_date = i.OperationDate.ToString("dd.MM.yyyy HH:mm"),
                    user = i.OperationUser,
                    oper = GetOperationName(i.OperationTypeId),
                    id = i.Id
                });
            return Json(model);
        }
        private string GetOperationName(int operType)
        {
            return operType switch
            {
                (int)OperationTypes.Insert => "Добавяне",
                (int)OperationTypes.Message => "Известяване",
                (int)OperationTypes.Delete => "Изтриване",
                (int)OperationTypes.Patch => "Актуализация",
                _ => "Редакция",
            };
        }
        public ContentResult Get_LogOperationHTML(long id)
        {
            var html = logOperation.LoadData(id);
            return Content(html);
        }

        public JsonResult Get_LogOperationHTMLwithPrior(string controller_name, string action_name, string object_key, long currentId)
        {
            var priorOperation = logOperation.Select(controller_name?.ToLower(), action_name?.ToLower(), object_key)
                        .Where(x => x.Id < currentId)
                        .OrderByDescending(x => x.Id)
                        .Take(1)
                        .FirstOrDefault();
            var currentHtml = logOperation.LoadData(currentId);
            var priorHtml = string.Empty;
            if (priorOperation != null)
            {
                priorHtml = logOperation.LoadData(priorOperation.Id);
            }
            return Json(new { current = currentHtml, prior = priorHtml });
        }

        #endregion
    }
}