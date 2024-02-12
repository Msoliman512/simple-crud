using Api.Features.Drivers.Commands;
using Api.Features.Drivers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Drivers.Endpoints;

[Route("drivers")]
public sealed class DriversController(ILogger<DriversController> logger, ISender sender) : ControllerBase
{
    /// <summary>
    /// Create a new driver.
    /// </summary>
    /// <param name="request">The request to create a new driver.</param>
    /// <returns>The created driver Id.</returns>
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateDriverResponse>> Create([FromForm] CreateDriverRequest request)
    {
        var createResponse= await sender.Send(request);
        return Ok(createResponse);
    }
    
    /// <summary>
    /// Update an existing driver.
    /// </summary>
    /// <param name="request">The request to update an existing driver.</param>
    /// <returns>true if updated successfully and false otherwise.</returns>
    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UpdateDriverResponse>> Update([FromForm] UpdateDriverRequest request)
    {
        var updateResponse = await sender.Send(request);
        return Ok(updateResponse);
    }

    
    /// <summary>
    /// Delete a driver.
    /// </summary>
    /// <param name="request">The request to delete a driver by it's Id.</param>
    /// <returns>true if deleted successfully and false otherwise.</returns>
    [AllowAnonymous]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<DeleteDriverResponse>> Delete(DeleteDriverRequest request)
    {
        var deleteResponse = await sender.Send(request);
        return Ok(deleteResponse);
    }
    
    /// <summary>
    /// Bulk random drivers insertion 
    /// </summary>
    /// <param name="request">The request to Add bulk of random drivers.</param>
    /// <returns>return the number of added drivers.</returns>
    [AllowAnonymous]
    [HttpPost("random-bulk-seed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateRandomBulkDriversResponse>> CreateRandomBulkDrivers([FromForm] CreateRandomBulkDriversRequest request)
    {
        var createRandomBulkDriversResponse = await sender.Send(request);
        return Ok(createRandomBulkDriversResponse);
    }

    /// <summary>
    /// Get drivers paginated list.
    /// </summary>
    /// <param name="request">The Id of the driver.</param>
    /// <returns>Paginated list of the drivers with ability to filter and sort.</returns>
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