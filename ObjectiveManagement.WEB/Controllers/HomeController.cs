using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ObjectiveManagement.Domain.Contracts;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.Web.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IObjectiveService _objectiveService;

        public HomeController(IObjectiveService objectiveService)
        {
            _objectiveService = objectiveService;
        }

        [HttpPost]
        public async Task<ActionResult<ObjectiveModel>> Create(ObjectiveModel objectiveModel)
        {
            var result = await _objectiveService.Create(objectiveModel);
            return Ok(result);
        }

        [HttpGet("id")]
        public ActionResult<ObjectiveModel> Get(Guid id)
        {
            var model = _objectiveService.Get(id);

            if (model == null)
            {
                return BadRequest("Objective not found");
            }

            return Ok(model);
        }

        [HttpGet]
        public IActionResult GetAllActiveObjectives()
        {
            var objectiveModelList = _objectiveService.GetAllActive();

            if (objectiveModelList == null)
            {
                return BadRequest("Objectives not found.");
            }

            return View(objectiveModelList);
        }

        [HttpGet("api/get_all")]
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
            if (!result) return BadRequest("Can't delete objective");
            return Ok();
        }
    }
}
