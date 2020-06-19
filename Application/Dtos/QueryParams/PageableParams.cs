using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.QueryParams
{
    public class PageableParams
    {
        [BindRequired]
        public int Page { get; set; }

        [BindRequired]
        public int PageSize { get; set; }


        public bool FirstRequest { get; set; } = true;
    }
}
