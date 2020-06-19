using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class SortrableParams
    {
        public string OrderByField { get; set; }

        public bool OrderByDesceding { get; set; } = true;
    }
}
