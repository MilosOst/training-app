using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.APIResponses;
using TrainingApp.Extensions;
using TrainingApp.Features.Trainings.Models;

namespace TrainingApp.Features.Trainings;


[ApiController]
[Route("trainings")]
[Authorize]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestResponse))]
[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(MessageResponse))]
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserTrainingDto>))]
    public async Task<ActionResult> GetTraining([FromQuery] DateTime date)
    {
        var trainings = await _trainingService.GetTrainingHistory(DateOnly.FromDateTime(date), HttpContext.GetUserId());
        return Ok(trainings);
    }
    
    [HttpPut("{id}")]
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> UpdateTraining(CreateTrainingRequest req, int id)
    {
        await _trainingService.UpdateTraining(req, HttpContext.GetUserId(), id);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteTraining(int id)
    {
        await _trainingService.DeleteTraining(HttpContext.GetUserId(), id);
        return NoContent();
    }
}