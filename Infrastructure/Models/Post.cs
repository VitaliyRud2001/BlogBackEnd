using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class Post : IEntityBase
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string BodyMarkDown { get; set; }

        public string ImagePath { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual User User { get; set; }

        public int UserId { get; set; }

        public virtual List<PostTag> Tags { get; set; }

    }
}
