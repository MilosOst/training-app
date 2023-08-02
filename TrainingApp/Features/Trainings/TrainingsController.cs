using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Extensions;
using TrainingApp.Features.Trainings.Models;

namespace TrainingApp.Features.Trainings;


[ApiController]
[Route("trainings")]
[Authorize]
public class TrainingsController: ControllerBase
{
    private readonly ITrainingService _trainingService;

    public TrainingsController(ITrainingService trainingService)
    {
        _trainingService = trainingService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> CreateTraining([FromBody] CreateTrainingRequest req)
    {
        await _trainingService.RegisterTraining(req, HttpContext.GetUserId());
        return NoContent();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> GetTraining(CreateDateRequest req)
    {
        return Ok(await _trainingService.TrainingHistory(req, HttpContext.GetUserId()));
    }
}