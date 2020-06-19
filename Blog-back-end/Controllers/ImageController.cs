using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<ActionResult<ImageDto>> SaveImage([FromForm] IFormFile file)
        {
            var uploadedUrl = await _imageService.UploadImageAsync(file);
            return new ImageDto
            {
                ImageUrl = uploadedUrl,
                FileName = file?.FileName
            };
        }

    }
}