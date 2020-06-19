using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class PaginationDto<TEntity> where TEntity : class
    {
        public List<TEntity> Page { get; set; }

        public int? TotalCount { get; set; }
    }
}
