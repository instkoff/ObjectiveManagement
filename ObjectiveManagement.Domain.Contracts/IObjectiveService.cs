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
        /// <summary>
        /// Создание задачи
        /// </summary>
        /// <param name="objectiveModel">Модель задачи</param>
        /// <returns>Элемент нового объекта в меню</returns>
        Task<MenuItemModel> Create(ObjectiveModel objectiveModel);
        /// <summary>
        /// Обновление существующей задачи
        /// </summary>
        /// <param name="objectiveModel">Модель задачи</param>
        /// <returns>Id обновленной задачи</returns>
        Task<Guid> Update(ObjectiveModel objectiveModel);
        /// <summary>
        /// Получение задачи по Id
        /// </summary>
        /// <param name="id">Id задачи</param>
        /// <returns>Модель задачи</returns>
        ObjectiveModel Get(Guid id);
        /// <summary>
        /// Удалние задачи
        /// </summary>
        /// <param name="id">Id задачи</param>
        /// <returns>Признак успешного удаления</returns>
        Task<bool> Delete(Guid id);
    }
}
