using System.Net;

namespace School_Data.Helpers
{
    public class APIResponse
    {        
        public HttpStatusCode StatusCode { get; set; }     
        public bool IsValid { get; set; } = true;     
        public string Message { get; set; } = string.Empty;     
        public List<string> ErrorMessages { get; set; }     
        public object Result { get; set; }

    }
}
