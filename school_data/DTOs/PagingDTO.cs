using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Data.DTOs
{
    public class PagingDTO
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? FieldOrder { get; set; }
        public bool IsAsc { get; set; } = true;
        public string? Filter { get; set; }
    }
}
