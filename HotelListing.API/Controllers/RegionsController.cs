using AutoMapper;
using HotelListing.API.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;

namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    readonly IRegionsRepository _regionsRepository;
    readonly IMapper _mapper;

    public RegionsController(IRegionsRepository regionsRepository, IMapper mapper)
    {
        _regionsRepository = regionsRepository;
        _mapper = mapper;
    }

    // GET: api/Regions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
    {
        return await _regionsRepository.GetAllAsync();
    }

    // GET: api/Regions/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Region>> GetRegion(int id)
    {
        var region = await _regionsRepository.GetAsync(id);

        if (region == null)
        {
            return NotFound();
        }

        return region;
    }

    // PUT: api/Regions/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRegion(int id, Region region)
    {
        if (id != region.Id)
        {
            return BadRequest();
        }

        try
        {
            await _regionsRepository.UpdateAsync(region);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RegionExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Regions
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Region>> PostRegion(Region region)
    {
        await _regionsRepository.AddAsync(region);

        return CreatedAtAction("GetRegion", new { id = region.Id }, region);
    }

    // DELETE: api/Regions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegion(int id)
    {
        var region = await _regionsRepository.GetAsync(id);
        if (region == null)
        {
            return NotFound();
        }

        await _regionsRepository.DeleteAsync(id);

        return NoContent();
    }

    async Task<bool> RegionExists(int id)
    {
        return await _regionsRepository.Exists(id);
    }
}