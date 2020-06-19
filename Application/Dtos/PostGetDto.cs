using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class PostGetDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string BodyMarkDown { get; set; }

        public string ImagePath { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<TagDto> Tags { get; set; }

        public UserDto User { get; set; }
    }
}
