using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace School_Data.DTOs
{
    public class PagingDTO
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? FieldOrder { get; set; }
        public bool IsAsc { get; set; } = true;
        public string? FilterFieldName { get; set; }
        public string? Filter { get; set; }
    }
}
