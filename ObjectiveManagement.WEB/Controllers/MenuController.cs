using System;
using Microsoft.AspNetCore.Mvc;
using ObjectiveManagement.Domain.Contracts;

namespace ObjectiveManagement.Web.Controllers
{
    /// <summary>
    /// Контроллер для меню
    /// </summary>
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        /// <summary>
        /// Получение списка корневых элементов
        /// </summary>
        /// <returns>Список корневых элементов</returns>
        [HttpGet("get_root")]
        public ActionResult GetRoot()
        {
            var menuItems = _menuService.GetTree();
            return Ok(menuItems);
        }
        /// <summary>
        /// Получение дочерних элементов
        /// </summary>
        /// <param name="id">Id родителя</param>
        /// <returns>Список подзадач</returns>
        [HttpGet("get_children")]
        public ActionResult GetChildren([FromQuery]Guid id)
        {
            var menuItems = _menuService.GetTree(id);
            return Ok(menuItems);
        }

    }
}
