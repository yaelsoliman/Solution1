using Application.Features.Properties.Commands;
using Application.Features.Properties.Queries;
using Application.Models;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ISender _mediatrSender;

        public PropertiesController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddNewProperty([FromBody] NewProperty newPropertyRequest)
        {
            bool isSuccessfull = await _mediatrSender.Send(new CreatePropertyRequest(newPropertyRequest));
            if (isSuccessfull)
            {
                return Ok("Property Created");
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePropertyAsync([FromBody] UpdateProperty updateProperty)
        {
            bool isSuccessfull =await _mediatrSender.Send(new UpdatePropertyRequest(updateProperty));
            if (isSuccessfull)
            {
                return Ok("Property Updated");
            }
            return NotFound("Property does not exists");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            PropertyDto propertyDto=await _mediatrSender.Send(new GetPropertyRequest(id));
            if (propertyDto is not null)
                return Ok(propertyDto);
            return NotFound("Property does not exists");
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProperties()
        {
            List<PropertyDto> propertiesDto = await _mediatrSender.Send(new GetPropertiesRequest());
            if(propertiesDto is not null)
                return Ok(propertiesDto);
            return NotFound("There isn't any Property");
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            bool isSuccessfull=await _mediatrSender.Send(new DeletePropertyRequest(id));
            if (isSuccessfull)
                return Ok("Property has Deleted");
            return NotFound("Property does not exist");
        }
    }
}
