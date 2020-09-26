using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Models.MyDossier;
using IOWebFramework.Core.Models.ProjectDetail;
using IOWebFramework.Infrastructure.Data.Common;
using IOWebFramework.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace IOWebFramework.Core.Services
{
    public class MyDossierService : IMyDossierService
    {
        private readonly IRepository _repo;
        private readonly ILogger<MyDossierService> _logger;
        public MyDossierService(IRepository repo,
                              ILogger<MyDossierService> logger)
        {
            this._repo = repo;
            this._logger = logger;
        }

        public MyDossierViewModel GetMyDossierViewModelByIds(int personId, int employeeId)
        {
            var result = _repo.All<Employee>()
                         .Include(x => x.Person)
                         .Where(e => (e.Id == employeeId && e.Person.Id == personId))
                         .Select(e => new MyDossierViewModel()
                         {
                             Id = e.Id,
                             PID = e.Person.PID,
                             Td = e.Td,
                             HireDate = e.HireDate,
                             Email = e.Email,
                             Address = e.Address,
                             Branch = e.Branch,
                             Department = e.Departament,
                             Position = e.Position,
                             Telephone = e.Phone,
                             Photo = e.Person.Photo,
                             PersonName = e.Person.FullName
                         })
                         .FirstOrDefault();

            return result;
        }

        public IQueryable<ProjectPersonListViewModel> GetProjectPersonListData(int personId)
        {
            var result = _repo.AllReadonly<Team>()
                              .Where(c => c.PersonId == personId)
                              .Select(c => new ProjectPersonListViewModel()
                              {
                                  Id = c.Id,
                                  Name = c.Project.FullName,
                                  ProjectRole = c.ProjectRole.Label,
                                  StartDate = c.StartDate,
                                  EndDate = c.EndDate
                              });
            return result;
        }
    }
}
