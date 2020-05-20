using System;
using Microsoft.AspNetCore.Mvc;
using ObjectiveManagement.Domain.Contracts;
using ObjectiveManagement.Domain.Contracts.Exceptions;
using ObjectiveManagement.Domain.Contracts.Models;

namespace ObjectiveManagement.Web.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IObjectiveService _objectiveService;

        public HomeController(IObjectiveService objectiveService)
        {
            _objectiveService = objectiveService;
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("create_sub_objective")]
        public IActionResult CreateSubObjective([FromQuery]Guid parentId, [FromHeader(Name = "Objective-name")]string objectiveName)
        {
            ViewBag.parentId = parentId;
            ViewBag.objectiveName = objectiveName;
            return PartialView("_CreateObjective", new ObjectiveModel());
        }

        [HttpGet("create_objective")]
        public IActionResult CreateObjective()
        {
            return PartialView("_CreateObjective", new ObjectiveModel());
        }

        [HttpGet("get_objective")]
        public IActionResult GetObjective([FromQuery]Guid id)
        {
            var model = _objectiveService.Get(id);

            if (model == null)
                throw new ObjectiveNotFoundException($"Objective not found by id {id}");

                return PartialView("_ObjectiveDetails", model);
        }

    }
}
