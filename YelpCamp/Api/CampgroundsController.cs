using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YelpCamp.Models;
using YelpCamp.Service;

namespace YelpCamp.Api;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class CampgroundsController : ControllerBase
{
    private readonly CampgroundsService _campgrounds;

    public CampgroundsController(CampgroundsService campgroundsService)
        => _campgrounds = campgroundsService;
    
    [HttpGet]
    public async Task<List<Campground>> Get()
        => await _campgrounds.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Campground>> Get(string id)
    {
        var camp = await _campgrounds.GetAsync(id);
        return camp is not null ? camp : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Post(Campground newCamp)
    {
        await _campgrounds.CreateAsync(newCamp);
        return CreatedAtAction(nameof(Get), new { id = newCamp }, newCamp);
    }
}