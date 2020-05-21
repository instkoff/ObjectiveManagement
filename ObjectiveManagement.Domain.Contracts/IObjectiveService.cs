using System;
using System.Threading.Tasks;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.Domain.Contracts
{
    /// <summary>
    /// Интерфейс сервиса работы с задачами
    /// </summary>
    public interface IObjectiveService
    {
        Task<MenuItemModel> Create(ObjectiveModel objectiveModel);
        Task<Guid> Update(ObjectiveModel objectiveModel);
        ObjectiveModel Get(Guid id);
        Task<bool> Delete(Guid id);
    }
}
