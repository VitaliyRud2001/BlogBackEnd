using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interface
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile formFile);
    }
}
