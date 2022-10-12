using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models.Hotel;

namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    readonly IHotelsRepository _hotelsRepository;
    readonly IMapper _mapper;

    public HotelsController(IHotelsRepository hotelsRepository, IMapper mapper)
    {
        _hotelsRepository = hotelsRepository;
        _mapper = mapper;
    }

    // GET: api/Hotels
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetHotelDO>>> GetHotels()
    {
        var hotels = await _hotelsRepository.GetAllAsync();
        var records = _mapper.Map<List<GetHotelDO>>(hotels);
        
        return Ok(records);
    }

    // GET: api/Hotels/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Hotel>> GetHotel(int id)
    {
        var hotel = await _hotelsRepository.GetAsync(id);

        if (hotel == null)
        {
            return NotFound();
        }

        var hotelDO = _mapper.Map<HotelDO>(hotel);

        return Ok(hotelDO);
    }

    // PUT: api/Hotels/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHotel(int id, UpdateHotelDO updateHotelDO)
    {
        if (id != updateHotelDO.Id)
        {
            return BadRequest();
        }

        var hotel = await _hotelsRepository.GetAsync(id);

        if (hotel == null)
        {
            return NotFound();
        }

        _mapper.Map(updateHotelDO, hotel);

        try
        {
            await _hotelsRepository.UpdateAsync(hotel);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await HotelExists(id))
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

    // POST: api/Hotels
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Hotel>> PostHotel(PostHotelDO postHotelDO)
    {
        var hotel = _mapper.Map<Hotel>(postHotelDO);

        await _hotelsRepository.AddAsync(hotel);

        return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
    }

    // DELETE: api/Hotels/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var hotel = await _hotelsRepository.GetAsync(id);
        if (hotel == null)
        {
            return NotFound();
        }

        await _hotelsRepository.DeleteAsync(id);

        return NoContent();
    }

    async Task<bool> HotelExists(int id)
    {
        return await _hotelsRepository.Exists(id);
    }
}