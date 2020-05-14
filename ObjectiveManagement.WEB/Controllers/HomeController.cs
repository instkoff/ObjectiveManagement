using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ObjectiveManagement.Domain.Contracts;
using ObjectiveManagement.Domain.Contracts.Exceptions;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.Web.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IObjectiveService _objectiveService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IObjectiveService objectiveService, ILogger<HomeController> logger)
        {
            _objectiveService = objectiveService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<MenuItemModel>> Create([FromBody] ObjectiveModel objectiveModel)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _objectiveService.Create(objectiveModel);
            return Ok(result);

        }

        [HttpGet("id")]
        public IActionResult Get(Guid id)
        {
            var model = _objectiveService.Get(id);

            if (model == null)
            {
                return BadRequest("Objective not found");
            }

            return PartialView("_ObjectiveDetails", model);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("add_sub_objective")]
        public IActionResult AddSubObjective(Guid Id)
        {
            ViewBag.parentId = Id;
            return PartialView("_CreateNewObjective", new ObjectiveModel());
        }

        [HttpGet("get_all")]
        public ActionResult<List<ObjectiveModel>> GetAllActiveObjectivesApi()
        {
            var objectiveModelList = _objectiveService.GetAllActive();

            if (objectiveModelList == null)
            {
                return BadRequest("Objectives not found.");
            }

            return Ok(objectiveModelList);
        }
        [HttpPut]
        public async Task<ActionResult> Update(ObjectiveModel model)
        {
            var result = await _objectiveService.Update(model);

            if (result == Guid.Empty)
            {
                return BadRequest("Objective not updated");
            }

            return Ok(result);
        }
        [HttpDelete("id")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var result = await _objectiveService.Delete(id);
            if (result) return Ok();
            _logger.LogError("Ошибка при удалении задачи.");
            throw new ObjectiveNotFoundException("Задача не найдена или у задачи есть подзадачи.");

        }
    }
}
