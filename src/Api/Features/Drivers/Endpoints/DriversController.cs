using Api.Features.Drivers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Drivers.Endpoints;

[Route("drivers")]
public sealed class DriversController(ILogger<DriversController> logger, ISender sender) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateDriverResponse>> Create([FromForm] CreateDriverRequest request)
    {
        var createResponse= await sender.Send(request);
        return Ok(createResponse);
    }
    
    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UpdateDriverResponse>> Update([FromForm] UpdateDriverRequest request)
    {
        var updateResponse = await sender.Send(request);
        return Ok(updateResponse);
    }

    [AllowAnonymous]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<DeleteDriverResponse>> Delete(DeleteDriverRequest request)
    {
        var deleteResponse = await sender.Send(request);
        return Ok(deleteResponse);
    }
    
    [AllowAnonymous]
    [HttpPost("random-bulk-seed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateRandomBulkDriversResponse>> CreateRandomBulkDrivers(CreateRandomBulkDriversRequest request)
    {
        var createRandomBulkDriversResponse = await sender.Send(request);
        return Ok(createRandomBulkDriversResponse);
    }


}