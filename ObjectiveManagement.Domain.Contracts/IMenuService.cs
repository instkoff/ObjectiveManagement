using System;
using System.Collections.Generic;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.Domain.Contracts
{
    /// <summary>
    /// Интерфейс сервиса обработки элементов меню
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// Получение всех коренных элементов меню
        /// </summary>
        /// <returns>Список элементов меню</returns>
        List<MenuItemModel> GetTree();
        /// <summary>
        /// Получение подзадач конкретного элемента меню
        /// </summary>
        /// <param name="id">Идентификатор элемента</param>
        /// <returns>Список элементов меню</returns>
        List<MenuItemModel> GetTree(Guid id);
    }
}