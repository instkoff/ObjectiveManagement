using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.Domain.Contracts
{
    public interface IObjectiveService
    {
        Task<Guid> Create(ObjectiveModel objectiveModel);
        Task<Guid> Update(ObjectiveModel objectiveModel);
        ObjectiveModel Get(Guid id);
        List<ObjectiveModel> GetAllActive();
        Task<bool> Delete(Guid id);
    }
}
