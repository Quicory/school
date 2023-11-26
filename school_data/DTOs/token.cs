
using System.Text.Json.Serialization;

namespace School_Data.DTOs
{
    public class Token : UserDTO
    {
        public string token { get; set; }
        [JsonPropertyName("roles")]
        public List<string> Roles { get; set; }
    }
}
