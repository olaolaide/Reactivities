using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivitiesController(DataContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities() => await context.Activities.ToListAsync();

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Activity>> GetActivity(Guid id)
    {
        var activity = await context.Activities.FindAsync(id);
        if (activity is null) return NotFound("No activity of Id");
        return Ok(activity);
    }
}