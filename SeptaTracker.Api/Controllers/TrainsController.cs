using Microsoft.AspNetCore.Mvc;
using SeptaTracker.Api.Models;

[ApiController]
[Route("api/[controller]")]
public class TrainsController : ControllerBase
{
    private readonly SeptaClient _septaClient;
    public TrainsController(SeptaClient SeptaClient)
    {
        _septaClient = SeptaClient;
    }

    [HttpGet("live")]
    public async Task<ActionResult<List<SeptaTrainDto>>> GetAllTrains()
    {
        var res = await _septaClient.GetTrainsAsync();
        return Ok(res);
    }
}