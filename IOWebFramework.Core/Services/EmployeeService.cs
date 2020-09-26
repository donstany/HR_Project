using AutoMapper;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Models;
using IOWebFramework.Core.Models.Cv;
using IOWebFramework.Core.Models.Dossier;
using IOWebFramework.Core.Models.Employees;
using IOWebFramework.Infrastructure.Data.Common;
using IOWebFramework.Infrastructure.Data.Models;
using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using IOWebFramework.Shared.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
//using AutoMapper;

namespace IOWebFramework.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository _repo;
        private readonly ILogger<EmployeeService> _logger;
        private readonly IMapper _mapper;
        private readonly IExperienceCalculator _experienceCalculator;
        public EmployeeService(IRepository repo,
                               ILogger<EmployeeService> logger,
                               IMapper mapper,
                               IExperienceCalculator experienceCalculator)
        {
            this._repo = repo;
            this._logger = logger;
            this._experienceCalculator = experienceCalculator;
            this._mapper = mapper;
        }

        public List<SelectListItem> GetDepartmentsDropDownByBranchId(int branchId)
        {
            var result = _repo.AllReadonly<Department>()
                              .Where(x => x.BranchId == branchId)
                              .Select(x =>
                              new SelectListItem
                              {
                                  Text = x.Label,
                                  Value = x.Id.ToString()
                              }).ToList();

            return result;
        }

        public IQueryable<EmployeeListViewModel> GetEmployees(bool isActive = true)
        {
            var result = _repo.AllReadonly<Employee>()
                                .Include(x => x.Person)
                                .Where(x => x.IsActive == isActive)
                                .Select(x => new EmployeeListViewModel()
                                {
                                    Id = x.Id,
                                    PersonId = x.Person.Id,
                                    Td = x.Td,
                                    Branch = x.Branch,
                                    Department = x.Departament,
                                    Name = x.Person.FullName
                                });
            return result;
        }

        public List<SelectListItem> GetPersonDropDown()
        {
            var result = _repo.AllReadonly<Person>()
                              .Select(e => new SelectListItem()
                              {
                                  Text = e.FullName,
                                  Value = e.Id.ToString()
                              }).ToList();

            return result;
        }

        public bool SaveData(EmployeeViewModel model)
        {
            bool result = false;
            Employee employeeEntity = null;
            Person personEntity = null;
            try
            {
                if (model.Id > 0) //update
                {
                    var bytesPhoto = Convert.FromBase64String(model.PhotoBase64 ?? string.Empty);

                    employeeEntity = _repo.GetById<Employee>(model.Id);

                    personEntity = _repo.AllReadonly<Person>()
                                        .FirstOrDefault(x => x.PID == model.UserDetailDTO.PID); // Search By EGN not by PersonId


                    // In System must exist ONLY 1 Person with unique PID ! Relation is 1 Person with multiple Employees
                    if (personEntity == null)
                    {
                        result = false;
                        return result;
                    }

                    personEntity.FullName = model.UserDetailDTO.FullName;
                    personEntity.PID = model.UserDetailDTO.PID;
                    personEntity.Photo = bytesPhoto;
                    _repo.Update(personEntity);
                    _repo.SaveChanges();

                    employeeEntity.Td = model.Td;
                    employeeEntity.FileContentId = model.FileContentId;
                    employeeEntity.HireDate = model.HireDate;
                    employeeEntity.FireDate = model.FireDate;
                    var aggregatedPreviuosExperienceInIO = _experienceCalculator.AggregateDateTokens(model.ExpirienceInIOYearsId, model.ExpirienceInIOMonthsId, model.ExpirienceInIODaysId);
                    var aggregatedPreviuosExperienceOutIO = _experienceCalculator.AggregateDateTokens(model.ExpirienceOutIOYearsId, model.ExpirienceOutIOMonthsId, model.ExpirienceOutIODaysId);
                    var aggregatedPreviuosExperienceSummed = _experienceCalculator.AggregateDateTokens((model.ExpirienceOutIOYearsId + model.ExpirienceInIOYearsId), (model.ExpirienceInIOMonthsId + model.ExpirienceOutIOMonthsId), (model.ExpirienceInIODaysId + model.ExpirienceOutIODaysId));
                    employeeEntity.PreviuosExperienceSummed = aggregatedPreviuosExperienceSummed;
                    employeeEntity.PreviuosExperienceInIs = aggregatedPreviuosExperienceInIO;
                    employeeEntity.PreviuosExperience = aggregatedPreviuosExperienceOutIO;
                    employeeEntity.Position = model.Position;
                    employeeEntity.Address = model.Address;
                    employeeEntity.Branch = model.Branch;
                    employeeEntity.Departament = model.Department;
                    employeeEntity.Email = model.Email;
                    employeeEntity.Phone = model.Telephone;
                    //employeeEntity.DepartmentId = model.DepartmentId;
                    //employeeEntity.PositionId = model.PositionId;
                    employeeEntity.IsActive = model.IsActive;
                    _repo.Update(employeeEntity);
                    _repo.SaveChanges();
                }
                else // insert
                {
                    var isNewBloodEmployee = !_repo.AllReadonly<Employee>()
                                                    .Include(x => x.Person)
                                                    .Where(x => x.Person.PID == model.UserDetailDTO.PID)
                                                    .Any();

                    //Check is Employee leaved Company
                    var isEmployeeLeaved = !_repo.AllReadonly<Employee>()
                                                 .Include(x => x.Person)
                                                 .Where(x => x.Person.PID == model.UserDetailDTO.PID)
                                                 .All(x => ((x.IsWorking == true) || isNewBloodEmployee)); // Or use All

                    if (isEmployeeLeaved)
                    {
                        result = false;
                        return result;
                    }

                    var bytesPhoto = Convert.FromBase64String(model.PhotoBase64 ?? string.Empty);

                    //SEARCH FOR EXISTING PERSON TO AVOID DUPLICATE PERSON IN DB
                    var existingPerson = _repo.AllReadonly<Person>()
                                                .FirstOrDefault(x => x.PID == model.UserDetailDTO.PID);

                    if (existingPerson == null)
                    {
                        personEntity = new Person()
                        {
                            FullName = model.UserDetailDTO.FullName,
                            PID = model.UserDetailDTO.PID,
                            Photo = bytesPhoto
                        };

                        _repo.Add<Person>(personEntity);
                        _repo.SaveChanges();
                    }


                    var savedPersonId = _repo.AllReadonly<Person>()
                                              .FirstOrDefault(x => x.PID == model.UserDetailDTO.PID).Id;

                    var aggregatedPreviuosExperienceInIO = _experienceCalculator.AggregateDateTokens(model.ExpirienceInIOYearsId, model.ExpirienceInIOMonthsId, model.ExpirienceInIODaysId);
                    var aggregatedPreviuosExperienceOutIO = _experienceCalculator.AggregateDateTokens(model.ExpirienceOutIOYearsId, model.ExpirienceOutIOMonthsId, model.ExpirienceOutIODaysId);
                    var aggregatedPreviuosExperienceSummed = _experienceCalculator.AggregateDateTokens((model.ExpirienceOutIOYearsId + model.ExpirienceInIOYearsId), (model.ExpirienceInIOMonthsId + model.ExpirienceOutIOMonthsId), (model.ExpirienceInIODaysId + model.ExpirienceOutIODaysId));
                    employeeEntity = new Employee()
                    {
                        PersonId = savedPersonId,
                        Td = model.Td,
                        HireDate = model.HireDate,
                        FireDate = model.FireDate,
                        PreviuosExperienceSummed = aggregatedPreviuosExperienceSummed,
                        PreviuosExperienceInIs = aggregatedPreviuosExperienceInIO,
                        PreviuosExperience = aggregatedPreviuosExperienceOutIO,
                        Departament = model.Department,
                        Branch = model.Branch,
                        Phone = model.Telephone,
                        Address = model.Address,
                        Email = model.Email,
                        Position = model.Position,
                        IsActive = model.IsActive,
                        FileContentId = model.FileContentId,
                    };

                    _repo.Add<Employee>(employeeEntity);
                    _repo.SaveChanges();

                    //_repo.SaveChanges();
                }
                //_repo.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Грешка при запис на служител ({ nameof(EmployeeService) })");
            }
            return result;
        }

        public string GetPersonDetailsByPersonId(int personId)
        {
            var result = _repo.AllReadonly<Person>()
                                         .Where(x => x.Id == personId)
                                         .Select(x => x.FullName)
                                         .FirstOrDefault();
            return result;
        }
        public string GetPersonDetailsByEmployeeId(int employeeId)
        {
            var result = _repo.AllReadonly<Employee>()
                                         .Include(x => x.Person)
                                         .Where(x => x.Id == employeeId)
                                         .Select(x => x.Person.FullName)
                                         .FirstOrDefault();
            return result;
        }

        public IQueryable<EmployeeMainInfoViewModel> GetEmployeeMainInfo(int employeeId)
        {
            var result = _repo.AllReadonly<Employee>()
                                         .Include(x => x.Person)
                                         .Where(x => x.Id == employeeId)
                                         .Select(res => new
                                         {
                                             EmployeeMainInfoViewModel = res,
                                             FormatedPreviuosExperienceInIsYear = _experienceCalculator.SplitDate(res.PreviuosExperienceInIs).Year,
                                             FormatedPreviuosExperienceInIsMonth = _experienceCalculator.SplitDate(res.PreviuosExperienceInIs).Month,
                                             FormatedPreviuosExperienceInIsDay = _experienceCalculator.SplitDate(res.PreviuosExperienceInIs).Day,
                                             FormatedPreviuosExperienceYear = _experienceCalculator.SplitDate(res.PreviuosExperience).Year,
                                             FormatedPreviuosExperienceMonth = _experienceCalculator.SplitDate(res.PreviuosExperience).Month,
                                             FormatedPreviuosExperienceDay = _experienceCalculator.SplitDate(res.PreviuosExperience).Day,
                                             FormatedPreviuosExperienceSummedYear = _experienceCalculator.SplitDate(res.PreviuosExperienceSummed).Year,
                                             FormatedPreviuosExperienceSummedMonth = _experienceCalculator.SplitDate(res.PreviuosExperienceSummed).Month,
                                             FormatedPreviuosExperienceSummedDay = _experienceCalculator.SplitDate(res.PreviuosExperienceSummed).Day,
                                         })
                                         .Select(res => new EmployeeMainInfoViewModel()
                                         {
                                             Branch = res.EmployeeMainInfoViewModel.Branch,
                                             Department = res.EmployeeMainInfoViewModel.Departament,
                                             Position = res.EmployeeMainInfoViewModel.Position,
                                             Td = res.EmployeeMainInfoViewModel.Td,
                                             FormatedPreviuosExperienceInIs = $"{res.FormatedPreviuosExperienceInIsYear} г. {res.FormatedPreviuosExperienceInIsMonth} м. {res.FormatedPreviuosExperienceInIsDay} д.",
                                             FormatedPreviuosExperienceSummed = $"{res.FormatedPreviuosExperienceSummedYear} г. {res.FormatedPreviuosExperienceSummedMonth} м. {res.FormatedPreviuosExperienceSummedDay} д.",
                                             FormatedPreviuosExperience = $"{res.FormatedPreviuosExperienceYear} г. {res.FormatedPreviuosExperienceMonth} м. {res.FormatedPreviuosExperienceDay} д.",
                                             Photo = res.EmployeeMainInfoViewModel.Person.Photo
                                         });

            return result;
        }

        public int GetPersonIdByEmployeeId(int employeeId)
        {
            var result = _repo.AllReadonly<Employee>().FirstOrDefault(e => e.Id == employeeId).PersonId;

            return result;
        }

        public int GetLastEmployeeIdByPersonId(int personId)
        {
            var employee = _repo.AllReadonly<Employee>().Where(e => e.PersonId == personId).OrderByDescending(e => e.HireDate).FirstOrDefault();
            var result = employee == null ? 0 : employee.Id;
            return result;
        }


        public EmployeeViewModel GetEmployeeViewModelById(int employeeId, int personId)
        {
            var result = _repo.All<Employee>()
                          .Include(x => x.Person)
                          .Where(e => (e.Id == employeeId && e.Person.Id == personId))
                          .Select(e => new EmployeeViewModel()
                          {
                              Id = e.Id,
                              PID = e.Person.PID,
                              Td = e.Td,
                              FileContentId = e.FileContentId,
                              HireDate = e.HireDate,
                              FireDate = e.FireDate,
                              PreviuosExperienceSummed = e.PreviuosExperienceSummed,
                              PreviuosExperienceInIO = e.PreviuosExperienceInIs,
                              PreviuosExperience = e.PreviuosExperience,
                              IsActive = e.IsActive,
                              Email = e.Email,
                              Address = e.Address,
                              Branch = e.Branch,
                              Department = e.Departament,
                              Position = e.Position,
                              Telephone = e.Phone,
                              PhotoBase64 = e.Person.Photo == null ? string.Empty : Convert.ToBase64String(e.Person.Photo),

                              UserDetailDTO = new UserDetailDTO()
                              {
                                  FullName = e.Person.FullName,
                                  Photo = e.Person.Photo,
                                  PID = e.Person.PID
                              }
                          })
                          .FirstOrDefault();

            SetDateToken(result);

            return result;
        }

        private void SetDateToken(EmployeeViewModel result)
        {
            result.ExpirienceInIOYearsId = _experienceCalculator.SplitDate(result.PreviuosExperienceInIO).Year;
            result.ExpirienceInIOMonthsId = _experienceCalculator.SplitDate(result.PreviuosExperienceInIO).Month;
            result.ExpirienceInIODaysId = _experienceCalculator.SplitDate(result.PreviuosExperienceInIO).Day;

            result.ExpirienceOutIOYearsId = _experienceCalculator.SplitDate(result.PreviuosExperience).Year;
            result.ExpirienceOutIOMonthsId = _experienceCalculator.SplitDate(result.PreviuosExperience).Month;
            result.ExpirienceOutIODaysId = _experienceCalculator.SplitDate(result.PreviuosExperience).Day;

        }

        public List<SelectListItem> GetInactiveEmployeesDropDown()
        {
            var model = _repo.AllReadonly<Employee>()
                             .Where(e => e.IsActive == false)
                             .Select(e => new SelectListItem()
                             {
                                 Text = e.Person.FullName,
                                 Value = e.PersonId.ToString()
                             }).ToList();
            return model;
        }

        public bool CheckTdIsUnique(string tdNumber)
        {
            var isTdUnique = !_repo.AllReadonly<Employee>()
                                .Any(e => e.Td == tdNumber);

            return isTdUnique;
        }

        public bool ImportDataFromAD()
        {
            var result = true;

            try
            {
                var dtoResults = ActiveDirectoryHelper.ImportAllUserDetails();

                var personEntities = dtoResults.Select(x => new Person()
                {
                    FullName = x.FullName,
                    Photo = x.Photo,
                });

                _repo.AddRange<Person>(personEntities);
                //_repo.UpdateRange<Person>(personEntities);
                _repo.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Грешка при импорт на данни ({ nameof(EmployeeService) })");
                result = false;
            }

            return result;
        }

        public EmployeeViewModel SyncDataFromADByEmail(string emailInAD)
        {
            var employeeViewModel = new EmployeeViewModel();

            if (emailInAD == null)
            {
                employeeViewModel.UserDetailDTO.IsSyncSuccessfully = false;
                return employeeViewModel;
            }

            try
            {

                employeeViewModel.UserDetailDTO = ActiveDirectoryHelper.ImportUsersDetailsByEmail(emailInAD).First();
                employeeViewModel.IsExistingActiveEmployee = _repo.All<Employee>().Any(x => (x.Email == emailInAD
                                                                                            && x.IsActive == true));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Грешка при синхронизиране на информация от АД по зададен емейл ({ nameof(EmployeeService) })");

                employeeViewModel.UserDetailDTO.IsSyncSuccessfully = false;
            }

            return employeeViewModel;
        }

        public void SyncJobScheduler()
        {
            var employeeEmailsFromDb = _repo.All<Person>()
                                        .SelectMany(x => x.Employee)
                                        .Select(x => x.Email).ToList();

            Employee employeeEntity = null;

            foreach (var itemEmployeeEmailsFromDb in employeeEmailsFromDb)
            {

                employeeEntity = _repo.All<Employee>()
                                        .Include(x => x.Person)
                                        .Where(x => x.Email == itemEmployeeEmailsFromDb)
                                        .FirstOrDefault();

                var userInfoFromAD = ActiveDirectoryHelper.ImportUsersDetailsByEmail(itemEmployeeEmailsFromDb).FirstOrDefault();

                employeeEntity.Person.FullName = userInfoFromAD.FullName;
                employeeEntity.Person.Photo = userInfoFromAD.Photo;
                employeeEntity.Person.SyncedAt = DateTime.Now;
                _repo.Update(employeeEntity);
                _repo.SaveChanges();

                employeeEntity.Position = userInfoFromAD.Position;
                employeeEntity.Address = userInfoFromAD.Address;
                employeeEntity.Branch = userInfoFromAD.Branch;
                employeeEntity.Departament = userInfoFromAD.Department;
                employeeEntity.Phone = userInfoFromAD.Telephone;
                employeeEntity.SyncedAt = DateTime.Now;
                _repo.Update(employeeEntity);
                _repo.SaveChanges();
            }

        }
        public EmployeeViewModel GetPersonDetails(int personId)
        {
            var person = _repo.GetById<Person>(personId);

            var lastEmployeePerPerson = _repo.All<Employee>()
                                //.Include(x => x.Department)
                                .Where(x => x.PersonId == personId)
                                .OrderByDescending(x => x.HireDate)
                                .FirstOrDefault();


            var userDetailDTO = new UserDetailDTO()
            {
                Photo = person.Photo
            };

            var resultDto = new EmployeeViewModel()
            {
                Td = lastEmployeePerPerson.Td,
                FileContentId = lastEmployeePerPerson?.FileContentId,
                FireDate = lastEmployeePerPerson?.FireDate,
                HireDate = lastEmployeePerPerson.HireDate,
                PreviuosExperience = lastEmployeePerPerson?.PreviuosExperience,
                PreviuosExperienceInIO = lastEmployeePerPerson?.PreviuosExperienceInIs,
                PreviuosExperienceSummed = lastEmployeePerPerson?.PreviuosExperienceSummed,

                Branch = lastEmployeePerPerson.Branch,
                Department = lastEmployeePerPerson.Departament,
                Position = lastEmployeePerPerson.Position,

                IsActive = lastEmployeePerPerson.IsActive,

                UserDetailDTO = userDetailDTO
            };

            return resultDto;
        }

        public int GetEmployeeIdByEmail(string email)
        {
            var result = _repo.AllReadonly<Employee>()
                                .FirstOrDefault(x => x.Email == email);

            var resultInt = result == null ? 0 : result.Id;
            return resultInt;
        }

        public CvViewModel GetCvViewModelById(int employeeId)
        {
            var personId = _repo.AllReadonly<Employee>()
                                .Where(e => e.Id == employeeId)
                                .Select(e => e.PersonId)
                                .FirstOrDefault();

            var result = _repo.AllReadonly<Person>()
                          .Include(x => x.Employee)
                          .Include(person => person.Diplomas).ThenInclude(diploma => diploma.EducationInstitution)
                          .Include(person => person.Diplomas).ThenInclude(diploma => diploma.Degree)
                          .Include(person => person.Diplomas).ThenInclude(diploma => diploma.Classifier)
                          .Include(person => person.Trainings).ThenInclude(training => training.TrainingCenter)
                          .Include(person => person.Trainings).ThenInclude(training => training.TrainingName)
                          .Include(person => person.Certificates).ThenInclude(certificate => certificate.CertificateNameIssuer)
                          .Include(person => person.Certificates).ThenInclude(certificate => certificate.CertificateType)
                          .Include(person => person.Teams).ThenInclude(team => team.Project)
                          .Include(person => person.Teams).ThenInclude(team => team.ProjectRole)
                          .Where(p => (p.Employee.FirstOrDefault(x => x.Id == employeeId).Id == employeeId))
                          .Select(res => new
                          {
                              CvViewModel = res,
                              FormatedPreviuosExperienceInIsYear = _experienceCalculator.SplitDate(res.Employee.OrderByDescending(x => x.HireDate).FirstOrDefault().PreviuosExperienceInIs).Year,
                              FormatedPreviuosExperienceInIsMonth = _experienceCalculator.SplitDate(res.Employee.OrderByDescending(x => x.HireDate).FirstOrDefault().PreviuosExperienceInIs).Month,
                              FormatedPreviuosExperienceInIsDay = _experienceCalculator.SplitDate(res.Employee.OrderByDescending(x => x.HireDate).FirstOrDefault().PreviuosExperienceInIs).Day,
                              FormatedPreviuosExperienceYear = _experienceCalculator.SplitDate(res.Employee.OrderByDescending(x => x.HireDate).FirstOrDefault().PreviuosExperience).Year,
                              FormatedPreviuosExperienceMonth = _experienceCalculator.SplitDate(res.Employee.OrderByDescending(x => x.HireDate).FirstOrDefault().PreviuosExperience).Month,
                              FormatedPreviuosExperienceDay = _experienceCalculator.SplitDate(res.Employee.OrderByDescending(x => x.HireDate).FirstOrDefault().PreviuosExperience).Day,
                              FormatedPreviuosExperienceSummedYear = _experienceCalculator.SplitDate(res.Employee.OrderByDescending(x => x.HireDate).FirstOrDefault().PreviuosExperienceSummed).Year,
                              FormatedPreviuosExperienceSummedMonth = _experienceCalculator.SplitDate(res.Employee.OrderByDescending(x => x.HireDate).FirstOrDefault().PreviuosExperienceSummed).Month,
                              FormatedPreviuosExperienceSummedDay = _experienceCalculator.SplitDate(res.Employee.OrderByDescending(x => x.HireDate).FirstOrDefault().PreviuosExperienceSummed).Day,
                          })
                          .Select(res => new CvViewModel()
                          {
                              FullName = res.CvViewModel.FullName,
                              Address = res.CvViewModel.Employee.OrderByDescending(x => x.HireDate).FirstOrDefault().Address,
                              PhoneNumber = res.CvViewModel.Employee.OrderByDescending(x => x.HireDate).FirstOrDefault().Phone,
                              Email = res.CvViewModel.Employee.OrderByDescending(x => x.HireDate).FirstOrDefault().Email,
                              FormatedPreviuosExperienceSummed = $"{res.FormatedPreviuosExperienceSummedYear} г. {res.FormatedPreviuosExperienceSummedMonth} м. {res.FormatedPreviuosExperienceSummedDay} д.",
                              FormatedPreviuosExperienceInIs = $"{res.FormatedPreviuosExperienceInIsYear} г. {res.FormatedPreviuosExperienceInIsMonth} м. {res.FormatedPreviuosExperienceInIsDay} д.",
                              FormatedPreviuosExperience = $"{res.FormatedPreviuosExperienceYear} г. {res.FormatedPreviuosExperienceMonth} м. {res.FormatedPreviuosExperienceDay} д.",
                              EducationDtos = res.CvViewModel.Diplomas.Select(x => new CvViewModel.EducationDto
                              {
                                  EducationInstitutionName = x.EducationInstitution.Description,
                                  DegreeName = x.Degree.Label,
                                  SpecialtyName = x.Classifier.Name
                              }).ToList(),
                              TrainingDtos = res.CvViewModel.Trainings.Select(x => new CvViewModel.TrainingDto
                              {
                                  TrainingName = x.TrainingName.Label,
                                  TrainingCenterName = x.TrainingCenter.Label,
                                  DateStart = x.DateStart,
                                  DateEnd = x.DateEnd
                              }).ToList(),
                              CertificateDtos = res.CvViewModel.Certificates.Select(x => new CvViewModel.CertificateDto
                              {
                                  CertificateName = x.CertificateNameIssuer.Name,
                                  CertificateType = x.CertificateType.Label,
                                  DateStart = x.DateStart,
                                  DateEnd = x.DateEnd
                              }).ToList(),
                              ProjectDtos = res.CvViewModel.Teams.Select(x => new CvViewModel.ProjectDto
                              {
                                  ProjectFullName = x.Project.FullName,
                                  ProjectName = x.Project.Name,
                                  ProjectRole = x.ProjectRole.Label,
                                  DateStart = x.StartDate,
                                  DateEnd = x.EndDate
                              }).ToList(),
                              PhotoBase64 = res.CvViewModel.Photo == null ? string.Empty : Convert.ToBase64String(res.CvViewModel.Photo)
                          })
                          .FirstOrDefault();

            return result;
        }

        public List<SelectListItem> GetPersonsWithActiveEmployeesDropDown()
        {
            var result = _repo.AllReadonly<Person>()
                              .Include(p => p.Employee)
                              .Where(p => p.Employee.Any(e => e.IsActive == true))
                              .Select(p => new SelectListItem()
                              {
                                  Value = p.Id.ToString(),
                                  Text = p.FullName
                              }).ToList();
            result = result.Prepend(new SelectListItem() { Text = "Избери", Value = null }).ToList();

            return result;
        }

        public List<SelectListItem> GetDateDropDown(int startFrom, int endTill)
        {
            var result = Enumerable.Range(startFrom, endTill).Select(
                x => new SelectListItem()
                {
                    Value = x.ToString(),
                    Text = x.ToString()
                }).ToList();

            return result;
        }

        public SyncedJobViewModel GetSyncInfoForUI()
        {
            var activeEmployees = _repo.AllReadonly<Person>()
                                        .Include(p => p.Employee)
                                        .Where(p => p.Employee.Any(x => x.IsActive == true));

            var countSyncedActiveEmployee = activeEmployees.Count();

            var syncedJobViewModel = activeEmployees
                           .Select(p => new SyncedJobViewModel
                           {
                               Count = countSyncedActiveEmployee,
                               SyncDate = p.Employee.OrderByDescending(x => x.SyncedAt)
                                                    .FirstOrDefault().SyncedAt
                           })
                           .OrderByDescending(x => x.SyncDate)
                           .First();

            return syncedJobViewModel;
        }

    }
}
