using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Core.ApiResponse
{
    public class PaginatedList
    {
        public int TotalCount { get; set; }

        public int Page { get; set; }
        public int Limit { get; set; }
        public object Data { get; set; }
    }
}
