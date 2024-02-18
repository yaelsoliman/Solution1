using Application.Features.Images.Commands;
using Application.Features.Images.Queries;
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
    public class ImagesController : ControllerBase
    {
        private readonly ISender _mediatorSender;

        public ImagesController(ISender mediatorSender)
        {
            _mediatorSender = mediatorSender;
        }
        [HttpPost("add")]
        public async Task<IActionResult> CreateImage([FromBody] NewImage newImage)
        {
            bool isSuccessful = await _mediatorSender.Send(new CreateImageRequest(newImage));
            if (isSuccessful)
            {
                return Ok("Created Image");
            }
            else
            {
                return BadRequest("Not Created");
            }
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateImage([FromBody] UpdateImage updateImage)
        {
            bool isSuccessful = await _mediatorSender.Send(new UpdateImageRequest(updateImage));
            if (isSuccessful)
            {
                return Ok("Updated Image");
            }
            else
            {
                return NotFound("Not Found");
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteImage(int Id)
        {
            bool isSuccessful = await _mediatorSender.Send(new DeleteImageRequest(Id));
            if (isSuccessful)
            {
                return Ok("Image Deleted");
            }
            else
            {
                return NotFound("Not Found");
            }
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetImage(int id)
        {
            ImageDto imageDto = await _mediatorSender.Send(new GetImageByIdRequest(id));
            if (imageDto != null)
                return Ok(imageDto);
            return NotFound("There is not any Image");
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAllImage()
        {
            List<ImageDto> imageDtos=await _mediatorSender.Send(new GetImagesRequest());
            if(imageDtos != null)
                return Ok(imageDtos);
            return NotFound("There is not any Image");
        }
    }
}
