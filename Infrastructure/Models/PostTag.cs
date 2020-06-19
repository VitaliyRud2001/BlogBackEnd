using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class PostTag : IEntityBase
    {
        public int PostId { get; set; }
        public int TagId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
