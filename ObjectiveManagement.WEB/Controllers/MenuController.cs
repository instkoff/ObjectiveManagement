using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using ObjectiveManagement.Domain.Contracts;

namespace ObjectiveManagement.Web.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("get_root")]
        public ActionResult GetRoot()
        {
            var menuItems = _menuService.GetTree();
            return Ok(menuItems);
        }
        [HttpGet("get_children")]
        public ActionResult GetChildren([FromQuery]Guid id)
        {
            var menuItems = _menuService.GetTree(id);
            return Ok(menuItems);
        }

    }
}
