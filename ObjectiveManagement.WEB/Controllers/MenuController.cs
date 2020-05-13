using System;
using Microsoft.AspNetCore.Mvc;
using ObjectiveManagement.Domain.Contracts;

namespace ObjectiveManagement.Web.Controllers
{
    [Route("api/[controller]")]
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

            if (menuItems == null)
            {
                return BadRequest("Objectives not found.");
            }
            return Ok(menuItems);
        }
        [HttpGet("get_children/{id}")]
        public ActionResult GetChildren(Guid id)
        {
            var menuItems = _menuService.GetTree(id);

            if (menuItems == null)
            {
                return BadRequest("Objectives not found.");
            }
            return Ok(menuItems);
        }

    }
}
