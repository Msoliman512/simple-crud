using Api.Features.Drivers.Commands;
using Api.Features.Drivers.Queries;
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

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetDriversResponse>> Get(GetDriversRequest request)
    {
        var getDriversResponse = await sender.Send(request);
        return Ok(getDriversResponse);
    }
    
    /// <summary>
    /// Get driver by ID.
    /// </summary>
    /// <param name="id">The Id of the driver to get.</param>
    /// <returns>The driver detailed information with more post calculated fields ex: AlphabetizedFullName.</returns>
    [AllowAnonymous]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetDriverByIdResponse>> GetById(int id)
    {
        var request = new GetDriverByIdRequest { Id = id };
        var getDriverByIdResponse = await sender.Send(request);
        return Ok(getDriverByIdResponse);
    }

}