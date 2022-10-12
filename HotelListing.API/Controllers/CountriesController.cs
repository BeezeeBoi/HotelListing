using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Models.Country;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public CountriesController(ICountriesRepository countriesRepository)
    {
        _countriesRepository = countriesRepository;
    }

    // GET: api/Countries/GetAllCountries
    [HttpGet("GetAllCountries")]
    [EnableQuery]
    [ResponseCache(NoStore = true)]
    public async Task<ActionResult<IEnumerable<GetCountryDO>>> GetCountries()
    {
        var countries = await _countriesRepository.GetAllAsync<GetCountryDO>();

        return Ok(countries);
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
        return Ok(country);
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

        try
        {
            await _countriesRepository.UpdateAsync(id, updateCountryDO);
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
    public async Task<ActionResult<CountryDO>> PostCountry(PostCountryDO countryDO)
    {
        var country = await _countriesRepository.AddAsync<PostCountryDO, GetCountryDO>(countryDO);
        return CreatedAtAction(nameof(PostCountry), new { id = country.Id }, country);
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        await _countriesRepository.DeleteAsync(id);
        return NoContent();
    }

    async Task<bool> CountryExists(int id)
    {
        return await _countriesRepository.Exists(id);
    }
}