using IOWebFramework.Core.Models.MyDossier;
using IOWebFramework.Core.Models.ProjectDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOWebFramework.Core.Contracts
{
    public interface IMyDossierService
    {
        /// <summary>
        /// Връща MyDossierViewModel от подадено employeeId и personId
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        MyDossierViewModel GetMyDossierViewModelByIds(int personId, int employeeId);
        /// <summary>
        /// връща списък с проекти, в които е участвал даденият служител
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        IQueryable<ProjectPersonListViewModel> GetProjectPersonListData(int personId);
    }
}
