using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Drivers.Endpoints;

[Route("drivers")]
public sealed class DriversController(ILogger<DriversController> logger) : ControllerBase
{
    [HttpGet(Name = "blah")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Object Get()
    {
        logger.LogInformation("Getting blah ................................................................");
        
        return Ok("blah!");
    }
    
}