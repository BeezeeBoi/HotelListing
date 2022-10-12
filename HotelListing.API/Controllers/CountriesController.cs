using AutoMapper;
using HotelListing.API.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Exceptions;
using HotelListing.API.Models.Country;
using HotelListing.API.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData.Query;

namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiVersion("1", Deprecated = false)]
[ApiController]
public class CountriesController : ControllerBase
{
    readonly ICountriesRepository _countriesRepository;
    readonly IMapper _mapper;

    public CountriesController(ICountriesRepository countriesRepository, IMapper mapper)
    {
        _countriesRepository = countriesRepository;
        _mapper = mapper;
    }

    // GET: api/Countries/GetAllCountries
    [HttpGet("GetAllCountries")]
    [EnableQuery]
    [ResponseCache(NoStore = true)]
    public async Task<ActionResult<IEnumerable<GetCountryDO>>> GetCountries()
    {
        var countries = await _countriesRepository.GetAllAsync();
        var records = _mapper.Map<List<GetCountryDO>>(countries);
        
        return Ok(records);
    }

    // GET: api/Countries/GetPagedCountries/?StartIndex=0&PageSize=25&PageNumber=1
    [HttpGet("GetPagedCountries")]
    public async Task<ActionResult<PagedResult<GetCountryDO>>> GetCountriesPaged([FromQuery] QueryParameters queryParameters)
    {
        var pagedResult = await _countriesRepository.GetAllAsync<GetCountryDO>(queryParameters);
        return Ok(pagedResult);
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CountryDO>> GetCountry(int id)
    {
        var country = await _countriesRepository.GetDetails(id);

        if (country == null)
        {
            throw new NotFoundException(nameof(GetCountry), id);
        }

        var countryDo = _mapper.Map<CountryDO>(country);

        return Ok(countryDo);
    }

    // PUT: api/Countries/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDO updateCountryDO)
    {
        if (id != updateCountryDO.Id)
        {
            return BadRequest();
        }

        var country = await _countriesRepository.GetAsync(id);

        if (country == null)
        {
            return NotFound();
        }

        _mapper.Map(updateCountryDO, country);

        try
        {
            await _countriesRepository.UpdateAsync(country);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await CountryExists(id))
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

    // POST: api/Countries
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Country>> PostCountry(PostCountryDO countryDO)
    {
        var country = _mapper.Map<Country>(countryDO);

        await _countriesRepository.AddAsync(country);

        return CreatedAtAction("GetCountry", new { id = country.Id }, country);
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var country = await _countriesRepository.GetAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        await _countriesRepository.DeleteAsync(id);

        return NoContent();
    }

    async Task<bool> CountryExists(int id)
    {
        return await _countriesRepository.Exists(id);
    }
}