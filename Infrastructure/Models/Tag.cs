using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class Tag : IEntityBase
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public virtual ICollection<PostTag> PostTag { get; set; } = new HashSet<PostTag>();
    }
}
