using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.APIResponses;

namespace TrainingApp.Features.Trainings.FixedDrills;

[Route("fixed_drills")]
[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(MessageResponse))]
public class FixedDrillsController: ControllerBase
{
    protected readonly IFixedDrillsService _fixedDrillsService;

    public FixedDrillsController(IFixedDrillsService fixedDrillsService)
    {
        _fixedDrillsService = fixedDrillsService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FixedDrill>))]
    public async Task<ActionResult<List<FixedDrill>>> GetFixedDrills()
    {
        return Ok(await _fixedDrillsService.GetFixedDrills());
    }
}