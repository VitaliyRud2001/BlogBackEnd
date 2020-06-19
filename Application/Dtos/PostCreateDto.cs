using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class PostCreateDto
    {
        public string Title { get; set; }
        public string Body { get; set; }

        public string BodyMarkDown { get; set; }

        public int UserId { get; set; }

        public IFormFile Image { get; set; }
        public List<TagDto> Tags { get; set; }

    }
}
