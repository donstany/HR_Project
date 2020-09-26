using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using IOWebFramework.Core.Contracts;
using System.Linq;
using IOWebFramework.Extensions;
using Microsoft.AspNetCore.Mvc;
using IOWebFramework.Core.Models.Dossier;
using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using Microsoft.AspNetCore.Identity;
using IOWebFramework.Infrastructure.Data.Models.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace IOWebFramework.Controllers
{
    //[Authorize(Roles = "HR,Employee")]
    public class DossierController : BaseController
    {
        private readonly IDossierService _dossierService;
        private readonly IEmployeeService _employeeService;
        private readonly INomenclatureService _nomenclatureService;
        private readonly UserManager<ApplicationUser> _userManager;

        public DossierController(IDossierService dossierService,
                                INomenclatureService nomenclatureService,
                                IEmployeeService employeeService,
                                UserManager<ApplicationUser> userManager)
        {
            this._dossierService = dossierService;
            this._nomenclatureService = nomenclatureService;
            this._employeeService = employeeService;
            this._userManager = userManager;
        }
        [Authorize(Roles = "HR, Employee")]
        public IActionResult Index(int employeeId, bool isUserDossier, string tabToOpen = null)
        {
            if (!string.IsNullOrEmpty(tabToOpen))
            {
                ViewBag.TabToOpen = tabToOpen;
            }

            ViewBag.PersonId = _employeeService.GetPersonIdByEmployeeId(employeeId);
            ViewBag.DossierLabel = _employeeService.GetPersonDetailsByEmployeeId(employeeId);
            ViewBag.isUserDossier = isUserDossier;

            var employeeMainInfo = _employeeService.GetEmployeeMainInfo(employeeId).FirstOrDefault();
            if (employeeMainInfo == null)
            {
                employeeMainInfo = new EmployeeMainInfoViewModel();
            }

            return View(employeeMainInfo);
        }


        public IActionResult Diplomas(int personId)
        {
            ViewBag.PersonId = personId;
            return View();
        }

        [HttpPost]
        public IActionResult DiplomaListData(IDataTablesRequest request, int personId)
        {
            IQueryable<DiplomaListViewModel> data = _dossierService.GetPersonDiplomas(personId);
            var response = request.GetResponse(data);
            return new DataTablesJsonResult(response, true);
        }
        [HttpGet]
        public IActionResult AddDiploma(int personId)
        {
            var model = new DiplomaViewModel{PersonId = personId};
            SetViewBagDiploma(personId);
            model.BreadcrumbInfo.Title = GetBreadcrumb(personId);
            model.IsAddingMode = true;
            return View(nameof(EditDiploma), model);
        }

        public IActionResult EditDiploma(int diplomaId, string tabToOpen = null)
        {
            if (!string.IsNullOrEmpty(tabToOpen))
            {
                ViewBag.TabToOpen = tabToOpen;
            }
            var diplomaViewModel = _dossierService.GetDiplomaViewModelById(diplomaId);
            SetViewBagDiploma(diplomaViewModel.PersonId);
            diplomaViewModel.BreadcrumbInfo.Title = GetBreadcrumb(diplomaViewModel.PersonId);

            return View(nameof(EditDiploma), diplomaViewModel);
        }

        [HttpPost]
        public IActionResult EditDiploma(DiplomaViewModel diplomaViewModel)
        {

            SetViewBagDiploma(diplomaViewModel.PersonId);
            if (!ModelState.IsValid)
            {
                return View(nameof(EditDiploma), diplomaViewModel);
            }
            if (diplomaViewModel.DegreeId != 3 && diplomaViewModel.SpecialtyId == null)
            {
                this.ShowWarningNotificationMessageOnUI("Полето 'Специалност' не е въведено!");
            }
            else if (diplomaViewModel.DegreeId == 3 && diplomaViewModel.SchoolProfileId == null)
            {
                this.ShowWarningNotificationMessageOnUI("Полето 'Профил' не е въведено!");
            }
            else
            {
                var idOfSavedEntity = _dossierService.DiplomaSaveData(diplomaViewModel);
                var isSavedinDb = idOfSavedEntity > 0;
                var employeeId = _employeeService.GetLastEmployeeIdByPersonId(diplomaViewModel.PersonId);
                if (isSavedinDb)
                {
                    this.ShowNotificationMessageOnUI(isSavedinDb);
                    return RedirectToAction(nameof(EditDiploma), new { diplomaId = idOfSavedEntity });
                }
                else
                {
                    this.ShowNotificationMessageOnUI(isSavedinDb);
                }
            }
            return View(nameof(EditDiploma), diplomaViewModel);
        }

        private void SetViewBagDiploma(int personId)
        {
            ViewBag.PersonId = personId;
            ViewBag.EmployeeId = _employeeService.GetLastEmployeeIdByPersonId(personId);
            ViewBag.DegreeId_ddl = _nomenclatureService.GetDropDownList<Degree>();
            ViewBag.EducationInstitutionId_ddl = _nomenclatureService.GetDropDownList<EducationInstitution>();
            ViewBag.SchoolProfileId_ddl = _nomenclatureService.GetDropDownList<SchoolProfile>();
        }

        public IActionResult Trainings(int personId)
        {
            ViewBag.PersonId = personId;
            return View();
        }

        [HttpPost]
        public IActionResult TrainingListData(IDataTablesRequest request, int personId)
        {
            IQueryable<TrainingListViewModel> data = _dossierService.GetTrainingsByPersonId(personId);
            var response = request.GetResponse(data);
            return new DataTablesJsonResult(response, true);
        }
        [HttpGet]
        public IActionResult AddTraining(int personId)
        {
            var trainingViewModel = new TrainingViewModel{PersonId = personId};
            SetViewbagTraining(personId);
            trainingViewModel.BreadcrumbInfo.Title = GetBreadcrumb(personId);
            trainingViewModel.IsAddingMode = true;
            return View(nameof(EditTraining), trainingViewModel);
        }

        [HttpPost]
        public IActionResult DeleteTraining(int id)
        {
            var model = _dossierService.DeleteTrainingById(id);
            return Json(model);
        }

        public IActionResult EditTraining(int trainingId, string tabToOpen = null)
        {
            if (!string.IsNullOrEmpty(tabToOpen))
            {
                ViewBag.TabToOpen = tabToOpen;
            }
   
            var trainingViewModel = _dossierService.GetTrainingById(trainingId);
            SetViewbagTraining(trainingViewModel.PersonId);
            trainingViewModel.BreadcrumbInfo.Title = GetBreadcrumb(trainingViewModel.PersonId);
            return View(nameof(EditTraining), trainingViewModel);
        }

        void SetViewbagTraining(int personId)
        {
            ViewBag.PersonId = personId;
            ViewBag.EmployeeId = _employeeService.GetLastEmployeeIdByPersonId(personId);
            ViewBag.TrainingNameId_ddl = _nomenclatureService.GetDropDownList<TrainingName>();
            ViewBag.TrainingCenterId_ddl = _nomenclatureService.GetDropDownList<TrainingCenter>();
        }

        [HttpPost]
        public IActionResult EditTraining(TrainingViewModel trainingViewModel)
        {
            SetViewbagTraining(trainingViewModel.PersonId);
            if (!ModelState.IsValid)
            {
                return View(nameof(EditTraining), trainingViewModel);
            }

            var id = _dossierService.TrainingSaveData(trainingViewModel);
            var isSavedInDb = id>0;
            var employeeId = _employeeService.GetLastEmployeeIdByPersonId(trainingViewModel.PersonId);
            if (isSavedInDb)
            {
                this.ShowNotificationMessageOnUI(isSavedInDb);
                return RedirectToAction(nameof(EditTraining), new { trainingId = id });
            }
            else
            {
                this.ShowNotificationMessageOnUI(isSavedInDb);
            }
            return View(nameof(EditTraining), trainingViewModel);
        }

        public IActionResult Certificates(int employeeId)
        {
            ViewBag.EmployeeId = employeeId;
            return View();
        }

        [HttpPost]
        public IActionResult CertificateListData(IDataTablesRequest request, int personId)
        {
            IQueryable<CertificateListViewModel> data = _dossierService.GetCertificatesByPersonId(personId);
            var response = request.GetResponse(data);
            return new DataTablesJsonResult(response, true);
        }

        public IActionResult AddCertificate(int personId)
        {
            var model = new CertificateViewModel{PersonId = personId};
            SetViewbagCertificate(personId);
            model.BreadcrumbInfo.Title = GetBreadcrumb(personId);

            return View(nameof(EditCertificate), model);
        }

        public IActionResult EditCertificate(int certificateId, string tabToOpen = null)
        {
            if (!string.IsNullOrEmpty(tabToOpen))
            {
                ViewBag.TabToOpen = tabToOpen;
            }

            var certificateViewModel = _dossierService.GetCertificateById(certificateId);
            SetViewbagCertificate(certificateViewModel.PersonId);
            certificateViewModel.BreadcrumbInfo.Title = GetBreadcrumb(certificateViewModel.PersonId);

            return View(nameof(EditCertificate), certificateViewModel);
        }

        void SetViewbagCertificate(int personId)
        {
            ViewBag.PersonId = personId;
            ViewBag.EmployeeId = _employeeService.GetLastEmployeeIdByPersonId(personId);
            ViewBag.CertificateTypeId_ddl = _nomenclatureService.GetDropDownList<CertificateType>();
            ViewBag.TrainingId_ddl = _dossierService.GetTrainingsDropDownByPersonId(personId, true);
        }

        [HttpPost]
        public IActionResult EditCertificate(CertificateViewModel certificateViewModel)
        {
            certificateViewModel.BreadcrumbInfo.Title = GetBreadcrumb(certificateViewModel.PersonId);
            SetViewbagCertificate(certificateViewModel.PersonId);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("CertificateNameIssuerId", "");
                return View(nameof(EditCertificate), certificateViewModel);
            }
            var employeeId = _employeeService.GetLastEmployeeIdByPersonId(certificateViewModel.PersonId);
            var id = _dossierService.CertificateSaveData(certificateViewModel);
            var result = id > 0;
            if (result)
            {
                this.ShowNotificationMessageOnUI(result);
                return RedirectToAction(nameof(EditCertificate), new { certificateId = id });
            }
            else
            {
                this.ShowNotificationMessageOnUI(result);
            }
            return View(nameof(EditCertificate), certificateViewModel);
        }

        [HttpPost]
        public IActionResult AddTrainingCenter(string code, string trainingCenterName, string description, bool isActive)
        {
            var result = _dossierService.TrainingCenterNomenclatureSaveData(code, trainingCenterName, description, isActive);
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
        public IActionResult AddTrainingName(string code, string trainingName, string description, bool isActive)
        {
            var result = _dossierService.TrainingNameNomenclatureSaveData(code, trainingName, description, isActive);
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
        public IActionResult AddEducationInstitution(string code, string educationInstitutionName, string description, bool isActive)
        {
            var result = _dossierService.EducationInstitutionNomenclatureSaveData(code, educationInstitutionName, description, isActive);
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
        private string GetBreadcrumb(int personId)
        {
            return _employeeService.GetPersonDetailsByPersonId(personId);
        }
    }
}