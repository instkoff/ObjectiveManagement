using System;
using Microsoft.AspNetCore.Mvc;
using ObjectiveManagement.Domain.Contracts;
using ObjectiveManagement.Domain.Contracts.Exceptions;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.Web.Controllers
{
    /// <summary>
    /// Контроллер для отображения форм 
    /// </summary>
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IObjectiveService _objectiveService;

        public HomeController(IObjectiveService objectiveService)
        {
            _objectiveService = objectiveService;
        }

        /// <summary>
        /// Отображение основного окна
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Отображение формы для добавления подзадачи
        /// </summary>
        /// <param name="parentId">Id родительской задачи</param>
        /// <returns>Частичное представление</returns>
        [HttpGet("create_sub_objective")]
        public IActionResult CreateSubObjective([FromQuery]Guid parentId)
        {
            ViewBag.parentId = parentId;
            ViewBag.objectiveName = Request.Cookies["selectedNodeName"];
            return PartialView("_CreateObjective", new ObjectiveModel());
        }
        /// <summary>
        /// Отображение формы добавления задачи
        /// </summary>
        /// <returns>Частичное представление</returns>
        [HttpGet("create_objective")]
        public IActionResult CreateObjective()
        {
            return PartialView("_CreateObjective", new ObjectiveModel());
        }
        /// <summary>
        /// Отображение формы с деталями задачи
        /// </summary>
        /// <param name="id">Id задачи</param>
        /// <returns>Частичное представление</returns>
        [HttpGet("get_objective")]
        public IActionResult GetObjective([FromQuery]Guid id)
        {
            var model = _objectiveService.Get(id);
            if (model == null)
                throw new ObjectiveNotFoundException($"Objective not found by id {id}");
            Response.Cookies.Append("selectedNodeName", model.Name);
            return PartialView("_ObjectiveDetails", model);
        }

    }
}
