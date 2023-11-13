using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace School_Data.DTOs
{
    public class APIResponse
    {
        public HttpStatusCode  StatusCode { get; set; }
        public bool IsValid { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }

    }
}
