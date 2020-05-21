using System;
using System.Collections.Generic;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.Domain.Contracts
{
    public interface IMenuService
    {
        /// <summary>
        /// Интерфейс сервиса обработки элементов меню
        /// </summary>
        /// <returns></returns>
        List<MenuItemModel> GetTree();
        List<MenuItemModel> GetTree(Guid id);
    }
}