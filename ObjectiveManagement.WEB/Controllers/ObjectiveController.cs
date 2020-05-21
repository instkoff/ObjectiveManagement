using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ObjectiveManagement.Domain.Contracts;
using ObjectiveManagement.Domain.Contracts.Exceptions;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.Web.Controllers
{
    /// <summary>
    /// Контроллер взаимодействия с задачами
    /// </summary>
    [Route("api/[controller]")]
    public class ObjectiveController : Controller
    {
        private readonly IObjectiveService _objectiveService;

        public ObjectiveController(IObjectiveService objectiveService)
        {
            _objectiveService = objectiveService;
        }
        /// <summary>
        /// Создание задачи
        /// </summary>
        /// <param name="objectiveModel"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult<MenuItemModel>> Create([FromBody]ObjectiveModel objectiveModel)
        {
            if (!ModelState.IsValid) 
                throw new ValidationException("Form validation error");
            var result = await _objectiveService.Create(objectiveModel);
            if (result == null) 
                throw new ObjectiveNotFoundException("Cant create objective, parent not found.");
            return Ok(result);

        }
        /// <summary>
        /// Обновление задачи
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> Update([FromBody]ObjectiveModel model)
        {
            if (!ModelState.IsValid) 
                throw new ValidationException("Form validation error");
            var result = await _objectiveService.Update(model);
            if (result == Guid.Empty)
                throw new ObjectiveNotFoundException("Objective not found for update.");
            return Ok(result);
        }
        /// <summary>
        /// Удаление задачи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete([FromBody]Guid id)
        {
            var result = await _objectiveService.Delete(id);
            if (result) return Ok();
            throw new ObjectiveNotFoundException("Can't delete objective or objective has subobjectives");
        }

    }
}
