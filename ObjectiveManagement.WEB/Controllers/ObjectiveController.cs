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
    public class ObjectiveController : Controller
    {
        private readonly IObjectiveService _objectiveService;
        private readonly ILogger<HomeController> _logger;

        public ObjectiveController(IObjectiveService objectiveService, ILogger<HomeController> logger)
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
        [HttpPut]
        public async Task<ActionResult> Update([FromBody]ObjectiveModel model)
        {
            var result = await _objectiveService.Update(model);

            if (result == Guid.Empty)
            {
                return BadRequest("Objective not updated");
            }

            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete([FromQuery]Guid id)
        {
            var result = await _objectiveService.Delete(id);
            if (result) return Ok();
            _logger.LogError("Ошибка при удалении задачи.");
            throw new ObjectiveNotFoundException("Задача не найдена или у задачи есть подзадачи.");

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
    }
}
