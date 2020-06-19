using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.QueryParams.Post
{
    public class PostQueryParams : PageableParams
    {
        public int[] Tags { get; set; }
        public string SearchTerm { get; set; }
        public SortrableParams SortableParams { get; set; }
    }
}
