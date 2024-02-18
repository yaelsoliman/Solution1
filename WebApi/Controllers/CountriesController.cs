using Application.Features.Countries.Commands;
using Application.Features.Countries.Queries;
using Application.Features.Properties.Queries;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ISender _medaitSender;

        public CountriesController(ISender medaitSender)
        {
            _medaitSender = medaitSender;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddNewCountry([FromBody] NewCountry newCountry)
        {
            bool isSuccessfully = await _medaitSender.Send(new CreateCountryRequest(newCountry));
            if (isSuccessfully)
            {

                return Ok("Created Country Successfully");
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCountry([FromBody] UpdateCountry updateCountry)
        {
            bool isSuccessfully = await _medaitSender.Send(new UpdateCountryRequest(updateCountry));
            if (isSuccessfully)
            {
                return Ok("Updated country Successfully");
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProperties()
        {
            List<CountryDto> countryDtos = await _medaitSender.Send(new GetCountriesRequest());
            if (countryDtos is not null)
                return Ok(countryDtos);
            return NotFound("There isn't any Property");
        }
    }
}
