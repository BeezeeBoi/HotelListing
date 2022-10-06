﻿using AutoMapper;
using HotelListing.API.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models.Region;

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
        var hotels = await _regionsRepository.GetAllAsync();
        var records = _mapper.Map<List<GetRegionDO>>(hotels);
        
        return Ok(records);
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

        var regionDO = _mapper.Map<RegionDO>(region);

        return Ok(regionDO);
    }

    // PUT: api/Regions/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRegion(int id, UpdateRegionDO updateRegionDO)
    {
        if (id != updateRegionDO.Id)
        {
            return BadRequest();
        }

        var region = await _regionsRepository.GetAsync(id);

        if (region == null)
        {
            return NotFound();
        }

        _mapper.Map(updateRegionDO, region);

        try
        {
            await _regionsRepository.UpdateAsync(region);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await RegionExists(id))
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
    public async Task<ActionResult<Region>> PostRegion(PostRegionDO postRegionDO)
    {
        var region = _mapper.Map<Region>(postRegionDO);

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